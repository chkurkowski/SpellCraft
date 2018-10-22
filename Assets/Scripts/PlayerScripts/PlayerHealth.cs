using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image playerHealthBar;
    public bool isAlive = true;
    public float maxPlayerHealth = 100f;
    public float playerHealth = 100f;
    private RespawnManager respawnManagerInfo;


	// Use this for initialization
	void Start () 
    {
        respawnManagerInfo = GameObject.Find("RespawnManager").GetComponent<RespawnManager>();
	}

    private void Awake()
    {
        isAlive = true;
    }

    // Update is called once per frame
    void Update () 
    {
        if(playerHealth <= 0)
        {
            respawnManagerInfo.KillPlayer();
            isAlive = false;
            //Color c = gameObject.GetComponent<SpriteRenderer>().color;
           // c.a = .55f;
            //gameObject.GetComponent<SpriteRenderer>().color = c;

        }
        else if(playerHealth > maxPlayerHealth)
        {
            playerHealth = maxPlayerHealth;
        }

    }

    public void HealPlayer(float healAmount)
    {
        playerHealth += healAmount;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

      

        if (col.gameObject.tag == "CheckPoint")
        {
            respawnManagerInfo.currentCheckPoint = col.gameObject;
            Debug.Log("CheckPoint was found!");
        }
    }

   
}
