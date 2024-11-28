using UnityEngine;

public class MinimapIconRotation : MonoBehaviour
{
    [SerializeField] private Transform player; // Referencia al jugador
    [SerializeField] private Camera playerCamera; // Referencia a la c�mara del jugador

    void Update()
    {
        // Obt�n solo la rotaci�n en el eje Y de la c�mara
        float playerYRotation = playerCamera.transform.eulerAngles.y;

        // Aplica la rotaci�n en el eje Z del icono del minimapa para que se alinee con la c�mara del jugador
        transform.rotation = Quaternion.Euler(0, 0, -playerYRotation);
    }
}
