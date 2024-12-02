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

    public void FinalAni()
    {
        ani.SetBool("attack", false);
    }

    // Método para recibir daño.
    public void TakeDamage(float damage)
    {
        if (health <= 0) return;

        health -= damage;

        if (health <= 0)
        {
            health = 0;
            StartCoroutine(Die());
        }
    }

    // Corrutina para manejar la muerte y destruir el objeto.
    private IEnumerator Die()
    {
        ani.SetBool("death", true); // Reproduce la animación de muerte.
        yield return new WaitForSeconds(ani.GetCurrentAnimatorStateInfo(0).length); // Espera a que termine la animación.
        Destroy(gameObject); // Destruye el objeto.
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