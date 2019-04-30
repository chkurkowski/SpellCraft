using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public int tutorialStage = 0;
    public QuartermasterScript Quartermaster;
    public GameObject dummyOne;
    public GameObject dummyTwo;
    public GameObject dummyThree;

    public GameObject tutorialRing;
    public GameObject tutorialRingExit;
    public GameObject tutorialDoor;

    private void Start()
    {
        dummyOne.SetActive(false);
        dummyTwo.SetActive(false);
        dummyThree.SetActive(false);
    }

    private void Update()
    {
        switch (tutorialStage)
        {
            case 1:
            
                Quartermaster.UpdateText(tutorialStage);
                Quartermaster.UpdatePosition(tutorialStage);
                break;
            case 2:
             //   dummyTwo.SetActive(true);
                Quartermaster.UpdateText(tutorialStage);
                Quartermaster.UpdatePosition(tutorialStage);
                break;
            case 3:
             //   dummyThree.SetActive(true);
                Quartermaster.UpdateText(tutorialStage);
                Quartermaster.UpdatePosition(tutorialStage);
                break;
            case 4:
              //  dummyFour.SetActive(true);
                Quartermaster.UpdateText(tutorialStage);
                Quartermaster.UpdatePosition(tutorialStage);
               // Quartermaster.MoveUiToLeft();
               // Quartermaster.FaceLeft();
                break;
            case 5:
             //   tutorialRing.GetComponent<SpriteRenderer>().color = Color.magenta;
               // tutorialRing.layer = 9;
              //  tutorialRingExit.SetActive(true);
                Quartermaster.UpdateText(tutorialStage);
                Quartermaster.UpdatePosition(tutorialStage);
                break;
            case 6:
               // tutorialDoor.SetActive(false);
                Quartermaster.UpdateText(tutorialStage);
                Quartermaster.UpdatePosition(tutorialStage);
                break;
            case 7:
                Quartermaster.UpdateText(tutorialStage);
                Quartermaster.UpdatePosition(tutorialStage);
                break;
            case 8:
                Quartermaster.UpdateText(tutorialStage);
                Quartermaster.UpdatePosition(tutorialStage);
                break;
            case 9:
             //   dummyOne.SetActive(true);
                Quartermaster.UpdateText(tutorialStage);
                Quartermaster.UpdatePosition(tutorialStage);
                break;
            case 10:
           //     dummyTwo.SetActive(true);
                Quartermaster.UpdateText(tutorialStage);
                Quartermaster.UpdatePosition(tutorialStage);
                break;
            case 11:
            //    dummyThree.SetActive(true);
                Quartermaster.UpdateText(tutorialStage);
                Quartermaster.UpdatePosition(tutorialStage);
                break;
            case 12:
              //  tutorialDoor.SetActive(false);
                Quartermaster.UpdateText(tutorialStage);
                Quartermaster.UpdatePosition(tutorialStage);
                break;
        }
    }

    public void NextTutorialStage()
    {
        //Quartermaster.UpdateText(tutorialStage + 1);
       // Quartermaster.UpdatePosition(tutorialStage + 1);
        tutorialStage++;
    }

}
