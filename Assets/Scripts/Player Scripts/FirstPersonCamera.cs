using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// taken from https://www.youtube.com/watch?v=5Rq8A4H6Nzw
public class FirstPersonCamera : MonoBehaviour
{

    // Variables
    public Transform player;
    public float mouseSensitivity = 2f;
    float cameraVerticalRotation = 0f;

    bool lockedCursor = true;


    void Start()
    {
        UpdateCursorState();
    }


    void Update()
    {
        // Collect Mouse Input

        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate the Camera around its local X axis

        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;


        // Rotate the Player Object and the Camera around its Y axis

        player.Rotate(Vector3.up * inputX);

    }


    void UpdateCursorState()
    {
        if (Time.timeScale == 0f)
        {
            // World is frozen: unlock the cursor and make it visible.
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            // World is not frozen: lock the cursor and hide it.
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}