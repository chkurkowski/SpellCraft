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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "EnemyProjectile")
        {

        }
    }
}
