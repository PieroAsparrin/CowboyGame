using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaSlider; // Componente Slider que representa la barra de estamina visualmente.
    public float maxStamina = 100f; // M�ximo valor de la estamina (cuando est� al m�ximo).
    private float currentStamina; // Valor actual de la estamina, cambia conforme se usa o se regenera.

    private float regenerateStaminaTime = 0.1f; // Tiempo entre regeneraci�n de cada unidad de estamina.
    private float regenerateAmount = 2f; // Cantidad de estamina que se regenera por cada intervalo.

    public float losingStaminaTime = 0.1f; // Tiempo entre cada decremento de estamina al usarla.

    private Coroutine myCoroutineLosing; // Referencia a la corrutina que maneja la p�rdida de estamina.
    private Coroutine myCoroutineRegenerate; // Referencia a la corrutina que maneja la regeneraci�n de estamina.

    // M�todo Start se llama una vez al inicio del juego
    void Start()
    {
        currentStamina = maxStamina; // Inicializa la estamina al m�ximo cuando empieza el juego.
        staminaSlider.maxValue = maxStamina; // Establece el valor m�ximo del slider de estamina.
        staminaSlider.value = maxStamina; // Inicializa la barra de estamina al valor m�ximo.
    }

    // Update es llamado una vez por frame, pero en este caso no se est� utilizando.
    void Update()
    {
        // No se necesita c�digo en el Update en este caso, ya que todo se maneja mediante corutinas.
    }

    // Este m�todo es llamado para restar una cantidad de estamina cuando se usa.
    public void UseStamina(float amount)
    {
        // Si la estamina actual es mayor que la cantidad que se quiere usar
        if (currentStamina - amount > 0)
        {
            // Si hay una corutina de p�rdida de estamina en curso, se detiene.
            if (myCoroutineLosing != null)
            {
                StopCoroutine(myCoroutineLosing);
            }
            // Inicia la corutina para perder estamina con el valor indicado.
            myCoroutineLosing = StartCoroutine(LosingStaminaCoroutine(amount));

            // Si hay una corutina de regeneraci�n de estamina en curso, se detiene.
            if (myCoroutineRegenerate != null)
            {
                StopCoroutine(myCoroutineRegenerate);
            }
            // Inicia la corutina para regenerar la estamina despu�s de haber usado estamina.
            myCoroutineRegenerate = StartCoroutine(RegenerateStaminaCoroutine());
        }
        else
        {
            // Si no hay suficiente estamina, muestra un mensaje en el log y detiene el sprint.
            Debug.Log("No tenemos Stamina");
            FindObjectOfType<PlayerController>().isSprinting = false;
        }
    }

    // Corutina que maneja la disminuci�n de la estamina.
    private IEnumerator LosingStaminaCoroutine(float amount)
    {
        // Mientras haya estamina (o hasta llegar a 0), se va decrementando la estamina.
        while (currentStamina >= 0)
        {
            currentStamina -= amount; // Disminuye la estamina por el valor dado.
            staminaSlider.value = currentStamina; // Actualiza la barra de estamina visualmente.

            // Espera un tiempo antes de continuar con la siguiente disminuci�n.
            yield return new WaitForSeconds(losingStaminaTime);
        }
        // Una vez que la corutina termina (sin m�s estamina), se asegura que el sprint se detenga.
        myCoroutineLosing = null;
        FindObjectOfType<PlayerController>().isSprinting = false;
    }

    // Corutina que maneja la regeneraci�n de la estamina.
    private IEnumerator RegenerateStaminaCoroutine()
    {
        // Espera un segundo antes de comenzar a regenerar.
        yield return new WaitForSeconds(1);

        // Mientras la estamina sea menor que la m�xima, la regeneramos poco a poco.
        while (currentStamina < maxStamina)
        {
            currentStamina += regenerateAmount; // Aumenta la estamina por la cantidad definida.
            staminaSlider.value = currentStamina; // Actualiza la barra de estamina visualmente.

            // Espera un tiempo antes de regenerar la siguiente cantidad de estamina.
            yield return new WaitForSeconds(regenerateStaminaTime);
        }
        // Una vez que la regeneraci�n termina (cuando se llega al m�ximo de estamina), se limpia la corutina.
        myCoroutineRegenerate = null;
    }
}
