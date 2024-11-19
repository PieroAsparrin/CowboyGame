using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayerController : MonoBehaviour
{
    public CharacterController characterController;

    // Declaramos una variable privada llamada 'speed' para controlar la velocidad de movimiento del personaje. Inicialmente es 10.
    public float speed = 10f;

    // Declaramos la gravedad
    public float gravity = -9.81f;

    Vector3 velocity;

    // Declaramos una variable privada llamada 'ani' de tipo Animator, que nos permitir� controlar las animaciones del personaje.
    private Animator ani;
    void Start()
    {
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


        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        characterController.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);



















        // Si el usuario est� presionando alguna tecla que genere movimiento (horizontal o vertical no son cero):
        if (horizontal != 0f || vertical != 0f)
        {
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
