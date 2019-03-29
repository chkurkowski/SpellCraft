using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonShieldFollow : MonoBehaviour {

    private Transform pylonLocation;
    // Use this for initialization
    void Start ()
    {
     pylonLocation = GameObject.Find("ProtoNovus").transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        pylonLocation = GameObject.Find("ProtoNovus").transform;
        gameObject.transform.position = pylonLocation.transform.position;
        gameObject.transform.rotation = pylonLocation.transform.rotation;
    }
}
