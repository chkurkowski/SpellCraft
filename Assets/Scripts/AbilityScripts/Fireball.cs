using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {
	
    public int damage = 5;
 
	// Use this for initialization
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Boss" && gameObject.tag == "Projectile")
        {
            //Do damage
            print("Hit: 5 damage");
            Destroy(gameObject);
        }
        else if(col.gameObject.tag != "Player" && gameObject.tag != "EnemyProjectile")
            Destroy(gameObject);

    }
}
