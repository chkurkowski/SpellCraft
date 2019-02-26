using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScriptActivator : MonoBehaviour {
    private DynamicCamera dynamicCameraInfo;
    private GameObject player;
    
    

	// Use this for initialization
	void Start ()
    {
        dynamicCameraInfo = gameObject.GetComponent<DynamicCamera>();

        dynamicCameraInfo.enabled = false;

        player = GameObject.Find("Player");
	}

     void Update()
    {
        if(dynamicCameraInfo.enabled == false)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -5);
           // gameObject.GetComponent<Camera>().orthographicSize = 70;
           //Chase, i commented this out because i wanted the lich arena to be zoomed out, but this was forcing the camera
           //to be zoomed tf in. we can add it back if we need it
        }
    }


    public void ActivateDynamicCamera()
    {
        dynamicCameraInfo.enabled = true;

    }


    public void DisableDynamicCamera()
    {
        dynamicCameraInfo.enabled = false;
    }
}
