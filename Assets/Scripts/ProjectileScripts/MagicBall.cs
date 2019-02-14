using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
      //  Debug.Log("PLAYER PROJECTILE HIT: " + col.transform.name + "with tag: " + col.transform.tag);
        if(col.GetComponent<Collider2D>().transform.tag == null)
        {
          //  Debug.Log("PLAYER PROJECTILE HIT UNTAGGED OBJECT ");
            Destroy(gameObject);
        }
        if (col.GetComponent<Collider2D>().transform.tag == "Boss")
        {
            //Do damage
            // print("Hit: 5 damage");
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "Vortex" || col.gameObject.tag == "EnemyProjectile")
        {
           
            //do nothing
        }
        else if (col.gameObject.tag != "Player" && gameObject.tag != "Reflect" && col.gameObject.tag != "Simulacrum")
        {
            if (col.GetComponent<Collider2D>().transform.tag != "Boss" || gameObject.tag != "CameraTrigger")
            {
                Destroy(gameObject);
            }

        }
    }
}
