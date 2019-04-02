using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHealth : MonoBehaviour
{
    //boss health bar
    private BossInfo bossInfo;
    public GameObject bossArt;
    public GameObject HealthBarParent;
    public Image healthBar;
    
   
    /// <summary>
    /// The current health of the boss.
    /// </summary>
    public float bossHealth = 100;
    /// <summary>
    /// The max health of the boss.
    /// </summary>
    public float bossMaxHealth = 100;

    //public AudioSource pylonAudioSource;

    public bool canBeDamaged = true;

    private bool isAlive = true;

    private bool isLasered = false;//forgot if this is even used tbh....

    private void Start()
    {
        bossInfo = gameObject.GetComponent<BossInfo>();
       // pylonAudioSource.Play();
        //healthBar = GameObject.Find("BossHealthBar").GetComponent<Image>();
        HealthBarParent.SetActive(false);
        healthBar.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update () 
    {
        if(bossInfo.isActivated)
        {
            HealthBarParent.SetActive(true);
            healthBar.gameObject.SetActive(true);
        }
        else if(!bossInfo.isActivated)
        {
            HealthBarParent.SetActive(false);
            healthBar.gameObject.SetActive(false);
        }
       //destroy boss if health = 0
        if (bossHealth <= 0)
        {

            //Destroy(boss);
            bossArt.gameObject.SetActive(false);
            HealthBarParent.SetActive(false);
            healthBar.gameObject.SetActive(false);

            isAlive = false;
            Color c = gameObject.GetComponent<SpriteRenderer>().color;
            c.a = .6f;
            gameObject.GetComponent<SpriteRenderer>().color = c;
            print("you win woohoo!");
           // pylonAudioSource.Stop();
        }
        healthBar.fillAmount = (bossHealth / 100f);

        if(bossHealth > bossMaxHealth)
        {
            bossHealth = bossMaxHealth;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (canBeDamaged)
        {
            if (col.GetComponent<Collider2D>().transform.tag == "Projectile")
            {
                ProjectileDamage projectileInfo = col.gameObject.GetComponent<ProjectileDamage>();
                if (projectileInfo != null)
                {
                    if(gameObject.name == "ProtoNovus")
                    {
                        bossHealth -= (projectileInfo.projectileDamage);
                    }
                  
                    //healthBar.fillAmount = healthBar.fillAmount - (projectileInfo.projectileDamage / 100f);
                }
                else
                {
                    Debug.Log("The projectile damage script wasn't found on object: " + col.gameObject.name);
                }
            }
        }
     }


    public void SetLasered(bool tof)
    {
        isLasered = tof;
    }

    public bool GetLasered()
    {
        return isLasered;
    }


    /// ///////////////////////////////////////// Alive Functions
    

    public bool GetAlive()
    {
        return isAlive;
    }

    public void SetAlive(bool aliveValue)
    {
        isAlive = aliveValue;
    }

    public void DealDamage(float damageAmount)
    {
        bossHealth -= damageAmount;
    }

    public void HealBoss(float healAmount)
    {
        bossHealth += healAmount;
    }


}
