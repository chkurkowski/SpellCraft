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
    private float limitedDistance = 10000f;
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
        distance = Vector2.Distance(Player.transform.position, DistanceLimiter());
        zoomSize = distance + zoomTweak;
        if(Vector3.Distance(Player.transform.position, PlayerAbilities.instance.handlers.cursorInWorldPos) >= 12)
            gameObject.transform.position = new Vector3(((Player.transform.position.x + DistanceLimiter().x) / 2), ((Player.transform.position.y + DistanceLimiter().y) / 2), -5f);
        // if (distance <= minZoom)
        // {
        //     cam.orthographicSize = minZoom;
        // }
        // else if (distance >= maxZoom)
        // {
        //     cam.orthographicSize = maxZoom;
        // }
        // else
        // {
        //     cam.orthographicSize = zoomSize;
        // }
    }

    public Vector3 DistanceLimiter()
    {
        float dist = Vector3.Distance(Player.transform.position, PlayerAbilities.instance.handlers.cursorInWorldPos);
        if(dist >= limitedDistance)
        {
            float angle = Mathf.Atan2(PlayerAbilities.instance.handlers.cursorInWorldPos.y - Player.transform.position.y, PlayerAbilities.instance.handlers.cursorInWorldPos.x - Player.transform.position.x);

            float X = transform.position.x + (limitedDistance * Mathf.Cos(angle));
            float Y = transform.position.y + (limitedDistance * Mathf.Sin(angle));

            print("X: " + X + " Y: " + Y);
            return new Vector2(X, Y);
        }
        else
        {
            return (Vector2)PlayerAbilities.instance.handlers.cursorInWorldPos;
        }

    }
}
