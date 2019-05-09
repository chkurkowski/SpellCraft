using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public bool godMode = false;
    [Header("Editor Variables")]
    public Image playerHealthBar;
    
    [Header("If the Player is Alive")]
    public bool isAlive = true;
    private bool isRunning = false;

    [Header("Public Health Variables")]
    public float maxPlayerHealth = 100f;
    public float playerHealth = 100f;
    private RespawnManager respawnManagerInfo;
    private PlayerMovement movement;
    private PlayerAbilities abilities;
    private Animator playerAnimator;

    public GameObject deathTeleportAnim;
    public GameObject spawnTeleportAnim;

    public bool absorbDamage = false;
    public float damageAbsorbed{get; private set;}

    [Header("Audio Source")]
    public AudioSource playerHealthSource;
    public AudioClip healPlayerSound;
    public AudioClip damagePlayerSound;
    public AudioSource teleportSound;
    public AudioClip teleportClip;


	// Use this for initialization
	void Start () 
    {
        respawnManagerInfo = GameObject.Find("RespawnManager").GetComponent<RespawnManager>();
        movement = GetComponent<PlayerMovement>();
        playerAnimator = GetComponent<Animator>();
        abilities = GetComponent<PlayerAbilities>();
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
            Instantiate(deathTeleportAnim, transform.position, transform.rotation);
            isAlive = false;
            if(!teleportSound.isPlaying)
            {
                teleportSound.Play();
            }
         }
        else if(playerHealth > maxPlayerHealth)
        {
            playerHealth = maxPlayerHealth;
        }

        if(!absorbDamage && damageAbsorbed > 0)
        {
            damageAbsorbed = 0;
        }
    }

    public void SpawnReviveAnim()
    {
        Instantiate(spawnTeleportAnim, transform.position, transform.rotation);
        isAlive = true;
        abilities.state = PlayerAbilities.State.IDLE;
        if (!teleportSound.isPlaying)
        {
            teleportSound.Play();
        }
    }

    public void DamagePlayer(float dmg)
    {
        if(!absorbDamage && gameObject.layer == 13 && !isRunning && !godMode)
        {
            playerHealthSource.clip = damagePlayerSound;
            playerHealthSource.PlayOneShot(damagePlayerSound);
            playerHealth -= dmg;
            playerHealthBar.fillAmount = playerHealth / 100;
            // DamageAnimations();
            if(!isRunning)
                StartCoroutine(InvincibilityFrames());
        }
        else
        {
            damageAbsorbed += dmg;
        }
    }

    public void HealPlayer(float healAmount)
    {
        //TODO add player heal sound here
        playerHealthSource.clip = healPlayerSound;
        playerHealthSource.PlayOneShot(healPlayerSound);
        playerHealth += healAmount;
        playerHealthBar.fillAmount = playerHealth / 100;
    }

    private IEnumerator InvincibilityFrames()
    {
        isRunning = true;
        gameObject.layer = 14;
       // print("Layer: " + gameObject.layer);
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
        isRunning = false;
       // print("Layer: " + gameObject.layer);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "CheckPoint")
        {
            respawnManagerInfo.currentCheckPoint = col.gameObject;
        }
    }

    private void DamageAnimations()
    {
        switch(movement.playerDirection)
        {
            case 0:
                playerAnimator.SetTrigger("triggerPlayerDamageUp");
                break;
            case 1:
                playerAnimator.SetTrigger("triggerPlayerDamageRight");
                break;
            case 2:
                playerAnimator.SetTrigger("triggerPlayerDamageDown");
                break;
            case 3:
                playerAnimator.SetTrigger("triggerPlayerDamageLeft");
                break;
        }
    }

    public void ResetPlayerHealth()
    {
        playerHealth = maxPlayerHealth;
        playerHealthBar.fillAmount = playerHealth / 100;
        isAlive = true;
    }

}
