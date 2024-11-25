using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaSlider; // Componente Slider que representa la barra de estamina visualmente.
    public float maxStamina = 100f; // Máximo valor de la estamina (cuando está al máximo).
    private float currentStamina; // Valor actual de la estamina, cambia conforme se usa o se regenera.

    private float regenerateStaminaTime = 0.1f; // Tiempo entre regeneración de cada unidad de estamina.
    private float regenerateAmount = 2f; // Cantidad de estamina que se regenera por cada intervalo.

    public float losingStaminaTime = 0.1f; // Tiempo entre cada decremento de estamina al usarla.

    private Coroutine myCoroutineLosing; // Referencia a la corrutina que maneja la pérdida de estamina.
    private Coroutine myCoroutineRegenerate; // Referencia a la corrutina que maneja la regeneración de estamina.

    // Método Start se llama una vez al inicio del juego
    void Start()
    {
        currentStamina = maxStamina; // Inicializa la estamina al máximo cuando empieza el juego.
        staminaSlider.maxValue = maxStamina; // Establece el valor máximo del slider de estamina.
        staminaSlider.value = maxStamina; // Inicializa la barra de estamina al valor máximo.
    }

    // Update es llamado una vez por frame, pero en este caso no se está utilizando.
    void Update()
    {
        // No se necesita código en el Update en este caso, ya que todo se maneja mediante corutinas.
    }

    // Este método es llamado para restar una cantidad de estamina cuando se usa.
    public void UseStamina(float amount)
    {
        // Si la estamina actual es mayor que la cantidad que se quiere usar
        if (currentStamina - amount > 0)
        {
            // Si hay una corutina de pérdida de estamina en curso, se detiene.
            if (myCoroutineLosing != null)
            {
                StopCoroutine(myCoroutineLosing);
            }
            // Inicia la corutina para perder estamina con el valor indicado.
            myCoroutineLosing = StartCoroutine(LosingStaminaCoroutine(amount));

            // Si hay una corutina de regeneración de estamina en curso, se detiene.
            if (myCoroutineRegenerate != null)
            {
                StopCoroutine(myCoroutineRegenerate);
            }
            // Inicia la corutina para regenerar la estamina después de haber usado estamina.
            myCoroutineRegenerate = StartCoroutine(RegenerateStaminaCoroutine());
        }
        else
        {
            // Si no hay suficiente estamina, muestra un mensaje en el log y detiene el sprint.
            Debug.Log("No tenemos Stamina");
            FindObjectOfType<PlayerController>().isSprinting = false;
        }
    }

    // Corutina que maneja la disminución de la estamina.
    private IEnumerator LosingStaminaCoroutine(float amount)
    {
        // Mientras haya estamina (o hasta llegar a 0), se va decrementando la estamina.
        while (currentStamina >= 0)
        {
            currentStamina -= amount; // Disminuye la estamina por el valor dado.
            staminaSlider.value = currentStamina; // Actualiza la barra de estamina visualmente.

            // Espera un tiempo antes de continuar con la siguiente disminución.
            yield return new WaitForSeconds(losingStaminaTime);
        }
        // Una vez que la corutina termina (sin más estamina), se asegura que el sprint se detenga.
        myCoroutineLosing = null;
        FindObjectOfType<PlayerController>().isSprinting = false;
    }

    // Corutina que maneja la regeneración de la estamina.
    private IEnumerator RegenerateStaminaCoroutine()
    {
        // Espera un segundo antes de comenzar a regenerar.
        yield return new WaitForSeconds(1);

        // Mientras la estamina sea menor que la máxima, la regeneramos poco a poco.
        while (currentStamina < maxStamina)
        {
            currentStamina += regenerateAmount; // Aumenta la estamina por la cantidad definida.
            staminaSlider.value = currentStamina; // Actualiza la barra de estamina visualmente.

            // Espera un tiempo antes de regenerar la siguiente cantidad de estamina.
            yield return new WaitForSeconds(regenerateStaminaTime);
        }
        // Una vez que la regeneración termina (cuando se llega al máximo de estamina), se limpia la corutina.
        myCoroutineRegenerate = null;
    }
}
