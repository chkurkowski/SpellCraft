using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fireball : MonoBehaviour 
{

<<<<<<< HEAD
    
	// Use this for initialization
    private void OnTriggerEnter2D(Collider2D col)
=======
    public int damage = 5;
 
	
    // Use this for initialization
    public void OnTriggerEnter2D(Collider2D col)
>>>>>>> a7640f54bb3a55c0e7df9aad063c0c8736cdd9d7
    {
        if(col.gameObject.tag == "Boss")
        {
            //Do damage
            print("Hit: 5 damage");
            Destroy(gameObject);
        }
        else if(col.gameObject.tag != "Player")
            Destroy(gameObject);

    }
}
