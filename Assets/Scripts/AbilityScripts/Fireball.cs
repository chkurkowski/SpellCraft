using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {
	
    public int fireBallDamage = 5;
 

	// Use this for initialization
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Boss" )
        {
            //Do damage
            print("Hit: 5 damage");
            Destroy(gameObject);
        }
        else if(col.gameObject.tag != "Player" && gameObject.tag != "Reflect")
        {

            Debug.Log("SHITS HAPPENING!");
            if(col.gameObject.tag != "Boss" || gameObject.tag != "CameraTrigger")
            {
                print(col.tag);
                Destroy(gameObject);
            }
          
        }
    }
}
