using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorb : MonoBehaviour {

    public PlayerHealth health;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "EnemyProjectile")
        {
            //float healAmount = col.GetComponent<Fireball>().fireBallDamage;
            //GameObject.Find("Player").GetComponent<PlayerHealth>().HealPlayer(healAmount);
           // GameObject.Find("Player").GetComponent<PlayerHealth>().playerHealth += 5;
           // Destroy(col.gameObject);
            //Heal the player
        }
    }
}
