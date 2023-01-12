using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float Sensx , Sensy ;
    public Transform player_orientation;
    private float xRotation , yRotation ;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mousex = Input.GetAxisRaw("Mouse X") * Time.deltaTime * Sensx;
        float mousey = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * Sensy;

        yRotation += mousex;
        xRotation -= mousey;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        player_orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        

    }
}
