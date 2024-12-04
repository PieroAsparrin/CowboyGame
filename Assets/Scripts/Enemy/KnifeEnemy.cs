using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class KnifeEnemy : MonoBehaviour
{
    private NavMeshAgent navMeshAgent; // Referencia al NavMeshAgent.
    private Transform playerTransform; // Transform del jugador.
    private bool isAttacking = false; // Bandera para saber si el enemigo está atacando.

    public float detectionRange = 5f; // Distancia para detectar al jugador.
    public float attackRange = 1f; // Distancia mínima para atacar al jugador.
    public Animator ani; // Referencia al Animator.

    public float health = 100f; // Vida inicial del enemigo.

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        playerTransform = FindAnyObjectByType<PlayerController>()?.transform;

        if (playerTransform == null)
        {
            Debug.LogError("PlayerController not found in the scene.");
        }
    }

    void Update()
    {
        if (health <= 0) return; // No realizar acciones si está muerto.

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > detectionRange)
        {
            Idle();
        }
        else if (distanceToPlayer > attackRange)
        {
            ChasePlayer();
        }
        else
        {
            AttackPlayer();
        }
    }

    private void Idle()
    {
        // El enemigo está quieto.
        navMeshAgent.isStopped = true;
        ani.SetBool("run", false);
        ani.SetBool("attack", false);
    }

    private void ChasePlayer()
    {
        // El enemigo persigue al jugador.
        navMeshAgent.isStopped = false;
        navMeshAgent.destination = playerTransform.position;

        ani.SetBool("run", true);
        ani.SetBool("attack", false);
    }

    private void AttackPlayer()
    {
        // El enemigo ataca si está en rango.
        if (isAttacking) return; // Evita múltiples ataques al mismo tiempo.

        navMeshAgent.isStopped = true; // Detener el movimiento.
        ani.SetBool("run", false);
        ani.SetBool("attack", true);

        isAttacking = true;
        StartCoroutine(ResetAttack()); // Reinicia el ataque después de un tiempo.
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(1f); // Tiempo entre ataques.
        isAttacking = false;
    }

    public void TakeDamage(float damage)
    {
        if (health <= 0) return;

        health -= damage;
        Debug.Log($"Enemy Health: {health}");

        if (health <= 0)
        {
            health = 0;
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        navMeshAgent.isStopped = true;
        ani.SetBool("death", true);

        // Esperar a que la animación de muerte termine.
        while (!ani.GetCurrentAnimatorStateInfo(0).IsTag("Death"))
        {
            yield return null;
        }

        yield return new WaitForSeconds(ani.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
