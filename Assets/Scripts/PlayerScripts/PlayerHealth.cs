using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image playerHealthBar;
    public float maxPlayerHealth = 100f;
    public float playerHealth = 100f;
    private RespawnManager respawnManagerInfo;


	// Use this for initialization
	void Start () 
    {
        respawnManagerInfo = GameObject.Find("RespawnManager").GetComponent<RespawnManager>();	
	}
	
	// Update is called once per frame
	void Update () 
    {

        if(playerHealth <= 0)
        {
            respawnManagerInfo.KillPlayer();
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
        if (col.gameObject.tag == "EnemyProjectile")
        {
            Debug.Log("Player was hit!");
            float projectileDamage = col.gameObject.GetComponent<Fireball>().fireBallDamage;
            playerHealthBar.fillAmount = playerHealthBar.fillAmount - (projectileDamage / 100);
            playerHealth -= projectileDamage;
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "Boss")
        {
            InvokeRepeating("MeleeDamage", 0, .5f);
        }

        if (col.gameObject.tag == "CheckPoint")
        {
            respawnManagerInfo.currentCheckPoint = col.gameObject;
            Debug.Log("CheckPoint was found!");
        }
    }

    private void OnTriggerExit2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Boss")
        {
            CancelInvoke("MeleeDamage");
        }
    }

    public void MeleeDamage()
    {
        playerHealth -= 10;
        playerHealthBar.fillAmount -= .10f;
    }
}
