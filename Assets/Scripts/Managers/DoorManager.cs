using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private PlayerHealth playerHealthInfo;
    public GameObject relatedBoss;
    public GameObject doorToOpenAndClose;
   

    private bool relevant = true;
    private bool doorClosed = false;
    // Use this for initialization
    void Start ()
    {
        playerHealthInfo = GameObject.Find("Player").GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(relevant)
        {
            if(relatedBoss.GetComponent<BossInfo>().isActivated && !doorClosed)
            {
                doorClosed = true;
                doorToOpenAndClose.SetActive(true);
            }

            if(relatedBoss.GetComponent<BossHealth>().GetAlive() || playerHealthInfo.playerHealth <= 0)
            {
                doorClosed = false;
                doorToOpenAndClose.SetActive(true);
            }
        }
		
	}
}
