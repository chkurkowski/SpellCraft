using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the Magic Missile Projectile
public class MagicBall : MonoBehaviour {

    private ProjectileDamage projectileDamageInfo;
    private float stunDamage;
    public bool firedFromPlayer = true;
    public float magicBallDamage;
    public float magicBallSpeed = 75;

    private void Start()
    {
        projectileDamageInfo = gameObject.GetComponent<ProjectileDamage>();
        magicBallDamage = projectileDamageInfo.projectileDamage;
    }

    void Update()
    {
        if(!firedFromPlayer)
        {
            transform.Translate(Vector2.up * Time.deltaTime * magicBallSpeed);
        }
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
        else if (col.gameObject.tag != "Player" && col.gameObject.tag != "Reflect" && col.gameObject.tag != "Simulacrum")
        {
            if (col.gameObject.tag != "Boss" || col.gameObject.tag != "CameraTrigger" || col.gameObject.tag != "HealStun")
            {
                Destroy(gameObject);
            }

        }
    }
}
