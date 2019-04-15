using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectHandler : MonoBehaviour {

	private float health = 100f;

	private void OnTriggerEnter2D(Collider2D col)
    {
    	print("Hit: " + col.gameObject.tag);
        if(col.gameObject.tag == "EnemyProjectile")
        {
        	print("Hit EnemyProjectile");
            PlayerAbilities.instance.AddToResource(.025f);
        }
    }

    public void SubtractHealth(float i)
    {
    	PlayerAbilities.instance.handlers.reflectHealth -= i;

    	if(PlayerAbilities.instance.handlers.reflectHealth < 0)
        {
        	PlayerAbilities.instance.handlers.ReflectBroken();
        	// PlayerAbilities.instance.handlers.reflectHealth = 100;
        }

        // print(PlayerAbilities.instance.handlers.reflectHealth);
    }
}
