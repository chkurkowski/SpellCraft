using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodyScreenManager : MonoBehaviour
{
    public GameObject bloodyScreenObject;
    private SpriteRenderer bloodyScreen;
    private PlayerHealth playerHealthInfo;
    private bool isBleeding = false;
    public float bleedSpeed = .1f;
	// Use this for initialization
	void Start ()
    {
        playerHealthInfo = GameObject.Find("Player").GetComponent<PlayerHealth>();
        bloodyScreen = bloodyScreenObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(!isBleeding && (playerHealthInfo.playerHealth / playerHealthInfo.maxPlayerHealth) <= 33f)
        {
            isBleeding = true;
            InvokeRepeating("BleedScreen", 0, bleedSpeed);
        }
	}


    void BleedScreen()
    {
        if ((playerHealthInfo.playerHealth / playerHealthInfo.maxPlayerHealth) <= 33f)
        {
            
        }
        else
        {
            isBleeding = false;
            CancelInvoke("BleedScreen");
        }
    }
}
