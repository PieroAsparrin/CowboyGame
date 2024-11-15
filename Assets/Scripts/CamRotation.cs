using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class CamRotation : MonoBehaviour
{
    private float MouseSensitivity = 2f;

    // [SerializeField] private Transform CameraTransform;
    [SerializeField] private Transform PlayerPosition;

    private float yRotation;
    private float xRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        

        // Se obtiene el movimiento del ratón en el eje X y Y
        float mouseX = Input.GetAxisRaw("Mouse X") * MouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * MouseSensitivity;



        // Rotación horizontal (giro alrededor del eje Y)
        PlayerPosition.Rotate(Vector3.up * mouseX);

        // Rotación vertical (giro alrededor del eje X), limitando el valor para evitar rotaciones extremas
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // Limita la rotación para evitar giros completos

        // Aplicar la rotación vertical a la cámara
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
