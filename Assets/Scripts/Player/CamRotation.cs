using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class CamRotation : MonoBehaviour
{
    public float MouseSensitivity = 0f; // Sensibilidad del ratón

    // [SerializeField] private Transform CameraTransform; // Referencia a la transformación de la cámara (comentada)
    [SerializeField] private Transform PlayerPosition; // Referencia a la posición del jugador (para rotarlo horizontalmente)

    private float yRotation; // Rotación alrededor del eje Y (horizontal)
    private float xRotation; // Rotación alrededor del eje X (vertical)

    public float upLimit = -19f; // Límite máximo para la rotación hacia arriba (evita que la cámara suba demasiado)
    public float downLimit = 22f; // Límite máximo para la rotación hacia abajo (evita que la cámara baje demasiado)

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor en el centro de la pantalla
        Cursor.visible = false; // Oculta el cursor para una mejor experiencia en primera persona
    }

    void Update()
    {
        // Se obtiene el movimiento del ratón en los ejes X y Y (sin procesar)
        float mouseX = Input.GetAxisRaw("Mouse X") * MouseSensitivity * Time.deltaTime; // Movimiento horizontal del ratón
        float mouseY = Input.GetAxisRaw("Mouse Y") * MouseSensitivity * Time.deltaTime; // Movimiento vertical del ratón

        // Rotación horizontal (giro alrededor del eje Y del jugador)
        PlayerPosition.Rotate(Vector3.up * mouseX); // Rota al jugador en el eje Y en función del movimiento del ratón

        // Rotación vertical (giro alrededor del eje X de la cámara), limitando el valor para evitar rotaciones extremas
        xRotation -= mouseY; // El eje Y del ratón está invertido, por eso usamos -= para que el movimiento sea coherente
        xRotation = Mathf.Clamp(xRotation, upLimit, downLimit);  // Limita la rotación para que no haya giros extremos de la cámara

        // Aplicar la rotación vertical a la cámara
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Aplica la rotación vertical a la cámara
    }
}
