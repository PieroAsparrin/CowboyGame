using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 25; // Da�o que hace la bala al impactar con un enemigo.

    // Este m�todo se ejecuta cuando la bala colisiona con otro objeto.
    private void OnCollisionEnter(Collision collision)
    {
        // Comprobamos si el objeto con el que colision� tiene la etiqueta "Enemy".
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Busca el componente HerraticEnemy para acceder a la vida.
            HerraticEnemy enemy = collision.gameObject.GetComponent<HerraticEnemy>();
            if (enemy != null)
            {
                // Resta el da�o de la bala a la vida del enemigo.
                enemy.TakeDamage(damage);
                Debug.Log($"Hit an enemy! Damage dealt: {damage}");
            }

            // Imprime en la consola que un enemigo ha sido alcanzado y muestra el da�o realizado.
            Debug.Log($"Hit an enemy! Damage dealt: {damage}");
        }

        // Destruye la bala despu�s de 1 segundo.
        Destroy(this.gameObject, 1f);
    }
}
