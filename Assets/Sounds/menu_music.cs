using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_music : MonoBehaviour 
{
    public bool isPlayingMenuMusic = false;
    public AudioSource menuAudio;
    public GameObject menuObject;


    // Use this for initialization
    void Start () 
    {

    }
	
	// Update is called once per frame
	void Update () 
    {
        if (gameObject.activeInHierarchy == true)
        {
            isPlayingMenuMusic = true;
        }
        else
        {
            isPlayingMenuMusic = false;
        }
	}

    public void MusicPlaying (bool isPlayingMenuMusic)
    {
        if(isPlayingMenuMusic == true)
        {
            menuAudio.Play();
        }
        else
        {
            menuAudio.Stop();
        }
    }

}
