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

    public AudioSource playerHealthSource;
    public AudioClip healPlayerSound;
    public AudioClip damagePlayerSound;


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
           
          

        }
        else if(playerHealth > maxPlayerHealth)
        {
            playerHealth = maxPlayerHealth;
        }

    }

    public void DamagePlayer(float dmg)
    {
        //TODO add player damage sound here
        playerHealthSource.clip = damagePlayerSound;
        playerHealthSource.PlayOneShot(damagePlayerSound);
        playerHealth -= dmg;
        StartCoroutine(InvincibilityFrames());
    }

    public void HealPlayer(float healAmount)
    {
        //TODO add player heal sound here
         playerHealthSource.clip = healPlayerSound;
         playerHealthSource.PlayOneShot(healPlayerSound);
        playerHealth += healAmount;
    }

    private IEnumerator InvincibilityFrames()
    {
        gameObject.layer = 14;
        Color firstColor = gameObject.GetComponent<SpriteRenderer>().color;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(.07f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(.10f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(.07f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(.10f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(.07f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(.10f);
        gameObject.layer = 13;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

      

        if (col.gameObject.tag == "CheckPoint")
        {
            respawnManagerInfo.currentCheckPoint = col.gameObject;
           
        }
    }

   
}
