using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonReflectShield : MonoBehaviour {
    public bool lookAtPlayer = false;
    private Transform playerLocation;
    public bool isLasered = false;
    public GameObject bomb;
   // public GameObject laser;
    // Use this for initialization
    void Start ()
    {
       // laser.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(lookAtPlayer)
        {
            playerLocation = GameObject.Find("Player").GetComponent<Transform>();
            Vector3 dir = playerLocation.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, transform.forward);
        }

        if (isLasered)
        {
            //laser.SetActive(true);
        }
        else if (!isLasered)
        {
            // Debug.Log("not lasered");
           // laser.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile" && collision.name != "PylonLaserShard(Clone)")
        {
            // Debug.Log("laserTriggerDetected");
            // isLasered = true;
            Instantiate(bomb, gameObject.transform.position, gameObject.transform.rotation);
        }
    }


}
