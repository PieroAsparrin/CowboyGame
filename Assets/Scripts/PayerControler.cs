using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Definimos una clase llamada PayerControler que hereda de MonoBehaviour, lo que le permite adjuntarse a un GameObject en Unity.
public class PayerControler : MonoBehaviour
{
    // Declaramos una variable privada llamada 'rb' de tipo Rigidbody para manejar las físicas del personaje.
    private new Rigidbody rb;

    // Declaramos una variable privada llamada 'speed' para controlar la velocidad de movimiento del personaje. Inicialmente es 10.
    private float speed = 10f;

    // Declaramos una variable privada llamada 'ani' de tipo Animator, que nos permitirá controlar las animaciones del personaje.
    private Animator ani;

    // [SerializeField] permite exponer variables privadas en el inspector de Unity. Esta línea está comentada, pero serviría para
    // asignar manualmente un Transform, probablemente relacionado con la rotación de la cámara.
    //[SerializeField] private Transform CameraRotation;

    // Método Start: Se ejecuta una vez al inicio del juego o cuando este script se activa.
    void Start()
    {
        // Asignamos a 'rb' el componente Rigidbody del GameObject al que está adjunto este script.
        rb = GetComponent<Rigidbody>();

        // Asignamos a 'ani' el componente Animator del GameObject al que está adjunto este script.
        ani = GetComponent<Animator>();
    }

    // Método Update: Se ejecuta una vez por frame y es ideal para manejar entradas del usuario.
    void Update()
    {
        // Capturamos la entrada del usuario en el eje horizontal (A/D o flechas izquierda/derecha).
        float horizontal = Input.GetAxisRaw("Horizontal");

        // Capturamos la entrada del usuario en el eje vertical (W/S o flechas arriba/abajo).
        float vertical = Input.GetAxisRaw("Vertical");

        // Si el usuario está presionando alguna tecla que genere movimiento (horizontal o vertical no son cero):
        if (horizontal != 0f || vertical != 0f)
        {
            // Calculamos la dirección de movimiento del personaje combinando:
            // - transform.forward: dirección hacia adelante del objeto.
            // - transform.right: dirección hacia la derecha del objeto.
            // Multiplicamos estas direcciones por las entradas vertical y horizontal del usuario.
            Vector3 direccion = transform.forward * vertical + transform.right * horizontal;

            // Movemos el personaje a la nueva posición usando Rigidbody. 
            // Calculamos la nueva posición sumando:
            // - La posición actual (transform.position).
            // - La dirección calculada, escalada por la velocidad (speed) y ajustada por Time.deltaTime (para mantener un movimiento uniforme en diferentes FPS).
            rb.MovePosition(transform.position + direccion * speed * Time.deltaTime);

            // Activamos la animación de movimiento configurando el parámetro "isMove" en el Animator a true.
            ani.SetBool("isMove", true);
        }
        else // Si el personaje no se está moviendo (ni horizontal ni verticalmente):
        {
            // Desactivamos la animación de movimiento configurando el parámetro "isMove" en el Animator a false.
            ani.SetBool("isMove", false);
        }
    }
}
