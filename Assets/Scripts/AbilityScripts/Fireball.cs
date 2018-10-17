using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fireball : MonoBehaviour 
{


    
	
    public int damage = 5;
 
	
    // Use this for initialization
    public void OnTriggerEnter2D(Collider2D col)
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
