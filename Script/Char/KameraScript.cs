using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraScript : MonoBehaviour
{
    public float mouseSensitivitas = 100f;

    public Transform BadanPlayer;

    float xRotasi = 0f;

    public CharacterController kontrol;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivitas * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivitas * Time.deltaTime;

        xRotasi -= mouseY;
        xRotasi = Mathf.Clamp(xRotasi, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotasi, 0f, 0f);
        BadanPlayer.Rotate(Vector3.up * mouseX);

        if (!kontrol.isGrounded)
        {
            BadanPlayer.Rotate(Vector3.left * mouseY);
        }

    }
}
