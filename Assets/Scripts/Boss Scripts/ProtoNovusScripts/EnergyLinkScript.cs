using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyLinkScript : MonoBehaviour
{
    public float linkDamage = 10f;
    public float speedMultiplier = 2.5f;
    public float damageMultiplier = 1.5f;
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
            //  col.GetComponent<Rigidbody2D>().velocity *= speedMultiplier;
            // col.GetComponent<MagicBall>().magicBallDamage *= damageMultiplier;
            // PlayerAbilities.instance.AddToResource(.025f);
            // Destroy(gameObject.transform.parent.parent.gameObject);
            gameObject.transform.parent.parent.gameObject.GetComponent<NovusBombScript>().bombExploded = true; ;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            CancelInvoke();
        }
    }

    public void Cancel()
    {
        CancelInvoke();
        
    }

    public void DamagePlayer()
    {
        GameObject.Find("Player").GetComponent<PlayerHealth>().DamagePlayer(linkDamage);
    }
}
