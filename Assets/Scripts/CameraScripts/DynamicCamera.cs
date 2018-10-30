using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour 
{
    public GameObject Player;
    public GameObject Boss;
    public Camera cam;

    private float distance;
    private float zoomSize;
    [SerializeField]
    private float zoomTweak = 0;
    private float minZoom = 55;
    private float maxZoom = 95;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update ()
    {
        distance = Vector2.Distance(Player.transform.position, Boss.transform.position);
        zoomSize = distance + zoomTweak;
        gameObject.transform.position = new Vector3(((Player.transform.position.x + Boss.transform.position.x)/2), ((Player.transform.position.y + Boss.transform.position.y) / 2), -5f);
        if (distance <= minZoom)
        {
            cam.orthographicSize = minZoom;
        }
        else if (distance >= maxZoom)
        {
            cam.orthographicSize = maxZoom;
        }
        else
        {
            cam.orthographicSize = zoomSize;
        }
    }
}
