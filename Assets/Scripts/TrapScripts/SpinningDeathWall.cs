using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningDeathWall : MonoBehaviour {
    public float spinSpeed;
    private RespawnManager respawnManagerInfo;
    // Use this for initialization
    void Start ()
    {
        respawnManagerInfo = GameObject.Find("RespawnManager").GetComponent<RespawnManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(0,0,spinSpeed);
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
