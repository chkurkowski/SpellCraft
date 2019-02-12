using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonMovement : MonoBehaviour {
    //TODO 
    //write a function for spinning that takes a time variable for the laser.
    //also write a function that can be called to increase the rate at which the boss spins.
    // Use this for initialization

    public float rotationIncrement;
    public float rotationAmount;

    void Start ()
    {
        InvokeRepeating("PylonRotate", 0, rotationIncrement);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void PylonRotate()
    {
        gameObject.transform.Rotate(0, 0, rotationAmount);
    }

}
