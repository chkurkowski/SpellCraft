using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInfo : MonoBehaviour
{
    private BossAttacks bossAttackInfo;
    private BossMovement bossMovementInfo;
    [HideInInspector]
    public BossHealth bossHealthInfo;

    [SerializeField]
    public bool isMad = false;
    public bool isEnraged = false;
    private Transform playerLocation;
    // Use this for initialization

    [Space(15)]
    public float bossRageLevel = 0f;

    [Space(15)]
    public float bossRageThreshold1 = 33f;
    public float bossRageThreshold2 = 66f;
    

    [Space(15)]
    public float bossStunLevel = 0f;
    public float bossStunThreshold = 100f;
    public float bossStunTime = 3f; 
    public bool  bossIsStunned = false;

    [Space(15)]
    public bool isActivated = false;
    public float agroDistance = 10f;

    public enum RageState
    {
        CALM,
        MAD,
        ENRAGED
    }

    public RageState rageState;

    private void Awake()
    {
        bossHealthInfo = gameObject.GetComponent<BossHealth>();
        //if(!isActivated)
       // {
           //bossHealthInfo.HealthBarParent.SetActive(false);
       // }
    }

    void Start ()
    {
       
        bossAttackInfo = gameObject.GetComponent<BossAttacks>();
        bossMovementInfo = gameObject.GetComponent<BossMovement>();
        playerLocation = GameObject.Find("Player").GetComponent<Transform>();
        rageState = RageState.CALM;

        StartCoroutine("StunTracker");
        StartCoroutine("RageTracker");
       // StartCoroutine("AgroTracker"); // might just do this through camera script!
            
    }

    private void Update()
    {
        playerLocation = GameObject.Find("Player").GetComponent<Transform>();
       if (isActivated)
       {
            bossHealthInfo.HealthBarParent.SetActive(true);
       }
    }

    /// ///////////////////////////////////////STUN STUFF/FUNCTIONS

    IEnumerator StunTracker()
    {
        while (bossHealthInfo.GetAlive())
        {
            if (bossStunLevel >= bossStunThreshold)
            {
                bossStunLevel = 0;
                Invoke("StartStun", 0);
                Invoke("EndStun", bossStunTime);
            }
            yield return null;
        }
    }

    public void StartStun()
    {
        bossIsStunned = true;
        bossAttackInfo.CancelAttack();
        bossMovementInfo.CancelMovement();
    }

    public void EndStun()
    {
        bossStunLevel = 0; ///placed here so that you cant build up stunLevel WHILE the boss is stunned
        bossIsStunned = false;
        bossAttackInfo.ResumeAttack();
        bossMovementInfo.ResumeMovement();
    }

    public void AddStunAmount(float stunAmount)
    {
        bossStunLevel += stunAmount;
    }

    public void RemoveStunAmount(float stunAmount)
    {
        bossStunLevel -=stunAmount;
    }

    public float GetStunAmount()
    {
        return bossStunLevel;
    }

    /// ///////////////////////////////////////RAGE STUFF/FUNCTIONS

    IEnumerator RageTracker()
    {
        while (bossHealthInfo.GetAlive())
        {
            if (bossRageLevel <= bossRageThreshold1)
            {
                isMad = false;
                isEnraged = false;
                rageState = RageState.CALM;
            }
            else if (bossRageLevel < bossRageThreshold2)
            {
                isMad = true;
                isEnraged = false;
                rageState = RageState.MAD;
            }
            else if (bossRageLevel >= bossRageThreshold2)
            {
                isMad = false;
                isEnraged = true;
                rageState = RageState.ENRAGED;
            }

            if (bossRageLevel > 100)
            {
                bossRageLevel = 100;
            }
            if (bossRageLevel < 0)
            {
                bossRageLevel = 0;
            }
            yield return null;
        }
    }

    public void AddRageAmount(float rageAmount)
    {
        bossRageLevel += rageAmount;
    }

    public void RemoveRageAmount(float rageAmount)
    {
        bossRageLevel -= rageAmount;
    }

    public void SetRageAmount(float rageAmount)
    {
        bossRageLevel = rageAmount;
    }

    public float GetRageAmount()
    {
        return bossRageLevel;
    }

    /// ///////////////////////////////////////AGRO STUFF

    IEnumerator AgroTracker()
    {
        while (bossHealthInfo.GetAlive())
        {
            if (Vector2.Distance(transform.position, playerLocation.position) <= agroDistance)
            {
                isActivated = true;
                //Debug.Log(isActivated + " is the value of isActivated");
                StopCoroutine(AgroTracker());
            }
            yield return null;
        }
    }

    public bool GetIsActivated()
    {
        return isActivated;
    }

    public Transform GetPlayerLocation()
    {
        return playerLocation;
    }

    public void ResetBoss()
    {
        isActivated = false;
        rageState = RageState.CALM;
        bossRageLevel = 0;
        bossHealthInfo.bossHealth = bossHealthInfo.bossMaxHealth;
    }

}
