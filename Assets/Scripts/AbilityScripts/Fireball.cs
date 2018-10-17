using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

<<<<<<< HEAD

    
	
    public int damage = 5;
 
	
    // Use this for initialization
    public void OnTriggerEnter2D(Collider2D col)
=======
	// Use this for initialization
    private void OnTriggerEnter2D(Collider2D col)
>>>>>>> parent of a7640f5... Merge pull request #5 from zeke1498/health-bar-branch
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
