using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemPlayer : MonoBehaviour
{
    private ObjectPoolerScript objectPooler;
    private BossHealth lichBossHealth;
    private SpriteRenderer colorInfo;

    public float golemHealth = 10f;
    public float golemHealthMaximum = 10f;
    public float golemDeathDamage = 10f;
    public bool canFacePlayer = true;

    //public GameObject golemProjectile;
    public Transform golemMuzzle;
    public GameObject golemReflect;

    private float attackTimer = 0;
    public float golemFireRate = .5f;
    public float golemReflectLength = 3f;
    public float golemReflectRecharge = 6f;

    public bool canAttack = true;
    public bool canReflect = true;
    public bool isMoving = false;
    public float randomMoveFrequency = 1f;
    public float moveSpeed = 10f;
    public float moveDuration = 3f;

    #region FacePlayer Variables
    private GameObject player;
    private Vector3 vectorToTarget;
    private float angle;
    private Quaternion rotAngle;
    #endregion


    // Use this for initialization
    void Start ()
    {
        objectPooler = ObjectPoolerScript.Instance;
        colorInfo = gameObject.GetComponent<SpriteRenderer>();
        golemReflect.SetActive(false);
        lichBossHealth = GameObject.Find("Lich").GetComponent<BossHealth>();
        player = GameObject.Find("Player");
       // StartCoroutine("EndMovement");
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (golemHealth <= 0) // if dead
        {
            golemHealth = golemHealthMaximum;

            lichBossHealth.DealDamage(golemDeathDamage);
          //  Debug.Log("Lich should be damaged");
            golemReflect.SetActive(false);
            canAttack = true;
            canReflect = true;
            isMoving = false;
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
            if(!isMoving)
            {
                Move();
            }
        }

    }


    public void FireProjectile()
    {
        GameObject spawnedFireBall = objectPooler.SpawnFromPool("Fireball", transform.position, transform.rotation);
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

    public void Move()
    {
        isMoving = true;
       
            InvokeRepeating("Movement", 0, randomMoveFrequency);
        InvokeRepeating("EndMovement", 0, moveDuration);
        
    }

    public void Movement()
    {
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
        //gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * -moveSpeed , ForceMode2D.Impulse);
    }

    public void RandomMovement()
    {
        Vector3 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, transform.forward);

        int randDirection = Random.Range(0, 4);
        if (randDirection == 0)
        {
            Debug.Log("Boss moves towards player.");
              gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * moveSpeed, ForceMode2D.Impulse);
            //transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
        }
        else if (randDirection == 1)
        {
            Debug.Log("Boss moves away from player.");
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * moveSpeed * -1 , ForceMode2D.Impulse);
            //transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
        }
        else if (randDirection == 2)
        {
            Debug.Log("Boss strafes right.");
             gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * moveSpeed , ForceMode2D.Impulse);
            //transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        }
        else if (randDirection >= 3)
        {
            Debug.Log("Boss strafes left.");
             gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * moveSpeed * -1 , ForceMode2D.Impulse);
            //transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            golemHealth -= collision.gameObject.GetComponent<ProjectileDamage>().projectileDamage;
            colorInfo.color = Color.red;
            Invoke("ResetColor", 0.50f);
            if(canReflect)
            {
                Reflect();
            }
        }
    }

    IEnumerator EndMovement()
    {
        yield return new WaitForSeconds(moveDuration);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0); 
    }



    private void ResetColor()
    {
        colorInfo.color = Color.white;
    }

}
