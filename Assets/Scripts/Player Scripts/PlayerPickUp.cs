// using System.Collections;
// using System.Collections.Generic;
// using Unity.Netcode;
// using UnityEngine;

// public class PickUpObject : NetworkBehaviour
// {
//     private NetworkObject heldNetworkObject;
//     private GameObject heldObject;
//     public float radius = 2f;
//     public float distance = 2f;
//     public float height = 1f;
//     public float throwForce = 750f; // Force applied when throwing

//     private void Update()
//     {
//         // Only process input on the local client
//         if (!IsOwner) return;

//         var t = transform;
//         var pressedE = Input.GetKeyDown(KeyCode.E);
//         var pressedLeftClick = Input.GetMouseButtonDown(0);

//         if (heldObject)
//         {
//             if (pressedE)
//             {
//                 ReleaseObjectServerRpc();
//             }
//             else if (pressedLeftClick)
//             {
//                 ThrowObjectServerRpc();
//             }
//         }
//         else
//         {
//             if (pressedE)
//             {
//                 var hits = Physics.SphereCastAll(t.position + t.forward, radius, t.forward, radius);
//                 var hitIndex = System.Array.FindIndex(hits, hit => hit.transform.CompareTag("Pickupable"));
//                 if (hitIndex != -1)
//                 {
//                     var hitObject = hits[hitIndex].transform.gameObject;
//                     var networkObject = hitObject.GetComponent<NetworkObject>();
//                     if (networkObject != null)
//                     {
//                         PickupObjectServerRpc(networkObject.NetworkObjectId);
//                     }
//                 }
//             }
//         }
//     }

//     [ServerRpc]
//     private void ThrowObjectServerRpc()
//     {
//         if (heldObject == null) return;

//         var rigidBody = heldObject.GetComponent<Rigidbody>();
//         rigidBody.linearDamping = 1f;
//         rigidBody.useGravity = true;
//         rigidBody.constraints = RigidbodyConstraints.None;

//         // Apply throwing force in the forward direction of the player
//         rigidBody.AddForce(transform.forward * throwForce);

//         // Notify all clients about the throw
//         ThrowObjectClientRpc(heldNetworkObject.NetworkObjectId);

//         heldNetworkObject = null;
//         heldObject = null;
//     }

//     [ClientRpc]
//     private void ThrowObjectClientRpc(ulong networkObjectId)
//     {
//         if (IsServer) return; // Server already handled this
//         heldNetworkObject = null;
//         heldObject = null;
//     }

//     [ServerRpc]
//     private void PickupObjectServerRpc(ulong networkObjectId)
//     {
//         var networkObject = NetworkManager.Singleton.SpawnManager.SpawnedObjects[networkObjectId];
//         if (networkObject == null) return;

//         heldNetworkObject = networkObject;
//         heldObject = networkObject.gameObject;
//         var rigidBody = heldObject.GetComponent<Rigidbody>();
//         rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
//         rigidBody.linearDamping = 25f;
//         rigidBody.useGravity = false;

//         // Notify all clients about the pickup
//         PickupObjectClientRpc(networkObjectId);
//     }

//     [ClientRpc]
//     private void PickupObjectClientRpc(ulong networkObjectId)
//     {
//         if (IsServer) return; // Server already handled this
//         var networkObject = NetworkManager.Singleton.SpawnManager.SpawnedObjects[networkObjectId];
//         if (networkObject == null) return;

//         heldNetworkObject = networkObject;
//         heldObject = networkObject.gameObject;
//     }

//     [ServerRpc]
//     private void ReleaseObjectServerRpc()
//     {
//         if (heldObject == null) return;

//         var rigidBody = heldObject.GetComponent<Rigidbody>();
//         rigidBody.linearDamping = 1f;
//         rigidBody.useGravity = true;
//         rigidBody.constraints = RigidbodyConstraints.None;

//         // Notify all clients about the release
//         ReleaseObjectClientRpc(heldNetworkObject.NetworkObjectId);

//         heldNetworkObject = null;
//         heldObject = null;
//     }

//     [ClientRpc]
//     private void ReleaseObjectClientRpc(ulong networkObjectId)
//     {
//         if (IsServer) return; // Server already handled this
//         heldNetworkObject = null;
//         heldObject = null;
//     }

//     private void FixedUpdate()
//     {
//         // Only the server should physically move the object
//         if (!IsServer || heldObject == null) return;

