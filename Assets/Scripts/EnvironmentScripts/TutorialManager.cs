using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public int tutorialStage = 0;
    public QuartermasterScript Quartermaster;
    public GameObject attackDummyOne;
    public GameObject attackDummyTwo;
    public GameObject attackDummyThree;
    public GameObject absorbDummyOne;
    public GameObject absorbDummyTwo;
    public GameObject absorbDummyThree;
    public GameObject splitDummyOne;
    public GameObject splitDummyTwo;
    public GameObject splitDummyThree;


    public GameObject rearWallClose;
    public GameObject rearGroundLaser;
    public GameObject rearGroundLaser2;

   // public GameObject tutorialRing;
   // public GameObject tutorialRingExit;
   // public GameObject tutorialDoor;

    private void Start()
    {
        attackDummyOne.SetActive(false);
        attackDummyTwo.SetActive(false);
        attackDummyThree.SetActive(false);
        absorbDummyOne.SetActive(false);
        absorbDummyTwo.SetActive(false);
        absorbDummyThree.SetActive(false);
        splitDummyOne.SetActive(false);
        splitDummyTwo.SetActive(false);
        splitDummyThree.SetActive(false);
        rearWallClose.SetActive(false);
        

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
                attackDummyOne.SetActive(true);
                rearWallClose.SetActive(true);
                rearGroundLaser.SetActive(false);
                rearGroundLaser2.SetActive(false);


                Quartermaster.UpdateText(tutorialStage);
                Quartermaster.UpdatePosition(tutorialStage);
                break;
            case 11:
                //    dummyThree.SetActive(true);
                attackDummyTwo.SetActive(true);
              
                Quartermaster.UpdateText(tutorialStage);
                Quartermaster.UpdatePosition(tutorialStage);
                break;
            case 12:
                //  tutorialDoor.SetActive(false);
                attackDummyThree.SetActive(true);
                Quartermaster.UpdateText(tutorialStage);
                Quartermaster.UpdatePosition(tutorialStage);
                break;
            case 13:
                absorbDummyOne.SetActive(true);
            
                Quartermaster.UpdateText(tutorialStage);
                Quartermaster.UpdatePosition(tutorialStage);
                break;

            case 14:
                absorbDummyTwo.SetActive(true);
                Quartermaster.UpdateText(tutorialStage);
                Quartermaster.UpdatePosition(tutorialStage);
                break;
            case 15:
                absorbDummyThree.SetActive(true);
                Quartermaster.UpdateText(tutorialStage);
                Quartermaster.UpdatePosition(tutorialStage);
                break;
            case 16:
                splitDummyOne.SetActive(true);
         
                Quartermaster.UpdateText(tutorialStage);
                Quartermaster.UpdatePosition(tutorialStage);
                break;
            case 17:
                splitDummyTwo.SetActive(true);
                Quartermaster.UpdateText(tutorialStage);
                Quartermaster.UpdatePosition(tutorialStage);
                break;
            case 18:
                splitDummyThree.SetActive(true);
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
