using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonVortex : MonoBehaviour
{
    private ProjectileDamage projectileDamageInfo;
    private PylonAttacks pylonAttackInfo;
    private float originalVortexLimit;
    private float originalDamage;
    private GameObject player;
 
   
 
   

    public float vortexLimitIncreaseAmount = 2.5f;

    private void Start()
    {
        projectileDamageInfo = gameObject.GetComponent<ProjectileDamage>();
        pylonAttackInfo = GameObject.Find("Pylon").GetComponent<PylonAttacks>();
        originalVortexLimit = pylonAttackInfo.vortexGrowthLimit;
        originalDamage = projectileDamageInfo.projectileDamage;
      
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     
        if(collision.tag == "Projectile")
        {
            projectileDamageInfo.projectileDamage += collision.gameObject.GetComponent<ProjectileDamage>().projectileDamage;
            pylonAttackInfo.vortexGrowthLimit += vortexLimitIncreaseAmount;
        }
        if(collision.tag == "Player")
        {
            player = collision.gameObject;
            InvokeRepeating("VortexPlayer", 0, 1);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            CancelInvoke("VortexPlayer");
        }
    }

    public void VortexPlayer()
    {
        player.GetComponent<PlayerHealth>().DamagePlayer(projectileDamageInfo.projectileDamage);
    }

    public void FlushVortex()
    {
        CancelInvoke("VortexPlayer");
        projectileDamageInfo.projectileDamage = originalDamage;
        pylonAttackInfo.vortexGrowthLimit = originalVortexLimit;
    }
}
