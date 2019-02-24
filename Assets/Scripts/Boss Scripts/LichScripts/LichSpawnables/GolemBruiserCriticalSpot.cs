using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBruiserCriticalSpot : MonoBehaviour {

    private GolemBruiser parentGolem;
    // Use this for initialization
    void Start()
    {
        parentGolem = gameObject.transform.parent.gameObject.GetComponent<GolemBruiser>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
           parentGolem.golemHealth -= collision.gameObject.GetComponent<ProjectileDamage>().projectileDamage;
        }
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().DamagePlayer(parentGolem.chargeDamage);
        }
    }
}
