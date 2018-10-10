using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour 
{
    public GameObject Player;
    public GameObject Boss;
    private float distance;
    private float zoomSize;
	
	// Update is called once per frame
	void Update ()
    {
        distance = Vector2.Distance(Player.transform.position, Boss.transform.position);
        zoomSize = distance;
        if(distance <=25)
        {
            GetComponent<Camera>().orthographicSize = 25;
        }
        else
        {
            GetComponent<Camera>().orthographicSize = zoomSize;
        }
    }
}
