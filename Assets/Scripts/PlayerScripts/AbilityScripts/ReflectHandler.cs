using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectHandler : MonoBehaviour {

	private float health = 100f;

	private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "EnemyProjectile")
        {
            PlayerAbilities.instance.AddToResource(.025f);

            print(col.gameObject.name);

            switch(col.gameObject.name)
            {
            	case "PylonLaserShard(Clone)":
            		health -= 1;
            		break;
        		case "Fireball(Clone)":
            		health -= 5;
            		break;
        		case "EnergyWave(Clone)":
            		health -= 20;
            		break;
            }

            print(health);
        }

        if(health < 0)
        	PlayerAbilities.instance.handlers.CancelReflect();
    }
}
