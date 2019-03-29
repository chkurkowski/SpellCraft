using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRingScript : MonoBehaviour
{
    private bool isActivated = false;
    private TutorialManager tutorialManagerInfo;
    private GameObject parentRing;
    private bool isExitRing = false;

    private void Start()
    {
        parentRing = gameObject.transform.parent.gameObject;
        tutorialManagerInfo = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
        if(gameObject.name == "tutorialRingExit")
        {
            isExitRing = true;
        }
               
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !isActivated)
        {
            if(!isExitRing)
            {
                isActivated = true;
                gameObject.GetComponent<Collider2D>().enabled = false;
                parentRing.GetComponent<SpriteRenderer>().enabled = true;
                parentRing.layer = 16;
                parentRing.GetComponent<Collider2D>().isTrigger = false;
                tutorialManagerInfo.NextTutorialStage();
            }
            else if(isExitRing)
            {
                tutorialManagerInfo.NextTutorialStage();
            }
           
        }
    }


}
