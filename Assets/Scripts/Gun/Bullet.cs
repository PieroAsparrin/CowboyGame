using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 20; // Daño que hace la bala al impactar con un enemigo

    // Este método se ejecuta cuando la bala colisiona con otro objeto
    private void OnCollisionEnter(Collision collision)
    {
        // Comprobamos si el objeto con el que colisionó tiene la etiqueta "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Si colisiona con un enemigo, destruye ese objeto (el enemigo muere)
            Destroy(collision.gameObject);
            // Imprime en la consola que un enemigo ha sido alcanzado y muestra el daño realizado
            Debug.Log($"Hit an enemy! Damage dealt: {damage}");
        }
        // Destruye la bala después de 1 segundo (1f)
        Destroy(this.gameObject, 1f);
    }

    // El método Update no tiene ninguna implementación en este caso, pero se mantiene en el código
    // por si quieres agregar funcionalidades adicionales en el futuro.
    void Update()
    {

    }
}
