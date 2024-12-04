using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerraticEnemy : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator ani;
    public Quaternion angulo;
    public float grado;

    public GameObject target;
    public bool atacando;

    public float health = 100f; // Vida inicial del enemigo.

    void Start()
    {
        ani = GetComponent<Animator>();
        target = GameObject.Find("Player");
    }

    void Update()
    {
        if (health > 0)
        {
            ComportamientoEnemigo();
        }
        else
        {

        }
    }

    public void ComportamientoEnemigo()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 5)
        {
            ani.SetBool("run", false);
            cronometro += 1 * Time.deltaTime;

            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }

            switch (rutina)
            {
                case 0:
                    ani.SetBool("walk", false);
                    break;

                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;

                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                    ani.SetBool("walk", true);
                    break;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) > 1)
            {
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);

                ani.SetBool("walk", false);
                ani.SetBool("run", true);
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);

                ani.SetBool("attack", false);
            }
            else
            {
                ani.SetBool("walk", false);
                ani.SetBool("run", false);

                ani.SetBool("attack", true);
                atacando = true;
            }
        }
    }

    public void Final_Ani()
    {
        ani.SetBool("attack", false);
        atacando = false;
    }

    /*
    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Enemy collided with {other.gameObject.name}");

        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Enemy hit by a bullet.");
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.GetDamage());
            }
        }
    }
    */

    // Método para recibir daño.
    public void TakeDamage(float damage)
    {
        Debug.Log($"Current Health: {health}");

        if (health <= 0) return;

        health -= damage;
        Debug.Log($"HEALTH: {health}");

        if (health <= 0)
        {
            health = 0;
            StartCoroutine(Die());
        }
    }


    // Corrutina para manejar la muerte y destruir el objeto.
    private IEnumerator Die()
    {
        ani.SetBool("death", true); // Inicia la animación de muerte.

        // Espera hasta que la animación de muerte esté realmente activa.
        while (!ani.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            yield return null; // Espera un frame antes de volver a comprobar.
        }

        // Una vez activa, espera a que termine.
        yield return new WaitForSeconds(ani.GetCurrentAnimatorStateInfo(0).length);

        Debug.Log("¡ENEMY DESTROYED!");
        Destroy(gameObject); // Destruye el enemigo después de la animación.
    }
}
/*
EXAMEN
Mis cambios
control de versiones
gdd
gameplay: Movimiento, tipo de camara
meta
 */ 