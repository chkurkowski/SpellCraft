using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaCameraActivator : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.tag == "Player")
        {
            GameObject.Find("Main Camera").GetComponent<CameraScriptActivator>().ActivateDynamicCamera();
        }
    }

    private void OnTriggerExit2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            GameObject.Find("Main Camera").GetComponent<CameraScriptActivator>().DisableDynamicCamera();
        }
    }
}
