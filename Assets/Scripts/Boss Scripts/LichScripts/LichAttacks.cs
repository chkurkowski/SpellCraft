using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichAttacks : BossAttacks
{
    private BossInfo bossInfoInfo;
    private BossAttacks bossAttacksInfo;
    private Animator lichAnimatorInfo;
    // Use this for initialization

    [Space(10)]
    [Header("Attack One Info")]
    public GameObject golemOne;
    public GameObject golemTwo;
    public GameObject golemThree;
    [Space(20)]
    public Transform golemOneSpawn;
    public Transform golemTwoSpawn;
    public Transform golemThreeSpawn;

    [Space(40)]
    [Header("Attack Two Info")]
    public GameObject corpsePillarParent;
    public GameObject corpsePillarOne;
    public GameObject corpsePillarTwo;
    public GameObject corpsePillarThree;
    public Transform corpseOneSpawn;
    public Transform corpseTwoSpawn;
    public Transform corpseThreeSpawn;

    [Space(40)]
    [Header("Attack Three Info")]
    public GameObject portal;
    public Transform portalSpawn;

    void Start ()
    {
        bossInfoInfo = gameObject.GetComponent<BossInfo>();
        bossAttacksInfo = gameObject.GetComponent<BossAttacks>();
        lichAnimatorInfo = gameObject.GetComponent<Animator>();

        DisableObjects();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Attack(int attackNumber)
    {
        if(attackNumber == 1)
        {
            if(golemOne.activeSelf || golemTwo.activeSelf || golemThree.activeSelf)
            {
                if (!corpsePillarParent.activeSelf)
                {
                    attackNumber = 2;
                }
                else if(!portal.activeSelf)
                {
                    attackNumber = 3;
                }
            }
        }//end of check 1

        if(attackNumber == 2)
        {
            if(corpsePillarParent.activeSelf)
            {
                if (!golemOne.activeSelf && !golemTwo.activeSelf && !golemThree.activeSelf)
                {
                    attackNumber = 1;
                }
                else if(!portal.activeSelf)
                {
                    attackNumber = 3;
                }
            }
        }//end of check 2


        if(attackNumber == 3)
        {
            if(portal.activeSelf)
            {
                if (!golemOne.activeSelf && !golemTwo.activeSelf && !golemThree.activeSelf)
                {
                    attackNumber = 1;
                }
                else if(!corpsePillarParent.activeSelf)
                {
                    attackNumber = 2;
                }
            }
        }

        switch (attackNumber)
        {
            case 0:

                Debug.Log("An incorrect attackNumber was passed as 0");
                break;

            case 1:
                if (!golemOne.activeSelf && !golemTwo.activeSelf && !golemThree.activeSelf)
                {
                    AttackOne();
                }
               
                break;

            case 2:
                if(!corpsePillarParent.activeSelf)
                {
                    AttackTwo();
                }
                break;

            case 3:
                if(!portal.activeSelf)
                {
                    AttackThree();
                }
                break;
        }
    }

    #region AttackOne

    public void AttackOne()
    {
        if (bossInfoInfo.isMad)
        {
            golemTwo.SetActive(true);
        }
        else if (bossInfoInfo.isEnraged)
        {
            golemTwo.SetActive(true);
            golemThree.SetActive(true);
        }
        golemOne.SetActive(true);
    }


    #endregion

    #region AttackTwo
    public void AttackTwo()
    {
        corpsePillarParent.SetActive(true);
    }

    #endregion

    #region AttackThree
    public void AttackThree()
    {
        portal.SetActive(true);
    }
    #endregion 

    public void DisableObjects()
    {
        golemOne.transform.position = golemOneSpawn.position;
        golemOne.SetActive(false);
        golemTwo.transform.position = golemTwoSpawn.position;
        golemTwo.SetActive(false);
        golemThree.transform.position = golemThreeSpawn.position;
        golemThree.SetActive(false);

        corpsePillarParent.SetActive(false);

        portal.transform.position = portalSpawn.position;
        portal.SetActive(false);

    }

    public void StopAttack()
    {
        bossAttacksInfo.EndAttack();
        bossAttacksInfo.isAttacking = false;
        CancelInvoke();
    }
}
