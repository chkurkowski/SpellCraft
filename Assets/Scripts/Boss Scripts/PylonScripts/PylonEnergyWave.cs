using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonEnergyWave : MonoBehaviour {
    public float moveSpeed = 40f;
    public float energyDamage = 20f;
    private GameObject player;
 
    // Use this for initialization

	
	// Update is called once per frame
	void FixedUpdate ()
    {
        transform.Translate(Vector2.up * Time.deltaTime * moveSpeed);
    }

    public void OnTriggerEnter2D(Collider2D trig)
    {
       // Debug.Log("Energy Blade Detected a collision");
        if(trig.gameObject.tag == "Player")
        {
          //  Debug.Log("Energy Blade Detected a player");
            player = trig.gameObject;
            InvokeRepeating("DamagePlayer", 0, 1);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            CancelInvoke();
            player = null;
        }
    }

    public void DamagePlayer()
    {
        player.GetComponent<PlayerHealth>().DamagePlayer(energyDamage);
           
    }
}
