using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterScript : MonoBehaviour
{
    public Transform destination;
    public bool canTeleport = true;
    public bool isTutorialTeleport = false;

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.tag == "Player")
        {
            if(canTeleport)
            {
                if(isTutorialTeleport)
                {
                    GameObject.Find("TutorialManager").GetComponent<TutorialManager>().NextTutorialStage();
                    isTutorialTeleport = false;
                }
                trig.gameObject.transform.position = destination.position + new Vector3(0, 15, 0); //if u want tele to tele link
                destination.gameObject.GetComponent<TeleporterScript>().canTeleport = false;
                destination.gameObject.GetComponent<TeleporterScript>().CanTeleport();
            }
        }
    }

    public void CanTeleport()
    {
        Invoke("TeleportReset", .5f);   
    }

    public void TeleportReset()
    {
        canTeleport = true;
    }

    private void OnTriggerExit2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            canTeleport = true;
        }
    }

}
