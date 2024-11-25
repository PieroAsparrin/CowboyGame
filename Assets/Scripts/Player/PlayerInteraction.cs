using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Este m�todo se ejecuta cuando el jugador entra en el �rea de un trigger (colisionador sin f�sicas)
    private void OnTriggerEnter(Collider other)
    {
        // Comprobamos si el objeto con el que el jugador ha colisionado tiene la etiqueta "GunAmmo"
        if (other.gameObject.CompareTag("GunAmmo"))
        {
            // Si el objeto es de tipo "GunAmmo", sumamos la cantidad de munici�n que tiene el objeto
            // al total de munici�n en el GameManager. Este valor lo obtiene del script 'AmmoBox'.
            GameManager.Instance.gunAmmo += other.gameObject.GetComponent<AmmoBox>().ammo;

            // Despu�s de recoger la munici�n, destruimos el objeto de munici�n (ya no es necesario)
            Destroy(other.gameObject);
        }
    }
}
