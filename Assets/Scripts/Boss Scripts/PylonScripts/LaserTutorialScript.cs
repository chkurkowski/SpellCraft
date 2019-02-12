using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTutorialScript : MonoBehaviour
{

    private LineRenderer lineRender;
    public Transform laserHit;

	// Use this for initialization
	void Start ()
    {
        lineRender = GetComponent<LineRenderer>();
        lineRender.useWorldSpace = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);
       // Debug.Log(hit.transform.name);
       // Debug.DrawLine(transform.position, transform.up);
        laserHit.position = hit.point;//makes the direction object(laserHit) move with the raycast
        lineRender.SetPosition(0, transform.position);
        lineRender.SetPosition(1, laserHit.position);
	}
}
