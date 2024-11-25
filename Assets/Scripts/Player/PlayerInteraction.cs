using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Este método se ejecuta cuando el jugador entra en el área de un trigger (colisionador sin físicas)
    private void OnTriggerEnter(Collider other)
    {
        // Comprobamos si el objeto con el que el jugador ha colisionado tiene la etiqueta "GunAmmo"
        if (other.gameObject.CompareTag("GunAmmo"))
        {
            // Si el objeto es de tipo "GunAmmo", sumamos la cantidad de munición que tiene el objeto
            // al total de munición en el GameManager. Este valor lo obtiene del script 'AmmoBox'.
            GameManager.Instance.gunAmmo += other.gameObject.GetComponent<AmmoBox>().ammo;

            // Después de recoger la munición, destruimos el objeto de munición (ya no es necesario)
            Destroy(other.gameObject);
        }
    }
}
