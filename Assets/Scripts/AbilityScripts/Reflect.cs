using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : MonoBehaviour {

    public PlayerAbilities abilities;

    // Use this for initialization
    private void Start()
    {
        abilities = FindObjectOfType<PlayerAbilities>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "EnemyProjectile")
        {
            //print(col.gameObject.GetComponent<Rigidbody2D>().velocity);
            // col.gameObject.GetComponent<Rigidbody2D>().velocity = -col.gameObject.GetComponent<Rigidbody2D>().velocity;
            //col.gameObject.GetComponent<Fireball>().fireBallSpeed *= -1;
        }
    }
}
