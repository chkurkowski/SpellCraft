using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObstacleScript : MonoBehaviour {
    private float wallHealth = 1f;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(wallHealth <= 0f)
        {
            Destroy(gameObject);//set up this way if we ever want to add new sprites for each level of health(currently only 1);
        }
	}

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.tag == "Projectile")
        {
            wallHealth -= 1;
        }
    }
}

