﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExit : MonoBehaviour {

	// Use this for initialization
	public void QuitGame()
	{
        Debug.Log("Gamequit");
        Application.Quit();
		
	}
}
