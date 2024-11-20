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

    //Contra gravedad
    public Transform groundCheck;
    public float sphereRadius = 0.3f;
    public LayerMask groundMask;
    public GameObject gunPrefab;
    public GameObject gunHolder;
    bool isGrounded;
    private bool gunActive = false;

    Vector3 velocity;

    //Salto
    public float jumpHeight = 3;

    // Declaramos una variable privada llamada 'ani' de tipo Animator, que nos permitirá controlar las animaciones del personaje.
    private Animator ani;
    void Start()
    {
        // Asignamos a 'ani' el componente Animator del GameObject al que está adjunto este script.
        ani = GetComponent<Animator>();
    }

    // Método Update: Se ejecuta una vez por frame y es ideal para manejar entradas del usuario.
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, sphereRadius, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        // Capturamos la entrada del usuario en el eje horizontal (A/D o flechas izquierda/derecha).
        float horizontal = Input.GetAxisRaw("Horizontal");
        // Capturamos la entrada del usuario en el eje vertical (W/S o flechas arriba/abajo).
        float vertical = Input.GetAxisRaw("Vertical");


        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        characterController.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);


        // Si el usuario está presionando alguna tecla que genere movimiento (horizontal o vertical no son cero):
        if (horizontal != 0f || vertical != 0f)
        {
            // Activamos la animación de movimiento configurando el parámetro "isMove" en el Animator a true.
            ani.SetBool("isMove", true);
        }
        else // Si el personaje no se está moviendo (ni horizontal ni verticalmente):
        {
            // Desactivamos la animación de movimiento configurando el parámetro "isMove" en el Animator a false.
            ani.SetBool("isMove", false);
        }

        if (Input.GetKeyDown(KeyCode.E) && !gunActive)
        {
            GameObject gun = Instantiate(gunPrefab, gunHolder.transform);
            gunActive = true;
        }

        if (Input.GetKeyDown(KeyCode.R) && gunActive)
        {
            Destroy(gunPrefab);
            gunActive = false;
        }
    }
}
