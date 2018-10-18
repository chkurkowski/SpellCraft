using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorb : MonoBehaviour {

    public PlayerHealth health;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "EnemyProjectile")
        {
            float healAmount = col.GetComponent<Fireball>().fireBallDamage;
            gameObject.GetComponentInParent<PlayerHealth>().HealPlayer(healAmount);
            //Heal the player
        }
    }
}
