using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour {

    public GameObject iceball;

    private float MAXTIMER = 2;
    private float timer = 0;
    private float atkSpeed = 50;
	
	// Update is called once per frame
	void Update () 
    {
        timer += Time.deltaTime;

        if(timer >= MAXTIMER)
        {
            GameObject fb = Instantiate(iceball, transform.position, Quaternion.identity);
            fb.GetComponent<Rigidbody2D>().velocity = Vector2.down * atkSpeed;
            timer = 0;
        }
	}
}
