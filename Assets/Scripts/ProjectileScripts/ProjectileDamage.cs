using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public float projectileDamage;
    public float projectileHealth = 5f;

    private void Update()
    {
        if(projectileHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}
