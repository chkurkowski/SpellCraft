using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorb : MonoBehaviour {

    public PlayerHealth health;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "EnemyProjectile")
        {
            //gameObject.GetComponentInParent<PlayerHealth>()
            //Heal the player
        }
    }
}
