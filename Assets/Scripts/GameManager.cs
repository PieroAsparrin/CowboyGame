using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text ammoText;

    public Image healthImage;
    public float maxHealth;
    public float currendHealth;

    public static GameManager Instance { get; private set; }

    public int gunAmmo= 14;

    private void Awake()
    {
        Instance = this;    
    }

    private void Start()
    {
        // Verifica que la salud actual sea válida al inicio
        if (currendHealth <= 0 || currendHealth > maxHealth)
        {
            currendHealth = maxHealth; // Restablece a la salud máxima si es inválida
        }

        // Actualiza la barra de salud para reflejar el estado inicial
        healthImage.fillAmount = currendHealth / maxHealth;
    }

    private void Update()
    {
        ammoText.text = gunAmmo.ToString();

        if (Input.GetKeyDown(KeyCode.I)) reduceHealth();
    }

    private void reduceHealth()
    {
        currendHealth -= 1;
        if (currendHealth < 0) currendHealth = 0;
        healthImage.fillAmount = currendHealth / maxHealth;
    }
}
