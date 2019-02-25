using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellKnight : MonoBehaviour, IPooledObject
{
    public float hellKnightHealth = 1.5f;
    public float hellKnightDamage = 5f;
    public float moveSpeed = 40f;

   




    private Vector3 vectorToTarget;
    private float angle;
    private GameObject player;
    private Quaternion rotAngle;

    public float lookAtRate = 0.1f;
    public float lookAtSpeed = 0.8f;
    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("Player");
    }
    public void OnObjectSpawn()
    {
        player = GameObject.Find("Player");
        FacePlayer();
        InvokeRepeating("SlowRotateToPlayer", 0, lookAtRate);
    }

    // Update is called once per frame
    void Update ()
    {
    
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
        if(hellKnightHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void SlowRotateToPlayer()
    {
         //Debug.Log("Slow rotate was called!");
        vectorToTarget = player.transform.position - transform.position;
        angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        rotAngle = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotAngle, Time.deltaTime * lookAtSpeed);
    }

    public void FacePlayer()
    {
        Vector3 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, transform.forward);
    }




    public void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Reflect" || trig.gameObject.tag == "Environment")
        {
            gameObject.SetActive(false);
        }
        if (trig.gameObject.tag == "Player")
        {
            trig.gameObject.GetComponent<PlayerHealth>().DamagePlayer(hellKnightDamage);
            gameObject.SetActive(false);//this is where animation for skeleton explode goes
        }
        if (trig.gameObject.tag == "Projectile")
        {
            hellKnightHealth -= trig.gameObject.GetComponent<ProjectileDamage>().projectileDamage;
            trig.gameObject.GetComponent<ProjectileDamage>().projectileHealth -= hellKnightDamage;  
        }
        if (trig.gameObject.tag == "CameraTrigger" || trig.gameObject.tag == "EnemyReflect" || trig.gameObject.tag == "Portal" || trig.gameObject.tag == "Boss")
        {
            //do nothing
        }
        if(trig.gameObject.tag == "Skeleton")
        {
            trig.gameObject.SetActive(false);
        }
        //if (trig.gameObject.tag == "EnemyProjectile")
        //{
        //    hellKnightHealth -= trig.gameObject.GetComponent<ProjectileDamage>().projectileDamage;
        //    Destroy(gameObject);
        //}
    }



}