//         var t = transform;
//         var rigidBody = heldObject.GetComponent<Rigidbody>();
//         var moveTo = t.position + distance * t.forward + height * t.up;
//         var difference = moveTo - heldObject.transform.position;
//         rigidBody.AddForce(difference * 500);
//         heldObject.transform.rotation = t.rotation;
//     }
// }



using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PickUpObject : NetworkBehaviour
{
    private NetworkObject heldNetworkObject;
    private GameObject heldObject;
    public float radius = 2f;
    public float distance = 2f;
    public float height = 1f;
    public float throwForce = 750f; // Force applied when throwing

    private void Update()
    {
        // Only process input on the local client
        if (!IsOwner) return;

        var t = transform;
        var pressedE = Input.GetKeyDown(KeyCode.E);
        var pressedLeftClick = Input.GetMouseButtonDown(0);

        if (heldObject)
        {
            if (pressedE)
            {
                ReleaseObjectServerRpc();
            }
            else if (pressedLeftClick)
            {
                ThrowObjectServerRpc();
            }
        }
        else
        {
            if (pressedE)
            {
                // Use SphereCastAll to find nearby objects.
                var hits = Physics.SphereCastAll(t.position + t.forward, radius, t.forward, radius);
                // Check if any hit has a tag "input data" or "output data"
                var hitIndex = System.Array.FindIndex(hits, hit =>
                    hit.transform.CompareTag("input data") || hit.transform.CompareTag("output data"));
                if (hitIndex != -1)
                {
                    var hitObject = hits[hitIndex].transform.gameObject;
                    var networkObject = hitObject.GetComponent<NetworkObject>();
                    if (networkObject != null)
                    {
                        PickupObjectServerRpc(networkObject.NetworkObjectId);
                    }
                }
            }
        }
    }

    [ServerRpc]
    private void ThrowObjectServerRpc()
    {
        if (heldObject == null) return;

        var rigidBody = heldObject.GetComponent<Rigidbody>();
        rigidBody.linearDamping = 1f;
        rigidBody.useGravity = true;
        rigidBody.constraints = RigidbodyConstraints.None;

        // Apply throwing force in the forward direction of the player
        rigidBody.AddForce(transform.forward * throwForce);

        // Notify all clients about the throw
        ThrowObjectClientRpc(heldNetworkObject.NetworkObjectId);

        heldNetworkObject = null;
        heldObject = null;
    }

    [ClientRpc]
    private void ThrowObjectClientRpc(ulong networkObjectId)
    {
        if (IsServer) return; // Server already handled this
        heldNetworkObject = null;
        heldObject = null;
    }

    [ServerRpc]
    private void PickupObjectServerRpc(ulong networkObjectId)
    {
        var networkObject = NetworkManager.Singleton.SpawnManager.SpawnedObjects[networkObjectId];
        if (networkObject == null) return;

        heldNetworkObject = networkObject;
        heldObject = networkObject.gameObject;
        var rigidBody = heldObject.GetComponent<Rigidbody>();
        rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        rigidBody.linearDamping = 25f;
        rigidBody.useGravity = false;

        // Notify all clients about the pickup
        PickupObjectClientRpc(networkObjectId);
    }

    [ClientRpc]
    private void PickupObjectClientRpc(ulong networkObjectId)
    {
        if (IsServer) return; // Server already handled this
        var networkObject = NetworkManager.Singleton.SpawnManager.SpawnedObjects[networkObjectId];
        if (networkObject == null) return;

        heldNetworkObject = networkObject;
        heldObject = networkObject.gameObject;
    }

    [ServerRpc]
    private void ReleaseObjectServerRpc()
    {
        if (heldObject == null) return;

        var rigidBody = heldObject.GetComponent<Rigidbody>();
        rigidBody.linearDamping = 1f;
        rigidBody.useGravity = true;
        rigidBody.constraints = RigidbodyConstraints.None;

        // Notify all clients about the release
        ReleaseObjectClientRpc(heldNetworkObject.NetworkObjectId);

        heldNetworkObject = null;
        heldObject = null;
    }

    [ClientRpc]
    private void ReleaseObjectClientRpc(ulong networkObjectId)
    {
        if (IsServer) return; // Server already handled this
        heldNetworkObject = null;
        heldObject = null;
    }

    private void FixedUpdate()
    {
        // Only the server should physically move the object
        if (!IsServer || heldObject == null) return;

        var t = transform;
        var rigidBody = heldObject.GetComponent<Rigidbody>();
        var moveTo = t.position + distance * t.forward + height * t.up;
        var difference = moveTo - heldObject.transform.position;
        rigidBody.AddForce(difference * 500);
        heldObject.transform.rotation = t.rotation;
    }
}
