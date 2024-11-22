using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Image staminaImage;
    public float maxStamina = 100f;
    private float currentStamina;

    private float regenerateStaminaTime = 1f;
    private float regenerateAmount = 2f;

    public float losingStaminaTime = 0.1f;
    void Start()
    {
        currentStamina = maxStamina;
        //staminaImage.fillAmount = maxStamina;//Necesito solucionar la stamina
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseStamina(float amount)
    {
        if (currentStamina - amount > 0)
        {
            StartCoroutine(LosingStaminaCoroutine(amount));

            StartCoroutine(RegenerateStaminaCoroutine());
        }
        else
        {
            Debug.Log("No tenemos Stamina");
        }
    }

    private IEnumerator LosingStaminaCoroutine(float amount)
    {
        while (currentStamina >= 0) 
        { 
        currentStamina-= amount;
            staminaImage.fillAmount = currentStamina;

            yield return new WaitForSeconds(losingStaminaTime);
        }

        FindObjectOfType<PlayerController>().isSprinting = false;
    }

    private IEnumerator RegenerateStaminaCoroutine()
    {
        yield return new WaitForSeconds(1);

        while (currentStamina < maxStamina) 
        { 
            currentStamina+= regenerateAmount;
            staminaImage.fillAmount= currentStamina;
            yield return new WaitForSeconds(regenerateStaminaTime);
        }

    }
}
