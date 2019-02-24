using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemPlayer : MonoBehaviour
{
    private BossHealth lichBossHealth;
    public float golemHealth = 10f;
    public float golemHealthMaximum = 10f;
    public float golemDeathDamage = 10f;
    public bool canFacePlayer = true;

    public GameObject golemProjectile;
    public Transform golemMuzzle;
    public GameObject golemReflect;

    private float attackTimer = 0;
    public float golemFireRate = .5f;
    public float golemReflectLength = 3f;
    public float golemReflectRecharge = 6f;

    public bool canAttack = true;
    public bool canReflect = true;

    #region FacePlayer Variables
    private GameObject player;
    private Vector3 vectorToTarget;
    private float angle;
    private Quaternion rotAngle;
    #endregion


    // Use this for initialization
    void Start ()
    {
        golemReflect.SetActive(false);
        lichBossHealth = GameObject.Find("Lich").GetComponent<BossHealth>();
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (golemHealth <= 0) // if dead
        {
            golemHealth = golemHealthMaximum;

            lichBossHealth.DealDamage(golemDeathDamage);
            Debug.Log("Lich should be damaged");
            golemReflect.SetActive(false);
            canAttack = true;
            canReflect = true;
            gameObject.SetActive(false);

        }
        else
        {
            FacePlayer();
            attackTimer += Time.deltaTime;
            if (attackTimer >= golemFireRate && canAttack)
            {
                FireProjectile();
                attackTimer = 0;
                
            }
        }

    }


    public void FireProjectile()
    {
        GameObject spawnedFireBall = Instantiate(golemProjectile, golemMuzzle.position, golemMuzzle.rotation);

        
    }


    
    public void Reflect()
    {
        if(canReflect)
        {
            canReflect = false;
            canAttack = false;
            golemReflect.SetActive(true);
            Invoke("ReflectOff", golemReflectLength);
            Invoke("ReflectReset", golemReflectRecharge);
        }
    }


    public void ReflectOff()
    {
        canAttack = true;
        golemReflect.SetActive(false);
    }

    public void ReflectReset()
    {
        canReflect = true;
    }


    public void FacePlayer()
    {
        if (canFacePlayer)
        {
            Vector3 dir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, transform.forward);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            golemHealth -= collision.gameObject.GetComponent<ProjectileDamage>().projectileDamage;
            if(canReflect)
            {
                Reflect();

            }
        }
    }

}
