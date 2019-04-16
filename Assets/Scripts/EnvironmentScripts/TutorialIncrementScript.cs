using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialIncrementScript : MonoBehaviour
{
    private TutorialManager tutorialInfo;
    private bool activateOnce = true;

    private void Start()
    {
        tutorialInfo = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && activateOnce)
        {
            activateOnce = false;
            tutorialInfo.NextTutorialStage();
        }
    }

}
