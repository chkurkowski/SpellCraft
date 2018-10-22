using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallScript : MonoBehaviour {

    private RespawnManager respawnManagerInfo;

    private void Start()
    {
        respawnManagerInfo = GameObject.Find("RespawnManager").GetComponent<RespawnManager>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision Happened!");
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Fire Wall Collision Happened!");
            respawnManagerInfo.KillPlayer();
        }
       
    }
}
