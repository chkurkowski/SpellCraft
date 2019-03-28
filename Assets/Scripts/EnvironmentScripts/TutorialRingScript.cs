using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRingScript : MonoBehaviour
{
    private bool isActivated = false;
    private TutorialManager tutorialManagerInfo;
    private GameObject parentRing;

    private void Start()
    {
        parentRing = gameObject.transform.parent.gameObject;
        tutorialManagerInfo = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !isActivated)
        {
            isActivated = true;
            gameObject.GetComponent<Collider2D>().enabled = false;
            parentRing.layer = 9;
            parentRing.GetComponent<Collider2D>().isTrigger = false;
            tutorialManagerInfo.NextTutorialStage();
        }
    }


}
