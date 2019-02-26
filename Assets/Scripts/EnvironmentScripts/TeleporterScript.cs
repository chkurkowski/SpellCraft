using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterScript : MonoBehaviour
{
    public Transform destination;

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.tag == "Player")
        {
            trig.gameObject.transform.position = destination.position;// + new Vector3(0,15,0); if u want tele to tele link
        }
    }
}
