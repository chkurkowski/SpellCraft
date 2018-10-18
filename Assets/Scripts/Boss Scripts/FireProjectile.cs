using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour 
{
    //this shoots the fireballs
    public GameObject fireball;

    private float MAXTIMER = 2;
    private float timer = 0;
    private float atkSpeed = 50;
	
	// Update is called once per frame
	/*void Update () 
    {
        timer += Time.deltaTime;

        if(timer >= MAXTIMER)
        {
            GameObject fb = Instantiate(fireball, transform.position + Vector3.down * 10 , Quaternion.identity);
            fb.GetComponent<Rigidbody2D>().velocity = Vector2.down * atkSpeed;
            timer = 0;
        }
	}*/
}
