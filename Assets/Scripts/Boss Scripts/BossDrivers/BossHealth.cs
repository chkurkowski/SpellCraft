using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHealth : MonoBehaviour
{
    //boss health bar

    public Image healthBar;
    public GameObject boss;
    public float bossHealth = 100;
    public float bossMaxHealth = 100;
    public bool isAlive = true;

    private void Start()
    {
        healthBar = GameObject.Find("BossHealthBar").GetComponent<Image>();
    }
    // Update is called once per frame
    void Update () 
    {
       //destroy boss if health = 0
        if(bossHealth <= 0)
        {
            //Destroy(boss);
            isAlive = false;
            Color c = gameObject.GetComponent<SpriteRenderer>().color;
            c.a = .6f;
            gameObject.GetComponent<SpriteRenderer>().color = c;
            print("you win woohoo!");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Projectile") 
        {
            float projectileDamage = 0;
            MagicBall magicBallInfo = col.gameObject.GetComponent<MagicBall>();
            if(magicBallInfo!= null)
            {
                 projectileDamage = magicBallInfo.magicBallDamage;
            }
            else
            {
                projectileDamage = col.gameObject.GetComponent<Fireball>().fireBallDamage/3;
            }
            
            bossHealth -= projectileDamage;
            healthBar.fillAmount = healthBar.fillAmount - (projectileDamage / 100f);
            
            
        }
        
     }



}
