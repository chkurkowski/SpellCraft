using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuartermasterScript : MonoBehaviour
{
    public Text quartermasterText;
    private TutorialManager tutorialManagerInfo;
    public Transform[] moveLocations = new Transform[6];
    [Multiline]
    public string[] instructions = new string[6];
    



	// Use this for initialization
	void Start ()
    {
        tutorialManagerInfo = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
        gameObject.transform.position = moveLocations[0].position;
        GameObject.Find("QuartermasterUiImage").GetComponent<RectTransform>().anchoredPosition = new Vector3(62, 20, 0);
        //-69 is the left position for x
        quartermasterText.text = instructions[0];
	}
	
    public void UpdateText(int tutorialStage)
    {
        quartermasterText.text = instructions[tutorialStage];
    }

    public void UpdatePosition(int newMoveLocation)
    {
        gameObject.transform.position = moveLocations[newMoveLocation].position;
    }
    
    public void MoveUiToLeft()
    {
        GameObject.Find("QuartermasterUiImage").GetComponent<RectTransform>().anchoredPosition = new Vector3(-69, 20, 0);
    }
    public void FaceLeft()
    {
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
    }
}
