using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonLaserRotator : MonoBehaviour
{
    private PylonMovement pylonMovementInfo;
	// Use this for initialization
	void Start ()
    {
        pylonMovementInfo = GameObject.Find("Pylon").GetComponent<PylonMovement>();
	}


    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            if (gameObject.name == "JankRotatorOne")
            {
                pylonMovementInfo.rotationDirection = 1;
            }
            else if (gameObject.name == "JankRotatorTwo")
            {
                pylonMovementInfo.rotationDirection = -1;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D trig)
    {
        if(trig.gameObject.tag == "Player")
        {
            if (gameObject.name == "JankRotatorOne")
            {
                pylonMovementInfo.rotationDirection = 1;
            }
            else if (gameObject.name == "JankRotatorTwo")
            {
                pylonMovementInfo.rotationDirection = -1;
            }
        }
        
    }
}
