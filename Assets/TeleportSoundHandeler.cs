using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportSoundHandeler : MonoBehaviour 
{

    public AudioSource tpSound;
    public GameObject reviveAnim;
    public GameObject deathAnim;


	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    public void PlayTPSound()
    {
        if(reviveAnim.activeInHierarchy == true)
        {
            tpSound.Play();
        }
    }
}
