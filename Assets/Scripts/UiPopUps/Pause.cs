using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

    public Image options;
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.P))
        {
            print("hit");
            if (options.enabled == true)
                options.enabled = false;
            else
                options.enabled = true;
        }
	}
}
