using UnityEngine;

public class MinimapIconRotation : MonoBehaviour
{
    [SerializeField] private Transform player; // Referencia al jugador
    [SerializeField] private Camera playerCamera; // Referencia a la cámara del jugador

    void Update()
    {
        // Obtén solo la rotación en el eje Y de la cámara
        float playerYRotation = playerCamera.transform.eulerAngles.y;

        // Aplica la rotación en el eje Z del icono del minimapa para que se alinee con la cámara del jugador
        transform.rotation = Quaternion.Euler(0, 0, -playerYRotation);
    }
}
