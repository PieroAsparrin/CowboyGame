using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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

    bool isGrounded;

    Vector3 velocity;

    //Salto
    public float jumpHeight = 3;


    public bool isSprinting;
    public float isSprintingSpeedMultiplier = 1.5f;
    private float sprintSpeed = 1;

    public float staminaUseAmount = 5;
    private StaminaBar staminaSlider;

    // Declaramos una variable privada llamada 'ani' de tipo Animator, que nos permitirá controlar las animaciones del personaje.
    private Animator ani;
    void Start()
    {
        staminaSlider = FindObjectOfType<StaminaBar>();
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

        JumpCheck();
        RunCheck();

        characterController.Move(move * speed * Time.deltaTime * sprintSpeed);

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

        MovementCheck(vertical, horizontal);
    }

    public void JumpCheck() {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }

    public void RunCheck()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = !isSprinting;
        }
        if (isSprinting == true)
        {
            sprintSpeed = isSprintingSpeedMultiplier;
            staminaSlider.UseStamina(staminaUseAmount);
        }
        else 
        {
            sprintSpeed = 1;
            staminaSlider.UseStamina(0);
        }
    }

    public void MovementCheck(float vertical, float horizontal) {

        if (vertical > 0f)
        {
            ani.SetBool("walkingForward", true);
        }
        else
        {
            ani.SetBool("walkingForward", false);
        }

        if (vertical < 0f)
        {
            ani.SetBool("walkingBackwards", true);
        }
        else
        {
            ani.SetBool("walkingBackwards", false);
        }

        if (horizontal > 0f) 
        {
            ani.SetBool("walkingRight", true);
        }
        else
        {
            ani.SetBool("walkingRight", false);
        }

        if (horizontal < 0f)
        {
            ani.SetBool("walkingLeft", true);
        }
        else
        {
            ani.SetBool("walkingLeft", false);
        }
    }
}
