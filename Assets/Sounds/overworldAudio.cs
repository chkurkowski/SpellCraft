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
        if (pylon.GetComponentInChildren<bossInfoInfo>().isActivated && !isPlayingMusic)
        {
            isPlayingOWMusic = true;
            owAudio.Play();

        }
        else if (bossHealthInfo.bossHealth <= 0)
        {
            lichMusic.Stop();
            isPlayingMusic = true;
        }
        else if (!bossInfoInfo.isActivated)
        {
            owAudio.Stop();
            isPlayingOWMusic = false;
        }
    }

}
