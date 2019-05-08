using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimScript : MonoBehaviour
{
    private RespawnManager respawnManagerInfo;
    private GameObject playerObject;

	// Use this for initialization
	void Start ()
    {
        playerObject = GameObject.Find("Player");
        respawnManagerInfo = GameObject.Find("RespawnManager").GetComponent<RespawnManager>();
    }
	
	
    public void RevivePlayer()
    {
       
        playerObject.GetComponent<SpriteRenderer>().enabled = true;
        playerObject.GetComponent<PlayerMovement>().canMove = true;
        playerObject.GetComponent<Collider2D>().enabled = true;
    }

    public void KillPlayer()
    {
        playerObject.GetComponent<SpriteRenderer>().enabled = false;
        playerObject.GetComponent<PlayerMovement>().canMove = false;
        playerObject.GetComponent<Collider2D>().enabled = false;
        GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);


    }

    public void RespawnPlayer()
    {
        respawnManagerInfo.KillPlayer();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
