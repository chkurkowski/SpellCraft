using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialPopUp : MonoBehaviour {
    public GameObject tutorialGhost;
	// Use this for initialization
	void Start ()
    {
        tutorialGhost.SetActive(false);	
	}

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.tag == "Player")
        {
            tutorialGhost.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            tutorialGhost.SetActive(false);
        }
    }
}

