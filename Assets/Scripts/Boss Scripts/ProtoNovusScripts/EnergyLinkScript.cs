using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyLinkScript : MonoBehaviour
{
    public float linkDamage = 10f;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            InvokeRepeating("DamagePlayer", 0, 1);
        }
        if(col.gameObject.tag == "Projectile")
        {
           // Debug.Log("ENTIRE BOMB ATTACK SHOULD HAVE BEEN DESTROYED!");
            Destroy(gameObject.transform.parent.parent.gameObject);
        }
    }

    public void DamagePlayer()
    {
        GameObject.Find("Player").GetComponent<PlayerHealth>().DamagePlayer(linkDamage);
    }
}
