using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the Magic Missile Projectile
public class MagicBall : MonoBehaviour {

    private ProjectileDamage projectileDamageInfo;
     public float magicBallDamage;
    public float magicBallSpeed = 75;

    private void Start()
    {
        projectileDamageInfo = gameObject.GetComponent<ProjectileDamage>();
        magicBallDamage = projectileDamageInfo.projectileDamage;
    }

    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Boss")
        {
            //Do damage
            // print("Hit: 5 damage");
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "Vortex" || col.gameObject.tag == "EnemyProjectile")
        {
            //do nothing
        }
        else if (col.gameObject.tag != "Player" && col.gameObject.tag != "Reflect" && col.gameObject.tag != "Simulacrum")
        {
            if (col.gameObject.tag != "Boss" || col.gameObject.tag != "CameraTrigger" || col.gameObject.tag != "HealStun")
            {
                Destroy(gameObject);
            }

        }
    }
}
