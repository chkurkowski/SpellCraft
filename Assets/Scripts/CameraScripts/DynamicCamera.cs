using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour 
{
    public GameObject Player;
    public GameObject Boss;
    private float distance;
    private float zoomSize;
    [SerializeField]
    private float zoomTweak = 0;
	
	// Update is called once per frame
	void Update ()
    {
        distance = Vector2.Distance(Player.transform.position, Boss.transform.position);
        zoomSize = distance + zoomTweak;
        gameObject.transform.position = new Vector3(((Player.transform.position.x + Boss.transform.position.x)/2), ((Player.transform.position.y + Boss.transform.position.y) / 2), -5f);
        if (distance <=50)
        {
            GetComponent<Camera>().orthographicSize = 50;
        }
        else
        {
            GetComponent<Camera>().orthographicSize = zoomSize;
        }
    }
}
