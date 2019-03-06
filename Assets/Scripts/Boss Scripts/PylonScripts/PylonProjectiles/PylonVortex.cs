using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonVortex : MonoBehaviour
{
    private ProjectileDamage projectileDamageInfo;
    private PylonAttacks pylonAttackInfo;
    private float originalDamage;
    public float vortexDamageNOTFOREDITING;

    public float vortexGrowthRate = .001f;
    private const float finalVortexLimit = 13f;
    private const float originalVortexLimit = 7f;
    public float vortexGrowthLimit = 7f;
    private GameObject player;

    public float vortexGrowthAmount = .1f;
    public float vortexRotateAmount = 1f;
 
    public float vortexLimitIncreaseAmount = .1f;

    private void Start()
    {
        projectileDamageInfo = gameObject.GetComponent<ProjectileDamage>();
        pylonAttackInfo = GameObject.Find("Pylon").GetComponent<PylonAttacks>();
        originalDamage = projectileDamageInfo.projectileDamage;

        vortexDamageNOTFOREDITING = projectileDamageInfo.projectileDamage;


    }

    private void FixedUpdate()
    {
        if (gameObject.transform.localScale.x <= vortexGrowthLimit && gameObject.transform.localScale.x <= finalVortexLimit)
        {
            gameObject.transform.localScale += new Vector3(vortexGrowthAmount, vortexGrowthAmount, 0);
        }
        transform.Rotate(0, 0, vortexRotateAmount);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     
       
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
        player.GetComponent<PlayerHealth>().DamagePlayer(vortexDamageNOTFOREDITING);
    }

    public void FlushVortex()
    {
        CancelInvoke("VortexPlayer");
        projectileDamageInfo.projectileDamage = originalDamage;
        vortexGrowthLimit = originalVortexLimit;
        vortexDamageNOTFOREDITING = projectileDamageInfo.projectileDamage;
    }
}
