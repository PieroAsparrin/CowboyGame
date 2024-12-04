using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public float damage = 20f; // Daño que causa el arma. Ajustable desde el Inspector.

    // Método para obtener el daño del arma.
    public float GetDamage()
    {
        return damage;
    }
}
