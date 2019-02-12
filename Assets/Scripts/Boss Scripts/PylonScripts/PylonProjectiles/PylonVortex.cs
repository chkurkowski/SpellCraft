using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonVortex : MonoBehaviour
{
    private ProjectileDamage projectileDamageInfo;
    private float originalDamage;
    private GameObject player;

    private void Start()
    {
        projectileDamageInfo = gameObject.GetComponent<ProjectileDamage>();
        originalDamage = projectileDamageInfo.projectileDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Environment")
        {
            //collision.gameObject.transform.parent = gameObject.transform;
        }
        if(collision.tag == "Projectile")
        {
           // projectileDamageInfo.projectileDamage += collision.gameObject.GetComponent<ProjectileDamage>().projectileDamage;
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
        gameObject.transform.DetachChildren();
        projectileDamageInfo.projectileDamage = originalDamage;
    }
}
