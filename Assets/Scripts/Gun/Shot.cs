using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public Transform spawnPoint; // El punto desde donde sale el disparo.
    public GameObject bullet; // La bala a instanciar y disparar.
    public float shotForce = 5000f; // Fuerza del disparo (ajustada a un valor m�s razonable).
    public float shotRate = 1.2f; // Tasa de disparo (segundos entre disparos).

    private float shotRateTime = 0; // Tiempo del siguiente disparo.

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Verifica si el jugador presiona el bot�n de disparo.
        {
            TryShoot(); // Intenta disparar si es posible.
        }
    }

    private void TryShoot()
    {
        // Verifica si es tiempo de disparar y si hay munici�n.
        if (Time.time > shotRateTime && GameManager.Instance.gunAmmo > 0)
        {
            Shoot(); // Llama al m�todo para disparar.
        }
    }

    private void Shoot()
    {
        GameManager.Instance.gunAmmo--; // Resta munici�n.

        // Instancia la nueva bala.
        GameObject newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);

        // Aplica la fuerza al rigidbody de la bala.
        newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce);

        // Establece el tiempo del pr�ximo disparo.
        shotRateTime = Time.time + shotRate;

        // Destruye la bala despu�s de 7 segundos.
        Destroy(newBullet, 7);
    }
}
