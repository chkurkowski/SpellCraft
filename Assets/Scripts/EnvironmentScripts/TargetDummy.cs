using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour
{
    public int targetDummyNumber;
    public float dummyHealth;
    private bool isAlive = true;
    private TutorialManager tutorialManagerInfo;

	// Use this for initialization
	void Start ()
    {
        tutorialManagerInfo = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (dummyHealth <= 0 && isAlive)
        {
            isAlive = false;
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;//destroyed animation here?
            tutorialManagerInfo.NextTutorialStage();
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(targetDummyNumber)
        {
            case 1:
                if(collision.tag == "Projectile")
                {
                    dummyHealth--;
                }
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }
}
