using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInfo : MonoBehaviour
{
    private BossAttacks bossAttackInfo;
    private BossMovement bossMovementInfo;

    [HideInInspector]
    public Transform playerLocation;
    // Use this for initialization

    [Space(15)]
    public float bossRageLevel = 0f;

    [Space(15)]
    public float bossRageThreshold1 = 25f;
    public float bossRageThreshold2 = 50f;
    public float bossRageThreshold3 = 75f;

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

    void Start ()
    {
        bossAttackInfo = gameObject.GetComponent<BossAttacks>();
        bossMovementInfo = gameObject.GetComponent<BossMovement>();
        playerLocation = GameObject.Find("Player").GetComponent<Transform>();
        rageState = RageState.CALM;

        StartCoroutine("StunTracker");
        StartCoroutine("RageTracker");
        StartCoroutine("AgroTracker");
            
    }

    private void Update()
    {
        playerLocation = playerLocation = GameObject.Find("Player").GetComponent<Transform>();
    }

    /// ///////////////////////////////////////STUN STUFF/FUNCTIONS

    IEnumerator StunTracker()
    {

        if (bossStunLevel >= bossStunThreshold)
        {
            bossStunLevel = 0;
            Invoke("StartStun", 0);
            Invoke("EndStun", bossStunTime);
        }

        yield return null;
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
        if (bossRageLevel <= bossRageThreshold1)
        {
            rageState = RageState.CALM;
        }
        else if (bossRageLevel <= bossRageThreshold2)
        {
            rageState = RageState.MAD;
        }
        else if (bossRageLevel <= bossRageThreshold3)
        {
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
        if (Vector2.Distance(transform.position, playerLocation.position) <= agroDistance)
        {
            isActivated = true;
            StopCoroutine(AgroTracker());
        }
        yield return null;
    }


}
