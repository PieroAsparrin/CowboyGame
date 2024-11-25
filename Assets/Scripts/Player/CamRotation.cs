using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class CamRotation : MonoBehaviour
{
    public float MouseSensitivity = 0f; // Sensibilidad del rat�n

    // [SerializeField] private Transform CameraTransform; // Referencia a la transformaci�n de la c�mara (comentada)
    [SerializeField] private Transform PlayerPosition; // Referencia a la posici�n del jugador (para rotarlo horizontalmente)

    private float yRotation; // Rotaci�n alrededor del eje Y (horizontal)
    private float xRotation; // Rotaci�n alrededor del eje X (vertical)

    public float upLimit = -19f; // L�mite m�ximo para la rotaci�n hacia arriba (evita que la c�mara suba demasiado)
    public float downLimit = 22f; // L�mite m�ximo para la rotaci�n hacia abajo (evita que la c�mara baje demasiado)

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor en el centro de la pantalla
        Cursor.visible = false; // Oculta el cursor para una mejor experiencia en primera persona
    }

    void Update()
    {
        // Se obtiene el movimiento del rat�n en los ejes X y Y (sin procesar)
        float mouseX = Input.GetAxisRaw("Mouse X") * MouseSensitivity * Time.deltaTime; // Movimiento horizontal del rat�n
        float mouseY = Input.GetAxisRaw("Mouse Y") * MouseSensitivity * Time.deltaTime; // Movimiento vertical del rat�n

        // Rotaci�n horizontal (giro alrededor del eje Y del jugador)
        PlayerPosition.Rotate(Vector3.up * mouseX); // Rota al jugador en el eje Y en funci�n del movimiento del rat�n

        // Rotaci�n vertical (giro alrededor del eje X de la c�mara), limitando el valor para evitar rotaciones extremas
        xRotation -= mouseY; // El eje Y del rat�n est� invertido, por eso usamos -= para que el movimiento sea coherente
        xRotation = Mathf.Clamp(xRotation, upLimit, downLimit);  // Limita la rotaci�n para que no haya giros extremos de la c�mara

        // Aplicar la rotaci�n vertical a la c�mara
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Aplica la rotaci�n vertical a la c�mara
    }
}
