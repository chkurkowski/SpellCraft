using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overworldMusicHandler : MonoBehaviour 
{

    public bool isPlayingMusic = false;
    public AudioSource overworldMusic;
    public GameObject pylonHB;

	// Use this for initialization
	void Start () 
    {

    }
	
	// Update is called once per frame
	void Update () 
    {
        if (pylonHB.activeInHierarchy == false)
        {
            isPlayingMusic = true;
            overworldMusic.Play();
        }

        else if (pylonHB.activeInHierarchy == true)
        {
            overworldMusic.Stop();
            isPlayingMusic = false;
        }
    }
}
