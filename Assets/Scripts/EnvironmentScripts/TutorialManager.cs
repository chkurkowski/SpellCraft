using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public int tutorialStage = 0;
    public GameObject dummyOne;
    public GameObject dummyTwo;
    public GameObject dummyThree;
    public GameObject dummyFour;

    public GameObject tutorialRing;
    public GameObject tutorialRingExit;
    public GameObject tutorialDoor;

    private void Start()
    {
        dummyOne.SetActive(false);
        dummyTwo.SetActive(false);
        dummyThree.SetActive(false);
        dummyFour.SetActive(false);
    }

    private void Update()
    {
        switch (tutorialStage)
        {
            case 1:
                dummyOne.SetActive(true);
                break;
            case 2:
                dummyTwo.SetActive(true);
                break;
            case 3:
                dummyThree.SetActive(true);
                break;
            case 4:
                dummyFour.SetActive(true);
                break;
            case 5:
                tutorialRing.GetComponent<SpriteRenderer>().color = Color.magenta;
                tutorialRing.layer = 9;
                tutorialRingExit.SetActive(true);
                break;
            case 6:
                tutorialDoor.SetActive(false);
                break;
        }
    }

    public void NextTutorialStage()
    {
        tutorialStage++;
    }

}
