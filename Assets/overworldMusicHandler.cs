using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverworldMusicHandler : MonoBehaviour
{
    public bool isPlayingOwMusic = false;
    public AudioSource overworldMusic;
    private BossHealth health;
    public Image pylonHealthBar;
//    public Image lichHealthBar;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (pylonHealthBar.isActiveAndEnabled)
        {
            isPlayingOwMusic = false;
            overworldMusic.Stop();
        }
        else if (!pylonHealthBar.isActiveAndEnabled)
        {
            if(!isPlayingOwMusic)
            {
                isPlayingOwMusic = true;
                overworldMusic.Play();
            }
        }
        else if (pylonHealthBar.isActiveAndEnabled && pylonHealthBar.fillAmount > 0 )
        {
            overworldMusic.Stop();
            isPlayingOwMusic = true;     
        }




 //       if (lichHealthBar.isActiveAndEnabled)
 //      {
 //          isPlayingOwMusic = false;
 //          overworldMusic.Stop();
 //       }
 //       else if (!lichHealthBar.isActiveAndEnabled)
 //       {
 //          if (!isPlayingOwMusic)
 //           {
 //               isPlayingOwMusic = true;
 //               overworldMusic.Play();
 //           }
 //       }
 //       else if (lichHealthBar.isActiveAndEnabled && pylonHealthBar.fillAmount > 0)
 //       {
 //           overworldMusic.Stop();
 //           isPlayingOwMusic = true;
 //       }
    }
   




}

        


