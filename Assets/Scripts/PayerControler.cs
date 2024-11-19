using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Definimos una clase llamada PayerControler que hereda de MonoBehaviour, lo que le permite adjuntarse a un GameObject en Unity.
public class PayerControler : MonoBehaviour
{
    // Declaramos una variable privada llamada 'rb' de tipo Rigidbody para manejar las f�sicas del personaje.
    private new Rigidbody rb;

    // Declaramos una variable privada llamada 'speed' para controlar la velocidad de movimiento del personaje. Inicialmente es 10.
    private float speed = 10f;

    // Declaramos una variable privada llamada 'ani' de tipo Animator, que nos permitir� controlar las animaciones del personaje.
    private Animator ani;

    // [SerializeField] permite exponer variables privadas en el inspector de Unity. Esta l�nea est� comentada, pero servir�a para
    // asignar manualmente un Transform, probablemente relacionado con la rotaci�n de la c�mara.
    //[SerializeField] private Transform CameraRotation;

    // M�todo Start: Se ejecuta una vez al inicio del juego o cuando este script se activa.
    void Start()
    {
        // Asignamos a 'rb' el componente Rigidbody del GameObject al que est� adjunto este script.
        rb = GetComponent<Rigidbody>();

        // Asignamos a 'ani' el componente Animator del GameObject al que est� adjunto este script.
        ani = GetComponent<Animator>();
    }

    // M�todo Update: Se ejecuta una vez por frame y es ideal para manejar entradas del usuario.
    void Update()
    {
        // Capturamos la entrada del usuario en el eje horizontal (A/D o flechas izquierda/derecha).
        float horizontal = Input.GetAxisRaw("Horizontal");

        // Capturamos la entrada del usuario en el eje vertical (W/S o flechas arriba/abajo).
        float vertical = Input.GetAxisRaw("Vertical");

        // Si el usuario est� presionando alguna tecla que genere movimiento (horizontal o vertical no son cero):
        if (horizontal != 0f || vertical != 0f)
        {
            // Calculamos la direcci�n de movimiento del personaje combinando:
            // - transform.forward: direcci�n hacia adelante del objeto.
            // - transform.right: direcci�n hacia la derecha del objeto.
            // Multiplicamos estas direcciones por las entradas vertical y horizontal del usuario.
            Vector3 direccion = transform.forward * vertical + transform.right * horizontal;

            // Movemos el personaje a la nueva posici�n usando Rigidbody. 
            // Calculamos la nueva posici�n sumando:
            // - La posici�n actual (transform.position).
            // - La direcci�n calculada, escalada por la velocidad (speed) y ajustada por Time.deltaTime (para mantener un movimiento uniforme en diferentes FPS).
            rb.MovePosition(transform.position + direccion * speed * Time.deltaTime);

            // Activamos la animaci�n de movimiento configurando el par�metro "isMove" en el Animator a true.
            ani.SetBool("isMove", true);
        }
        else // Si el personaje no se est� moviendo (ni horizontal ni verticalmente):
        {
            // Desactivamos la animaci�n de movimiento configurando el par�metro "isMove" en el Animator a false.
            ani.SetBool("isMove", false);
        }
    }
}
