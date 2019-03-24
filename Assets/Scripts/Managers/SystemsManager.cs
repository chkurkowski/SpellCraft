using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SystemsManager : MonoBehaviour
{
    public Image options;
    private bool enabled = false;

	// Update is called once per frame
	void Update ()
    {
        // if(Input.GetKeyDown(KeyCode.Escape))
        // {
        //     Application.Quit();
        // }
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            print("hit");
            if (options.enabled == true && enabled)
            {
                enabled = false;
                options.gameObject.SetActive(false);
            }
            else
            {
                enabled = true;
                options.gameObject.SetActive(true);
            }
        }
    }
}
