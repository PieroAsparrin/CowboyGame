using System.Collections; // Importa la librer�a para trabajar con colecciones gen�ricas.
using System.Collections.Generic; // Importa la librer�a para usar colecciones m�s avanzadas, como listas, diccionarios, etc.
using UnityEngine; // Importa la librer�a de Unity para trabajar con sus funcionalidades.

public class PlayerController : MonoBehaviour // Define la clase PlayerController que controla el comportamiento del jugador.
{
    public CharacterController characterController; // Declara la variable para el CharacterController que permite manejar la f�sica del personaje.

    // Declaramos una variable p�blica llamada 'speed' para controlar la velocidad de movimiento del personaje. Inicialmente es 10.
    public float speed = 10f;

    // Declaramos la gravedad.
    public float gravity = -9.81f;

    // Variables relacionadas con la comprobaci�n del suelo.
    public Transform groundCheck; // El punto desde donde se verifica si el jugador est� tocando el suelo.
    public float sphereRadius = 0.3f; // El radio de la esfera usada para la comprobaci�n de contacto con el suelo.
    public LayerMask groundMask; // M�scara de capa para determinar qu� se considera "suelo".
    bool isGrounded; // Boleano que indica si el jugador est� tocando el suelo.

    Vector3 velocity; // Variable para manejar la velocidad, incluyendo la gravedad.

    // Configuraci�n para el salto.
    public float jumpHeight = 3; // Altura del salto.

    // Variables para manejar el sprint.
    public bool isSprinting; // Estado que indica si el jugador est� corriendo a alta velocidad.
    public float isSprintingSpeedMultiplier = 1.5f; // Multiplicador de velocidad cuando se est� sprintando.
    private float sprintSpeed = 1; // Variable privada para controlar la velocidad de sprint.

    public float staminaUseAmount = 5; // Cantidad de estamina que se usa al sprintar.
    private StaminaBar staminaSlider; // Referencia a la barra de estamina del jugador.

    // Declaramos una variable privada llamada 'ani' de tipo Animator, que nos permitir� controlar las animaciones del personaje.
    private Animator ani;

    void Start()
    {
        staminaSlider = FindObjectOfType<StaminaBar>(); // Encontramos el objeto StaminaBar en la escena.
        ani = GetComponent<Animator>(); // Asignamos el componente Animator del GameObject al que est� adjunto este script.
    }

    // M�todo Update: Se ejecuta una vez por frame y es ideal para manejar entradas del usuario.
    void Update()
    {
        // Verificamos si el jugador est� tocando el suelo.
        isGrounded = Physics.CheckSphere(groundCheck.position, sphereRadius, groundMask);

        // Si el jugador est� tocando el suelo y est� cayendo, restablecemos la velocidad en el eje Y.
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Velocidad negativa para que el jugador "pegue" al suelo.
        }

        // Capturamos la entrada del usuario en el eje horizontal (A/D o flechas izquierda/derecha).
        float horizontal = Input.GetAxisRaw("Horizontal");
        // Capturamos la entrada del usuario en el eje vertical (W/S o flechas arriba/abajo).
        float vertical = Input.GetAxisRaw("Vertical");

        // Calculamos el movimiento en funci�n de las entradas del usuario.
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        // Llamamos a los m�todos para controlar el salto y el sprint.
        JumpCheck();
        RunCheck();

        // Movemos el CharacterController en funci�n del movimiento calculado, velocidad y tiempo entre frames (Time.deltaTime).
        characterController.Move(move * speed * Time.deltaTime * sprintSpeed);

        // Aplicamos la gravedad.
        velocity.y += gravity * Time.deltaTime;

        // Movemos el CharacterController con la gravedad aplicada.
        characterController.Move(velocity * Time.deltaTime);

        // Llamamos a MovementCheck para actualizar las animaciones en funci�n del movimiento.
        MovementCheck(vertical, horizontal);
    }

    public void JumpCheck()
    {
        ani.SetBool("isJumping", false);
        // Si el jugador presiona espacio y est� en el suelo, aplicamos el salto.
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            ani.SetBool("isJumping", true);
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); // Calculamos la velocidad necesaria para alcanzar la altura del salto.
        }
    }

    public void RunCheck()
    {
        // Comprobamos si el jugador presiona Shift para activar/desactivar el sprint.
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = !isSprinting; // Alternamos el estado de sprint.
        }

        // Solo permitimos el sprint si el jugador est� en el suelo y movi�ndose hacia adelante.
        if (isSprinting)
        {
            // Si el jugador no est� en el suelo o no est� movi�ndose hacia adelante, cancelamos el sprint.
            if (!isGrounded || Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") <= 0)
            {
                isSprinting = false; // Cancelamos el sprint si no cumple las condiciones.
            }
            else
            {
                sprintSpeed = isSprintingSpeedMultiplier; // Aumentamos la velocidad al sprintar.

                // Consumimos estamina usando Time.deltaTime para normalizar el consumo.
                staminaSlider.UseStamina(staminaUseAmount * Time.deltaTime);
            }
        }
        else
        {
            sprintSpeed = 1; // Restauramos la velocidad normal si no se est� sprintando.
        }

        // Actualizamos la animaci�n en funci�n de si se est� sprintando.
        ani.SetBool("isSprinting", isSprinting);
    }

    // Este m�todo se encarga de cambiar las animaciones dependiendo de la direcci�n en que se mueve el jugador.
    public void MovementCheck(float vertical, float horizontal)
    {
        // Si el jugador se mueve hacia adelante, se activa la animaci�n de caminar hacia adelante.
        if (vertical > 0f)
        {
            ani.SetBool("walkingForward", true);
        }
        else
        {
            ani.SetBool("walkingForward", false);
        }

        // Si el jugador se mueve hacia atr�s, se activa la animaci�n de caminar hacia atr�s.
        if (vertical < 0f)
        {
            ani.SetBool("walkingBackwards", true);
        }
        else
        {
            ani.SetBool("walkingBackwards", false);
        }

        // Si el jugador se mueve hacia la derecha, se activa la animaci�n de caminar a la derecha.
        if (horizontal > 0f)
        {
            ani.SetBool("walkingRight", true);
        }
        else
        {
            ani.SetBool("walkingRight", false);
        }

        // Si el jugador se mueve hacia la izquierda, se activa la animaci�n de caminar a la izquierda.
        if (horizontal < 0f)
        {
            ani.SetBool("walkingLeft", true);
        }
        else
        {
            ani.SetBool("walkingLeft", false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Comprobamos si el objeto que colision� tiene el tag "Melee".
        if (other.CompareTag("Melee"))
        {
            // Obtenemos el componente Knife del objeto que colision�.
            Knife knife = other.GetComponent<Knife>();

            if (knife != null)
            {
                // Obtenemos el da�o del Knife y lo aplicamos al jugador.
                float damage = knife.GetDamage();
                GameManager.Instance.TakeDamage(damage); // Llamamos al m�todo del GameManager para reducir la salud.
                Debug.Log("�Jugador recibi� " + damage + " de da�o!");
            }
        }
    }

}