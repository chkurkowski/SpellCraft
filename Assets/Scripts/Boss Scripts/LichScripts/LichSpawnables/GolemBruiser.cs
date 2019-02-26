using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBruiser : MonoBehaviour
{
    private BossHealth lichBossHealth;
    private LichAttacks lichBossAttacks;
    private SpriteRenderer colorInfo;

    public float golemHealth = 10f;
    public float golemHealthMaximum = 10f;
    public float golemDeathDamage = 10f;
    private bool canFacePlayer = true;


    private GameObject player;
    public GameObject criticalSpot;
    public float attackRate;

    public float chargeDamage = 15f;
    public float chargeSpeed;
    public float chargeRate;
    public float chargeLength;

    private Vector3 vectorToTarget;
    private float angle;
    private Quaternion rotAngle;
    private bool isAttacking = false;


    // Use this for initialization
    void Start()
    {
     
        colorInfo = gameObject.GetComponent<SpriteRenderer>();
        lichBossAttacks = GameObject.Find("Lich").GetComponent<LichAttacks>();
        lichBossHealth = GameObject.Find("Lich").GetComponent<BossHealth>();
        player = GameObject.Find("Player");
        criticalSpot.SetActive(false);
    }

    private void OnEnable()
    {
        criticalSpot.SetActive(false);
        golemHealth = golemHealthMaximum;
    }

    // Update is called once per frame
    void Update()
    {
        if (golemHealth <= 0)
        {
            golemHealth = golemHealthMaximum;
            transform.position = lichBossAttacks.golemOneSpawn.position;
            lichBossHealth.DealDamage(golemDeathDamage);
            Debug.Log("Lich should be damaged");
            gameObject.SetActive(false);
           
        }
        else
        {
            FacePlayer();
            if(!isAttacking)
            {
                criticalSpot.SetActive(false);
                isAttacking = true;
                canFacePlayer = false;
                InvokeRepeating("RushPlayer", 0, chargeRate);
                Invoke("StopRush", chargeLength);
            }
        }
    }

    public void FacePlayer()
    {
        if(canFacePlayer)
        {
            Vector3 dir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, transform.forward);
        }
    }

    public void RushPlayer()
    {
        criticalSpot.SetActive(true);
        gameObject.transform.Translate(Vector2.up * Time.deltaTime * chargeSpeed);
    }

    public void StopRush()
    {
        CancelInvoke("RushPlayer");
        criticalSpot.SetActive(false);
        canFacePlayer = true;
        //isAttacking = false;

        Invoke("ResetAttack", attackRate);
    }

    public void ResetAttack()
    {
        criticalSpot.SetActive(false);
        canFacePlayer = true;
        isAttacking = false;
    }



    public SpriteRenderer GetColorInfo()
    {
        return colorInfo;
    }

    public void ChildResetColor()
    {
        Invoke("ResetColor", .50f);
    }

    private void ResetColor()
    {
        colorInfo.color = Color.white;
    }
}
