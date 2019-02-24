using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemArtillery : MonoBehaviour {

    private BossHealth lichBossHealth;
    public float golemHealth = 10f;
    public float golemHealthMaximum = 10f;
    public float golemDeathDamage = 10f;
    public bool canFacePlayer = true;


    public GameObject golemArtilleryProjectile;
    public Transform artilleryMuzzle;


    private float attackTimer = 0;
    public float golemFireRate = 4f;



    #region FacePlayer Variables
    private GameObject player;
    private Vector3 vectorToTarget;
    private float angle;
    private Quaternion rotAngle;
    private bool isAttacking = false;
    #endregion

    // Use this for initialization
    void Start ()
    {
        lichBossHealth = GameObject.Find("Lich").GetComponent<BossHealth>();
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (golemHealth <= 0)
        {
            golemHealth = golemHealthMaximum;

            lichBossHealth.DealDamage(golemDeathDamage);
            Debug.Log("Lich should be damaged");
            gameObject.SetActive(false);

        }
        else
        {
            FacePlayer();
            attackTimer += Time.deltaTime;
            if (attackTimer >= golemFireRate)
            {
                attackTimer = 0;
                FireArtillery();
            }
        }
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

    public void FireArtillery()
    {
        Instantiate(golemArtilleryProjectile, artilleryMuzzle.position, artilleryMuzzle.rotation);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            golemHealth -= collision.gameObject.GetComponent<ProjectileDamage>().projectileDamage;
        }
        
    }
}
