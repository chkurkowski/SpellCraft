using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    public GameObject fireBall;
    public float turretHealth = 1f;
	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("FireProjectile", 0, 1);
	}
	
    private void FireProjectile()
    {

       GameObject spawnedFireBall = Instantiate(fireBall, new Vector2(transform.position.x, transform.position.y - 10), Quaternion.identity);
        spawnedFireBall.transform.Rotate(0,0,180);
    }

	// Update is called once per frame
	void Update ()
    {
		if(turretHealth <= 0)
        {
            CancelInvoke("FireProjectile");
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D trig)
    {
        Debug.Log("Anything happened");
        if (trig.gameObject.tag == "Projectile" && trig.gameObject.name == "Fireball(Clone)")
        {
            Debug.Log("This shit happened");
            turretHealth--;
        }
    }
}
