using UnityEngine;
using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;

public class OutPutDeviceScore : NetworkBehaviour
{
    // Reference to your Score manager.
    // You can assign this in the Inspector, or it will try to find one at runtime.
    public Score scoreManager;

    void OnCollisionEnter(Collision target)
    {
        Debug.Log("collided!");

        if (target.gameObject.CompareTag("output data"))
        {
            // Increase the score using the Score manager's AddPoint method.
            if (scoreManager != null)
            {
                scoreManager.AddPoint();
            }
            else
            {
                // Try to find the Score manager if it wasn't assigned.
                Score foundScore = FindObjectOfType<Score>();
                if (foundScore != null)
                {
                    foundScore.AddPoint();
                }
                else
                {
                    Debug.LogWarning("Score Manager not found in the scene.");
                }
            }

            // If the object is a networked object, despawn it before destroying.
            NetworkObject netObj = target.gameObject.GetComponent<NetworkObject>();
            if (netObj != null)
            {
                netObj.Despawn();
            }
            
            // Destroy the collided pickupable object.
            Destroy(target.gameObject);
        }
    }
}
