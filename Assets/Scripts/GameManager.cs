using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text gunAmmoText;// Referencia al componente Text para la cantidad de munici�n.
    public Text bagAmmoText;// Referencia al componente Texte para la cantidad de munici�n en la bolsa.

    public Slider healthSlider;// Referencia al Slider que representa la salud del jugador.
    private float maxHealth = 100f;// Salud m�xima del jugador.
    public float currentHealth;// Salud actual del jugador.

    public static GameManager Instance { get; private set; } // Instancia est�tica del GameManager.

    public int gunAmmo = 7; // Cantidad de balas en el cargador (m�ximo por cargador).
    public int bagAmmo = 50; // Cantidad de balas totales en la bolsa.
    private int maxGunAmmo = 7; // Capacidad m�xima del cargador.

    private void Awake()
    {
        Instance = this; // Asigna esta instancia como la instancia est�tica.
    }

    private void Start()
    {
        if (currentHealth <= 0 || currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Establece la salud inicial al m�ximo.
        }

        healthSlider.maxValue = maxHealth; // Configura el valor m�ximo del Slider.
        healthSlider.value = currentHealth; // Configura el valor inicial del Slider.

        UpdateAmmoText(); // Actualiza el texto inicial de munici�n.
    }

    private void Update()
    {
        gunAmmoText.text = gunAmmo.ToString() + " I ";
        bagAmmoText.text = "  " + bagAmmo.ToString();

        // Controles para da�o y curaci�n.
        if (Input.GetKeyDown(KeyCode.I)) TakeDamage(10f); // Reduce salud en 10 al presionar 'I'.
        if (Input.GetKeyDown(KeyCode.K)) Heal(10f);       // Aumenta salud en 10 al presionar 'K'.

        // Recarga manual al presionar 'R'.
        if (Input.GetKeyDown(KeyCode.R)) Reload();
        // Comprueba si el cargador est� vac�o para recargar autom�ticamente.
        if (gunAmmo <= 0)
        {
            AutoReload();
        }
    }

    // M�todo para reducir la salud.
    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0) return; // Si ya est� a 0, no hacer nada.

        currentHealth -= damage; // Reduce la salud.
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Asegura que no sea menor a 0.

        UpdateHealthBar(); // Actualiza el Slider de salud.

        if (currentHealth <= 0)
        {
            HandlePlayerDeath(); // Llama a la funci�n para manejar la muerte del jugador.
        }
    }

    // M�todo para curar.
    public void Heal(float amount)
    {
        if (currentHealth >= maxHealth) return; // Si ya est� al m�ximo, no hacer nada.

        currentHealth += amount; // Aumenta la salud.
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Asegura que no exceda el m�ximo.

        UpdateHealthBar(); // Actualiza el Slider de salud.
    }

    // M�todo para actualizar la barra de salud visual.
    private void UpdateHealthBar()
    {
        healthSlider.value = currentHealth; // Actualiza el valor del Slider seg�n la salud actual.
    }

    // M�todo para recargar manualmente el arma.
    public void Reload()
    {
        int bulletsNeeded = maxGunAmmo - gunAmmo; // Calcula las balas que faltan para llenar el cargador.
        int bulletsToReload = Mathf.Min(bulletsNeeded, bagAmmo); // Calcula cu�ntas balas tomar de la bolsa.

        gunAmmo += bulletsToReload; // Agrega las balas recargadas al cargador.
        bagAmmo -= bulletsToReload; // Resta las balas recargadas de la bolsa.

        UpdateAmmoText(); // Actualiza el texto de munici�n.
    }

    // M�todo para recargar autom�ticamente cuando el cargador est� vac�o.
    private void AutoReload()
    {
        if (bagAmmo > 0) // Solo recarga si hay balas en la bolsa.
        {
            Reload();
        }
    }

    // M�todo para actualizar el texto de munici�n.
    private void UpdateAmmoText()
    {
        gunAmmoText.text = gunAmmo.ToString() + " I ";
        bagAmmoText.text = "  "+bagAmmo.ToString();
    }

    private void HandlePlayerDeath()
    {
        // Buscar el objeto del jugador con el tag "Player" y destruirlo.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            SceneManager.LoadScene("Reiniciar");
        }

        // Aqu� puedes a�adir otras acciones como mostrar una pantalla de Game Over o reiniciar el nivel.
    }
}
