using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHealOrb : MonoBehaviour
{
    public GameObject novusBomb;
    public GameObject healOrb;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Projectile" || collision.tag == "Player")
        {
            Instantiate(healOrb, transform.position, transform.rotation);
            Destroy(novusBomb);
        }
    }
}
