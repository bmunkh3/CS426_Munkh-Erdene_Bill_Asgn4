using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    public float speed = 2f;
    public Transform cameraTransform;  // Reference to the Camera's transform

    // Create a list of colors
    public List<Color> colors = new List<Color>();

    // References for audio and camera
    [SerializeField] private AudioListener audioListener;
    [SerializeField] private Camera playerCamera;

    void Update()
    {
        if (!IsOwner) return;  // Only run this script for the owner of the object

        HandleMovement();
    }

    private void HandleMovement()
    {
        // Calculate movement direction based on camera orientation
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;  // Ensure movement is only on the x-z plane
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = new Vector3();
        if (Input.GetKey(KeyCode.W))
            moveDirection += forward;
        if (Input.GetKey(KeyCode.S))
            moveDirection -= forward;
        if (Input.GetKey(KeyCode.A))
            moveDirection -= right;
        if (Input.GetKey(KeyCode.D))
            moveDirection += right;

        transform.position += moveDirection * speed * Time.deltaTime;
    }

    public override void OnNetworkSpawn()
    {
        if (colors.Count > (int)OwnerClientId)
            GetComponent<MeshRenderer>().material.color = colors[(int)OwnerClientId];

        if (IsOwner)
        {
            // Enable the camera and audio listener for the owner
            audioListener.enabled = true;
            playerCamera.enabled = true;
        }
    }
}
