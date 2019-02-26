using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfernalSoul : MonoBehaviour, IPooledObject
{
    public GameObject explosion;
    public float infernalSoulHealth = 1.5f;
    public float infernalSoulDamage = 5f;
    public float moveSpeed = 40f;


    public void OnObjectSpawn()
    {
        
    }

    // Update is called once per frame
    void Update ()
    {
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
        if (infernalSoulHealth <= 0)
        {
            Instantiate(explosion);
            gameObject.SetActive(false);
        }
    }

    public void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }


    public void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Reflect" || trig.gameObject.tag == "Environment" || trig.gameObject.tag == "EnemyProjectile") 
        {
            Explode();
        }
        if (trig.gameObject.tag == "Player")
        {
            Explode();
        }
        if (trig.gameObject.tag == "Projectile")
        {
            Explode();
        }
        if (trig.gameObject.tag == "CameraTrigger" || trig.gameObject.tag == "EnemyReflect" || trig.gameObject.tag == "Portal" || trig.gameObject.tag == "Boss")
        {
            //do nothing
        }
        if (trig.gameObject.tag == "Skeleton")
        {
            trig.gameObject.SetActive(false);
        }
      
    }



}
