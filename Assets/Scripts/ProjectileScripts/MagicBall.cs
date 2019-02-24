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
    private bool reflected = false;
    private bool canReflect = true;

    private void Start()
    {
        projectileDamageInfo = gameObject.GetComponent<ProjectileDamage>();
        magicBallDamage = projectileDamageInfo.projectileDamage;
    }

    void Update()
    {
        if(!firedFromPlayer && !reflected)
        {
            transform.Translate(Vector2.up * Time.deltaTime * magicBallSpeed);
        }
        else if(reflected && canReflect)
        {
            canReflect = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity * -1;
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
        else if (col.gameObject.tag == "EnemyReflect")
        {
            Debug.Log("enemy reflect should occur");
            reflected = true;
            gameObject.tag = "EnemyProjectile";
            gameObject.layer = 9; //changes physics layers, do not touch or I stab you
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
        }
        if (col.GetComponent<Collider2D>().transform.tag == "Boss")
        {
           // Debug.Log(gameObject.name + " was destroyed by Boss:" + col.gameObject.name);
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "Vortex" || col.gameObject.tag == "EnemyProjectile")
        {
           
            //do nothing
        }
        else if (col.gameObject.tag != "Player" && col.gameObject.tag != "Reflect" && col.gameObject.tag != "Simulacrum" && col.gameObject.tag != "EnemyReflect" && col.gameObject.tag != "CameraTrigger")
        {
            if (col.gameObject.tag != "Boss" || col.gameObject.tag != "CameraTrigger" || col.gameObject.tag != "HealStun" )
            {
                //Debug.Log(gameObject.name + " was destroyed by " + col.gameObject.name + "with tag :" + col.gameObject.tag );
                Destroy(gameObject);
            }

        }
       
    }
}
