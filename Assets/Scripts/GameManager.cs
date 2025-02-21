using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text gunAmmoText;// Referencia al componente Text para la cantidad de munición.
    public Text bagAmmoText;// Referencia al componente Texte para la cantidad de munición en la bolsa.

    public Slider healthSlider;// Referencia al Slider que representa la salud del jugador.
    private float maxHealth = 100f;// Salud máxima del jugador.
    public float currentHealth;// Salud actual del jugador.

    public static GameManager Instance { get; private set; } // Instancia estática del GameManager.

    public int gunAmmo = 7; // Cantidad de balas en el cargador (máximo por cargador).
    public int bagAmmo = 50; // Cantidad de balas totales en la bolsa.
    private int maxGunAmmo = 7; // Capacidad máxima del cargador.

    private void Awake()
    {
        Instance = this; // Asigna esta instancia como la instancia estática.
    }

    private void Start()
    {
        if (currentHealth <= 0 || currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Establece la salud inicial al máximo.
        }

        healthSlider.maxValue = maxHealth; // Configura el valor máximo del Slider.
        healthSlider.value = currentHealth; // Configura el valor inicial del Slider.

        UpdateAmmoText(); // Actualiza el texto inicial de munición.
    }

    private void Update()
    {
        gunAmmoText.text = gunAmmo.ToString() + " I ";
        bagAmmoText.text = "  " + bagAmmo.ToString();

        // Controles para daño y curación.
        if (Input.GetKeyDown(KeyCode.I)) TakeDamage(10f); // Reduce salud en 10 al presionar 'I'.
        if (Input.GetKeyDown(KeyCode.K)) Heal(10f);       // Aumenta salud en 10 al presionar 'K'.

        // Recarga manual al presionar 'R'.
        if (Input.GetKeyDown(KeyCode.R)) Reload();
        // Comprueba si el cargador está vacío para recargar automáticamente.
        if (gunAmmo <= 0)
        {
            AutoReload();
        }
    }

    // Método para reducir la salud.
    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0) return; // Si ya está a 0, no hacer nada.

        currentHealth -= damage; // Reduce la salud.
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Asegura que no sea menor a 0.

        UpdateHealthBar(); // Actualiza el Slider de salud.

        if (currentHealth <= 0)
        {
            HandlePlayerDeath(); // Llama a la función para manejar la muerte del jugador.
        }
    }

    // Método para curar.
    public void Heal(float amount)
    {
        if (currentHealth >= maxHealth) return; // Si ya está al máximo, no hacer nada.

        currentHealth += amount; // Aumenta la salud.
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Asegura que no exceda el máximo.

        UpdateHealthBar(); // Actualiza el Slider de salud.
    }

    // Método para actualizar la barra de salud visual.
    private void UpdateHealthBar()
    {
        healthSlider.value = currentHealth; // Actualiza el valor del Slider según la salud actual.
    }

    // Método para recargar manualmente el arma.
    public void Reload()
    {
        int bulletsNeeded = maxGunAmmo - gunAmmo; // Calcula las balas que faltan para llenar el cargador.
        int bulletsToReload = Mathf.Min(bulletsNeeded, bagAmmo); // Calcula cuántas balas tomar de la bolsa.

        gunAmmo += bulletsToReload; // Agrega las balas recargadas al cargador.
        bagAmmo -= bulletsToReload; // Resta las balas recargadas de la bolsa.

        UpdateAmmoText(); // Actualiza el texto de munición.
    }

    // Método para recargar automáticamente cuando el cargador está vacío.
    private void AutoReload()
    {
        if (bagAmmo > 0) // Solo recarga si hay balas en la bolsa.
        {
            Reload();
        }
    }

    // Método para actualizar el texto de munición.
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

        // Aquí puedes añadir otras acciones como mostrar una pantalla de Game Over o reiniciar el nivel.
    }
}
