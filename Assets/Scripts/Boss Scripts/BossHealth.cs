using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHealth : MonoBehaviour
{
    //boss health bar

    public Image healthBar;
    public GameObject boss;


	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
       //destroy boss if health = 0
        if(healthBar.fillAmount <= 0)
        {
               Destroy(boss);
            print("you win woohoo!");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Projectile") 
        {
            float projectileDamage = col.gameObject.GetComponent<Fireball>().fireBallDamage;

            healthBar.fillAmount = healthBar.fillAmount - (projectileDamage / 100f);
        }
     }



}
