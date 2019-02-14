using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectLaser : MonoBehaviour
{
    public bool isLasered = false;
    public GameObject laser;
	// Use this for initialization
	void Start ()
    {
        laser.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(isLasered)
        {
            laser.SetActive(true);
        }
        else if(!isLasered)
        {
            // Debug.Log("not lasered");
            laser.SetActive(false);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "LaserEndPoint")
        {
           // Debug.Log("laserTriggerDetected");
            isLasered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "LaserEndPoint")
        {
           /// Debug.Log("laserTriggerDetected 2");
            isLasered = false;
        }
    }
}
