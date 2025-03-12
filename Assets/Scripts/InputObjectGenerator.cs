using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;

public class InputObjectGenerator : NetworkBehaviour
{
    [SerializeField] private GameObject dataPrefab;
    // where the object you want to spawn over and over goes.
        
    public GameObject instantData;
    // timer stuff: starts at 0
    // spawnTime: how many seconds until the next object spawns
    private float timer = 0.0f;
    private float spawnTime = 3.5f;




    // Start is called once before the first execution of Update after the MonoBehaviour is created

    
    // Update is called once per frame

    void Update()
    {
        if(IsServer){
            Debug.Log("Host Started!");
            spawn();
        }
        
    }

    void spawn(){
            
            timer += Time.deltaTime;

            // if two seconds have passed, spawn the object in.
            if(timer > spawnTime){
            Vector3 randomPos = new Vector3(Random.Range(7, -10), 4, Random.Range(-9, -12));
            instantData = Instantiate(dataPrefab, randomPos, Quaternion.identity);
            instantData.GetComponent<NetworkObject>().Spawn(true);
            timer = timer - spawnTime;
            }
        
    }


}
