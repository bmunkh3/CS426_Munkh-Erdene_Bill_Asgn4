using UnityEngine;
using Unity.Netcode;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Services.Core;
using Unity.Services.Authentication;

public class GameOverScreen : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartButton()
    {
        Time.timeScale = 1f;
        if (NetworkManager.Singleton != null && NetworkManager.Singleton.IsHost)
        {
            // Shut down the network session.
            NetworkManager.Singleton.Shutdown();
            SceneManager.LoadScene("Game_Scene");
        }
        else
        {
            SceneManager.LoadScene("Game_Scene");
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
