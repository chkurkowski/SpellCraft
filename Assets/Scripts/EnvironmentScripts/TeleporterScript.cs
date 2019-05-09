using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterScript : MonoBehaviour
{
    public Transform destination;
    public bool canTeleport = true;
    public bool isTutorialTeleport = false;
    public GameObject teleAnim;
    public GameObject teleAnimReserved;
    public Transform teleportDestination;
    public AudioSource tpSound;

    private void OnTriggerEnter2D(Collider2D trig)
    {

        if(trig.gameObject.tag == "Player")
        {
            tpSound.Play();
            GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = false;
            GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
            GameObject.Find("Player").GetComponent<SpriteRenderer>().enabled = false;
            GameObject spawnedAnim = Instantiate(teleAnim, GameObject.Find("Player").transform.position, transform.rotation);
            spawnedAnim.GetComponent<TeleportAnim>().SetTeleReference(gameObject);
        }
    }

    public void TeleportPlayer()
    {
        if (canTeleport)
        {
            if (isTutorialTeleport)
            {
                GameObject.Find("TutorialManager").GetComponent<TutorialManager>().NextTutorialStage();
                isTutorialTeleport = false;
            }
            GameObject.Find("Player").GetComponent<PlayerHealth>().ResetPlayerHealth();
            GameObject.Find("Player").gameObject.transform.position = destination.gameObject.GetComponent<TeleporterScript>().teleportDestination.position;
            GameObject spawnedReverseAnim = Instantiate(teleAnimReserved, GameObject.Find("Player").transform.position, transform.rotation);
            spawnedReverseAnim.GetComponent<TeleportAnim>().SetTeleReference(gameObject);
           
            destination.gameObject.GetComponent<TeleporterScript>().canTeleport = false;
            destination.gameObject.GetComponent<TeleporterScript>().CanTeleport();    
        }
    }

    public void UnteleportPlayer()
    {
        tpSound.Play();
        GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
        GameObject.Find("Player").GetComponent<SpriteRenderer>().enabled = true;
    }

    public void CanTeleport()
    {
        Invoke("TeleportReset", 1f);   
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
