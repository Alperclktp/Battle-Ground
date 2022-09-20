using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity;

    public Transform target, player;

    float mouseX, mouseY;

    private void Start()
    {
        Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        CameraControl();
    }

    private void CameraControl()
    {
        mouseX += Input.GetAxis("Mouse X") * sensitivity;

        mouseY -= Input.GetAxis("Mouse Y") * sensitivity;

        mouseY = Mathf.Clamp(mouseY, -7f, 35);

        transform.LookAt(target);

        if (Input.GetKey(KeyCode.LeftShift)) //Free look
        {
            target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        }
        else
        {
            target.rotation = Quaternion.Euler(mouseY, mouseX, 0);

            player.rotation = Quaternion.Euler(0, mouseX, 0);
        }
    }   
}
