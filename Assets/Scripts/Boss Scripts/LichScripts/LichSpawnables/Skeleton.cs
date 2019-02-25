using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour, IPooledObject
{
    public float skeletonDamage = 1f;
    public float moveSpeed = 20f;

    // Use this for initialization
    public void OnObjectSpawn()
    {
   
    }

    // Update is called once per frame
    void Update ()
    {
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed * -1);
	}



    public void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Reflect" || trig.gameObject.tag == "Environment" || trig.gameObject.tag == "Boss") 
        {
            gameObject.SetActive(false);
        }
        if(trig.gameObject.tag == "Player")
        {
            trig.gameObject.GetComponent<PlayerHealth>().DamagePlayer(skeletonDamage);
            gameObject.SetActive(false);//this is where animation for skeleton explode goes
        }
        if(trig.gameObject.tag == "Projectile")
        {
            trig.gameObject.GetComponent<ProjectileDamage>().projectileHealth -= skeletonDamage;
            gameObject.SetActive(false);//this is where animation for skeleton explode goes
        }

        if (trig.gameObject.tag == "CameraTrigger" || trig.gameObject.tag == "EnemyReflect" || trig.gameObject.tag == "Portal" || trig.gameObject.tag == "Skeleton")
        {
            //do nothing
        }

        if(trig.gameObject.tag == "EnemyProjectile")
        {
            gameObject.SetActive(false);
        }
    }

  
}
