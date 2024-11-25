using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text ammoText; // Referencia al componente Text que muestra la cantidad de munición en pantalla.

    public Image healthImage; // Referencia a la barra de salud que se actualizará visualmente.
    public float maxHealth; // Valor máximo de salud del jugador.
    public float currendHealth; // Salud actual del jugador.

    public static GameManager Instance { get; private set; } // Instancia estática para acceder al GameManager desde otros scripts.

    public int gunAmmo = 14; // Cantidad inicial de munición del arma.

    private void Awake()
    {
        Instance = this; // Asigna la instancia estática del GameManager a este objeto.
    }

    private void Start()
    {
        // Verifica que la salud actual sea válida al inicio.
        if (currendHealth <= 0 || currendHealth > maxHealth)
        {
            currendHealth = maxHealth; // Restablece a la salud máxima si la salud actual es inválida.
        }

        // Actualiza la barra de salud para reflejar el estado inicial.
        healthImage.fillAmount = currendHealth / maxHealth; // Rellena la barra de salud en base a la salud actual y máxima.
    }

    private void Update()
    {
        ammoText.text = gunAmmo.ToString(); // Actualiza el texto que muestra la cantidad de munición disponible.

        if (Input.GetKeyDown(KeyCode.I)) reduceHealth(); // Si se presiona la tecla 'I', se reduce la salud del jugador.
    }

    private void reduceHealth()
    {
        currendHealth -= 1; // Reduce la salud en 1.
        if (currendHealth < 0) currendHealth = 0; // Asegura que la salud no sea negativa.

        healthImage.fillAmount = currendHealth / maxHealth; // Actualiza la barra de salud en función de la nueva salud.
    }
}
