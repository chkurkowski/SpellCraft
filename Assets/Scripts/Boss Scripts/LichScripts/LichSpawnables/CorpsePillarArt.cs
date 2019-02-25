using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpsePillarArt : MonoBehaviour
{
    public Transform associatedPillar;
	// Use this for initialization
	
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.position = associatedPillar.position;
    }
}
