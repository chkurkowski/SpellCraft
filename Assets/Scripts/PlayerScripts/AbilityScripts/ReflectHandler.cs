using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectHandler : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "EnemyProjectile")
        {
        	print("Hit");
            PlayerAbilities.instance.AddToResource(.025f);
        }
    }
}
