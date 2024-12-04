using System.Collections; // Importa la librería para trabajar con colecciones genéricas.
using System.Collections.Generic; // Importa la librería para usar colecciones más avanzadas, como listas, diccionarios, etc.
using UnityEngine; // Importa la librería de Unity para trabajar con sus funcionalidades.

public class PlayerController : MonoBehaviour // Define la clase PlayerController que controla el comportamiento del jugador.
{
    public CharacterController characterController; // Declara la variable para el CharacterController que permite manejar la física del personaje.

    // Declaramos una variable pública llamada 'speed' para controlar la velocidad de movimiento del personaje. Inicialmente es 10.
    public float speed = 10f;

    // Declaramos la gravedad.
    public float gravity = -9.81f;

    // Variables relacionadas con la comprobación del suelo.
    public Transform groundCheck; // El punto desde donde se verifica si el jugador está tocando el suelo.
    public float sphereRadius = 0.3f; // El radio de la esfera usada para la comprobación de contacto con el suelo.
    public LayerMask groundMask; // Máscara de capa para determinar qué se considera "suelo".
    bool isGrounded; // Boleano que indica si el jugador está tocando el suelo.

    Vector3 velocity; // Variable para manejar la velocidad, incluyendo la gravedad.

    // Configuración para el salto.
    public float jumpHeight = 3; // Altura del salto.

    // Variables para manejar el sprint.
    public bool isSprinting; // Estado que indica si el jugador está corriendo a alta velocidad.
    public float isSprintingSpeedMultiplier = 1.5f; // Multiplicador de velocidad cuando se está sprintando.
    private float sprintSpeed = 1; // Variable privada para controlar la velocidad de sprint.

    public float staminaUseAmount = 5; // Cantidad de estamina que se usa al sprintar.
    private StaminaBar staminaSlider; // Referencia a la barra de estamina del jugador.

    // Declaramos una variable privada llamada 'ani' de tipo Animator, que nos permitirá controlar las animaciones del personaje.
    private Animator ani;

    void Start()
    {
        staminaSlider = FindObjectOfType<StaminaBar>(); // Encontramos el objeto StaminaBar en la escena.
        ani = GetComponent<Animator>(); // Asignamos el componente Animator del GameObject al que está adjunto este script.
    }

    // Método Update: Se ejecuta una vez por frame y es ideal para manejar entradas del usuario.
    void Update()
    {
        // Verificamos si el jugador está tocando el suelo.
        isGrounded = Physics.CheckSphere(groundCheck.position, sphereRadius, groundMask);

        // Si el jugador está tocando el suelo y está cayendo, restablecemos la velocidad en el eje Y.
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Velocidad negativa para que el jugador "pegue" al suelo.
        }

        // Capturamos la entrada del usuario en el eje horizontal (A/D o flechas izquierda/derecha).
        float horizontal = Input.GetAxisRaw("Horizontal");
        // Capturamos la entrada del usuario en el eje vertical (W/S o flechas arriba/abajo).
        float vertical = Input.GetAxisRaw("Vertical");

        // Calculamos el movimiento en función de las entradas del usuario.
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        // Llamamos a los métodos para controlar el salto y el sprint.
        JumpCheck();
        RunCheck();

        // Movemos el CharacterController en función del movimiento calculado, velocidad y tiempo entre frames (Time.deltaTime).
        characterController.Move(move * speed * Time.deltaTime * sprintSpeed);

        // Aplicamos la gravedad.
        velocity.y += gravity * Time.deltaTime;

        // Movemos el CharacterController con la gravedad aplicada.
        characterController.Move(velocity * Time.deltaTime);

        // Llamamos a MovementCheck para actualizar las animaciones en función del movimiento.
        MovementCheck(vertical, horizontal);
    }

    public void JumpCheck()
    {
        ani.SetBool("isJumping", false);
        // Si el jugador presiona espacio y está en el suelo, aplicamos el salto.
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

        // Solo permitimos el sprint si el jugador está en el suelo y moviéndose hacia adelante.
        if (isSprinting)
        {
            // Si el jugador no está en el suelo o no está moviéndose hacia adelante, cancelamos el sprint.
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
            sprintSpeed = 1; // Restauramos la velocidad normal si no se está sprintando.
        }

        // Actualizamos la animación en función de si se está sprintando.
        ani.SetBool("isSprinting", isSprinting);
    }

    // Este método se encarga de cambiar las animaciones dependiendo de la dirección en que se mueve el jugador.
    public void MovementCheck(float vertical, float horizontal)
    {
        // Si el jugador se mueve hacia adelante, se activa la animación de caminar hacia adelante.
        if (vertical > 0f)
        {
            ani.SetBool("walkingForward", true);
        }
        else
        {
            ani.SetBool("walkingForward", false);
        }

        // Si el jugador se mueve hacia atrás, se activa la animación de caminar hacia atrás.
        if (vertical < 0f)
        {
            ani.SetBool("walkingBackwards", true);
        }
        else
        {
            ani.SetBool("walkingBackwards", false);
        }

        // Si el jugador se mueve hacia la derecha, se activa la animación de caminar a la derecha.
        if (horizontal > 0f)
        {
            ani.SetBool("walkingRight", true);
        }
        else
        {
            ani.SetBool("walkingRight", false);
        }

        // Si el jugador se mueve hacia la izquierda, se activa la animación de caminar a la izquierda.
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
        // Comprobamos si el objeto que colisionó tiene el tag "Melee".
        if (other.CompareTag("Melee"))
        {
            // Obtenemos el componente Knife del objeto que colisionó.
            Knife knife = other.GetComponent<Knife>();

            if (knife != null)
            {
                // Obtenemos el daño del Knife y lo aplicamos al jugador.
                float damage = knife.GetDamage();
                GameManager.Instance.TakeDamage(damage); // Llamamos al método del GameManager para reducir la salud.
                Debug.Log("¡Jugador recibió " + damage + " de daño!");
            }
        }
    }

}