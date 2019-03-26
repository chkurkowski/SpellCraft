using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overworldAudio : MonoBehaviour 
{

    public bool isPlayingOWMusic = false;
    public AudioSource owAudio;
    public GameObject pylon;
    public GameObject lich;

    // Use this for initialization
    void Start () 
    {

    }
	
	// Update is called once per frame
	void Update () 
    {
        if (pylon.GetComponentInChildren<BossInfo>().isActivated || lich.GetComponentInChildren<BossInfo>().isActivated || isPlayingOWMusic)
        {
            isPlayingOWMusic = false;
        }
        else
        {
            isPlayingOWMusic = true;

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
