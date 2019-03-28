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
        }
    }

    public void NextTutorialStage()
    {
        tutorialStage++;
    }

}
