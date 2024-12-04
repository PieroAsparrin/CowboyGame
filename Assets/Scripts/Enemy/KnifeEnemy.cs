using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KnifeEnemy : MonoBehaviour
{
    private NavMeshAgent navMeshAgent; // Referencia al NavMeshAgent.
    private Transform playerTransform; // Transform del jugador.
    private bool hasSpottedPlayer = false; // Bandera para saber si el enemigo ya detectó al jugador.

    public float detectionRange = 5f; // Distancia para detectar al jugador.
    public Animator ani; // Referencia al Animator.

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>(); // Inicializa el Animator.
        playerTransform = FindAnyObjectByType<PlayerController>().transform; // Encuentra el Transform del jugador.
        
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= detectionRange || hasSpottedPlayer)
        {
            // Si está dentro del rango o ya lo detectó, lo persigue.
            hasSpottedPlayer = true; // Marca que el jugador ha sido detectado.

            // Gira hacia el jugador.
            Vector3 directionToPlayer = playerTransform.position - transform.position;
            directionToPlayer.y = 0; // Evita rotaciones en el eje Y.
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200 * Time.deltaTime);

            // Persigue al jugador.
            navMeshAgent.destination = playerTransform.position;

            // Animación de correr.
            ani.SetBool("run", true);
        }
        else
        {
            // Si no ha detectado al jugador y está fuera del rango, se detiene.
            navMeshAgent.destination = transform.position;
            ani.SetBool("run", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {

        }
    }

}
