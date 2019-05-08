using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TutorialTextManager : MonoBehaviour {

	public string[] textMessages;

	public GameObject textBox;

	public Text text;

	private int messageNum = 0;

	// Use this for initialization
	void Start () 
	{
		textBox.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(messageNum != 0)
		{
			if(textBox.activeSelf == false)
				ToggleActive();

			SetMessage(messageNum);
		}
	}

	private void MoveOnScreen()
	{
		
	}

	private void MoveOffScreen()
	{
		
	}

	//Call this to turn off the dialogue
	public void ToggleActive()
	{
		textBox.SetActive(!textBox.activeSelf);
	}

	private void SetMessage(int index)
	{
		text.text = textMessages[index];
	}

	//Call this when the next tutorial text should appear
	public void IncrementMessageNumber()
	{
		messageNum++;
	}

}
