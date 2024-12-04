using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 25f; // Daño que hace la bala al impactar con un enemigo.

    // Este método se ejecuta cuando la bala colisiona con otro objeto.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Bullet hit an enemy.");
            HerraticEnemy enemy = collision.gameObject.GetComponent<HerraticEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(this.gameObject, 1f);
        }
    }


    public float GetDamage()
    {
        return damage;
    }

}
