using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class CamRotation : MonoBehaviour
{
    public float MouseSensitivity = 0f;//Sensibilidad

    // [SerializeField] private Transform CameraTransform;
    [SerializeField] private Transform PlayerPosition;

    private float yRotation;
    private float xRotation;

    public float upLimit = -19f; //Establecer� el limite maximo para subir la camara
    public float downLimit = 22f; //Establecer� el limite manimo para bajar la camara

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        

        // Se obtiene el movimiento del rat�n en el eje X y Y
        float mouseX = Input.GetAxisRaw("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * MouseSensitivity * Time.deltaTime;



        // Rotaci�n horizontal (giro alrededor del eje Y)
        PlayerPosition.Rotate(Vector3.up * mouseX);

        // Rotaci�n vertical (giro alrededor del eje X), limitando el valor para evitar rotaciones extremas
        xRotation -= mouseY;//Utilizo -= porque el ejeY del raton esta invertido
        xRotation = Mathf.Clamp(xRotation, upLimit, downLimit);  // Limita la rotaci�n para evitar giros completos

        // Aplicar la rotaci�n vertical a la c�mara
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
