using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public float damage = 20f; // Da�o que causa el arma. Ajustable desde el Inspector.

    // M�todo para obtener el da�o del arma.
    public float GetDamage()
    {
        return damage;
    }
}
