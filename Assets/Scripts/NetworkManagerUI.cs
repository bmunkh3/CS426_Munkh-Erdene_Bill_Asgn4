using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Networking.Transport.Relay;
using Unity.Netcode.Transports.UTP;
using System.Collections.Generic;

public class NetworkManagerUI : NetworkBehaviour
{
    [SerializeField] private Button host_btn;
    [SerializeField] private Button client_btn;
    [SerializeField] private TMP_Text joinCodeText;
    [SerializeField] private TMP_Text playersListText;
    [SerializeField] private TMP_InputField joinCodeInputField;

    [SerializeField] private int maxPlayers = 4;

    public string joinCode;
    private Allocation hostAllocation;

    private void Awake()
    {
        if (host_btn == null || client_btn == null || joinCodeText == null || playersListText == null || joinCodeInputField == null)
        {
            Debug.LogError("One or more UI references are not assigned in the Inspector!");
        }

        // Set up button listeners.
        host_btn.onClick.AddListener(() => StartHostLobby());
        client_btn.onClick.AddListener(() =>
        {
            string code = joinCodeInputField.text.Trim();
            StartClientRelay(code);
        });
    }

    private async void Start()
    {
        if (host_btn != null) host_btn.interactable = false;
        if (client_btn != null) client_btn.interactable = false;

        try
        {
            // Only initialize Unity Services if they haven't been initialized yet.
            if (UnityServices.State != ServicesInitializationState.Initialized)
            {
                await UnityServices.InitializeAsync();
            }

            // Only sign in if the user isn't already signed in.
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error initializing Unity Services or signing in: " + e);
            return;
        }

        if (host_btn != null) host_btn.interactable = true;
        if (client_btn != null) client_btn.interactable = true;

        if (NetworkManager.Singleton == null)
        {
            Debug.LogError("NetworkManager.Singleton is null. Please ensure you have a NetworkManager in the scene.");
            return;
        }

        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;

        UpdatePlayersList();
    }

    private void OnDestroy()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnected;
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        UpdatePlayersList();
    }

    private void OnClientDisconnected(ulong clientId)
    {
        UpdatePlayersList();
    }

    private void UpdatePlayersList()
    {
        if (playersListText == null)
            return;

        string list = "Connected Players:\n";
        List<string> entries = new List<string>();
        bool hostSimulated = false;

        // If the host session is running, simulate the host slot.
        if (NetworkManager.Singleton != null && NetworkManager.Singleton.IsHost)
        {
            entries.Add("Player 1: Host");
            hostSimulated = true;
        }

        // Start numbering from Player 2 if the host is present.
        int slot = hostSimulated ? 2 : 1;
        List<ulong> clientIds = new List<ulong>(NetworkManager.Singleton.ConnectedClients.Keys);
        clientIds.Sort();

        foreach (ulong id in clientIds)
        {
            // Avoid duplicating the local client (the host).
            if (hostSimulated && id == NetworkManager.Singleton.LocalClientId)
                continue;
            entries.Add($"Player {slot}: Client {id}");
            slot++;
        }

        // Fill remaining slots with "Waiting..."
        for (int i = entries.Count + 1; i <= maxPlayers; i++)
        {
            entries.Add($"Player {i}: Waiting...");
        }

        foreach (string entry in entries)
        {
            list += entry + "\n";
        }
        playersListText.text = list;
    }

    public async void StartHostLobby()
    {
        try
        {
            hostAllocation = await RelayService.Instance.CreateAllocationAsync(maxPlayers);
            joinCode = await RelayService.Instance.GetJoinCodeAsync(hostAllocation.AllocationId);
        }
        catch (RelayServiceException e)
        {
            Debug.LogError($"Relay error: {e}");
            return;
        }

        // Configure UnityTransport with the Relay server data.
        var serverData = new RelayServerData(hostAllocation, "dtls");
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(serverData);

        // Start the host session so clients can join.
        NetworkManager.Singleton.StartHost();

        // Display the join code.
        joinCodeText.text = joinCode;
        UpdatePlayersList();

        // Hide interactive elements (buttons and join code input field) by making them invisible.
        HideInteractiveElements();
    }

    public async void StartClientRelay(string joinCode)
    {
        JoinAllocation joinAllocation = null;
        try
        {
            joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
        }
        catch (RelayServiceException e)
        {
            Debug.LogError($"Relay error: {e}");
            return;
        }

        var serverData = new RelayServerData(joinAllocation, "dtls");
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(serverData);

        // Start the client session.
        NetworkManager.Singleton.StartClient();

        // Hide interactive elements (buttons and join code input field) by making them invisible.
        HideInteractiveElements();
    }

    private void HideInteractiveElements()
    {
        HideElement(host_btn.gameObject);
        HideElement(client_btn.gameObject);
        HideElement(joinCodeInputField.gameObject);
    }

    private void HideElement(GameObject element)
    {
        CanvasGroup cg = element.GetComponent<CanvasGroup>();
        if (cg == null)
        {
            cg = element.AddComponent<CanvasGroup>();
        }
        cg.alpha = 0f; // Make the element invisible.
    }
}
