using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overworldAudio : MonoBehaviour 
{

    public bool isPlayingOWMusic = false;
    public AudioSource owAudio;
    public GameObject player;

    // Use this for initialization
    void Start () 
    {

    }
	
	// Update is called once per frame
	void Update () 
    {
        if (player.transform.position.y > -121 && player.transform.position.y < -458 && player.transform.position.x > -162 && player.transform.position.x < 115)
        {
            isPlayingOWMusic = true;
        }
        else if (player.transform.position.y < -121 && player.transform.position.y > -458 && player.transform.position.x < -162 && player.transform.position.x > 115)
        {
            isPlayingOWMusic = false;
        }
    }
    public void MusicPlaying(bool isPlayingMenuMusic)
    {
        if (isPlayingMenuMusic == true)
        {
                owAudio.Play();
        }
        else
        {
                owAudio.Stop();
        }
    }
}
