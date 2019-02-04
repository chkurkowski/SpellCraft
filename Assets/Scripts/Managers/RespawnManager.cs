using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour{
    public GameObject currentCheckPoint;
    private PlayerHealth playerHealthInfo;


    void Start()
    {
        playerHealthInfo = FindObjectOfType<PlayerHealth>();
    }


    void Update()
    {


    }
    public void KillPlayer()
    {
        playerHealthInfo.transform.position = currentCheckPoint.transform.position;
        playerHealthInfo.HealPlayer(playerHealthInfo.maxPlayerHealth);
        playerHealthInfo.playerHealthBar.fillAmount += 1;
       
    }
}
