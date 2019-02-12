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
	
}
