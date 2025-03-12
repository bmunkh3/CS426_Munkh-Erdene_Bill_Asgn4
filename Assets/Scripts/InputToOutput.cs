using UnityEngine;
using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;

public class InputToOutput : NetworkBehaviour
{
    [SerializeField]
    public GameObject outputData;
    public GameObject instantOutput;

    void OnCollisionEnter(Collision target)
    {
        Debug.Log("collided!");

        if (target.gameObject.CompareTag("input data"))
        {
            // Use the first contact point and add an offset of 5 meters in the direction of the contact normal.
            Vector3 spawnLoc = target.contacts[0].point + target.contacts[0].normal * 5f;

            // Instantiate the output object at the calculated location.
            instantOutput = Instantiate(outputData, spawnLoc, Quaternion.identity);
            instantOutput.GetComponent<NetworkObject>().Spawn(true);

            // Despawn and destroy the collided pickupable object.
            target.gameObject.GetComponent<NetworkObject>().Despawn();
            Destroy(target.gameObject);
        }
        
    }
}
