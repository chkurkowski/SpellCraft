using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public float spawnFrequency = 1f;
    public bool flipBullets = false;
    private int bulletFlipper = 0;
    public GameObject bullet1;
    public GameObject bullet2;

	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("SpawnBullet", 0, spawnFrequency);
	}
	
    public void SpawnBullet()
    {
        if(!flipBullets)
        {
            Instantiate(bullet1, transform.position, transform.rotation);
        }
        else if(flipBullets)
        {
            if(bulletFlipper == 0)
            {
                Instantiate(bullet1, transform.position, transform.rotation);
                bulletFlipper++;
            }
            else if(bulletFlipper == 1)
            {
                Instantiate(bullet2, transform.position, transform.rotation);
                bulletFlipper--;
            }
        }
        
    }

}
