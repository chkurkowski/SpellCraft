using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichAttacks : MonoBehaviour
{
    private BossInfo bossInfoInfo;
    private BossHealth bossHealthInfo;
    private BossAttacks bossAttacksInfo;
    private Animator lichAnimatorInfo;

    //audio sources
    public AudioSource hellPortalAudio;
    public AudioSource skeleSpawnAudio;
    public AudioSource golemAudio;
    public AudioSource artilarygolemAudio;
    public AudioSource cloneGolemAudio;



    // Use this for initialization

    [Space(10)]
    [Header("Attack One Info")]
    
    public GameObject golemOne;
    public GameObject golemTwo;
    public GameObject golemThree;
    [Space(20)]
    public GameObject golemHex;
    public Transform golemOneSpawn;
    public Transform golemTwoSpawn;
    public Transform golemThreeSpawn;

    [Space(40)]
    [Header("Attack Two Info")]
    public GameObject corpseHex;

    public GameObject corpsePillarParent;
    public Transform corpseParentSpawn;
    public GameObject corpsePillarArt;

    [Space(40)]
    [Header("Attack Three Info")]
    public GameObject portalHex;
    public GameObject portal;
    public Transform portalSpawn;

    [Space(40)]

    public bool canCastGolem = true;
    public bool canCastPillars = true;
    public bool canCastPortal = true; //resets once all attacks are taken down

    void Start ()
    {
        bossHealthInfo = gameObject.GetComponent<BossHealth>();
        bossInfoInfo = gameObject.GetComponent<BossInfo>();
        bossAttacksInfo = gameObject.GetComponent<BossAttacks>();
        lichAnimatorInfo = gameObject.GetComponent<Animator>();

        DisableObjects();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(!bossHealthInfo.GetAlive())
        {
            DisableObjects();
            bossAttacksInfo.canAttack = false;
        }
		else if(!golemOne.activeSelf && !golemTwo.activeSelf && !golemThree.activeSelf && !corpsePillarParent.activeSelf && !portal.activeSelf)
        {
            canCastGolem = true;
            canCastPillars = true;
            canCastPortal = true;
        }
        
	}

    public void Attack(int attackNumber)
    {
        bossAttacksInfo.EndAttack();
        if (attackNumber == 1)
        {
            if(golemOne.activeSelf || golemTwo.activeSelf || golemThree.activeSelf || !canCastGolem)
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
            if(corpsePillarParent.activeSelf || !canCastPillars)
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
            if(portal.activeSelf || !canCastPortal)
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
                    if(canCastGolem)
                    {
                        canCastGolem = false;
                        AttackOne();
                    }
                    
                }
               
                break;

            case 2:
                if(!corpsePillarParent.activeSelf)
                {
                    if(canCastPillars)
                    {
                        canCastPillars = false;
                        AttackTwo();
                    }
                   
                }
                break;

            case 3:
                if(!portal.activeSelf)
                {
                    if(canCastPortal)
                    {
                        canCastPortal = false;
                        AttackThree();
                    }
                   
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
            golemTwo.transform.position = golemTwoSpawn.position;
        }
        if (bossInfoInfo.isEnraged)
        {
            golemTwo.SetActive(true);
            golemTwo.transform.position = golemTwoSpawn.position;
            artilarygolemAudio.Play();


            golemThree.SetActive(true);
            golemThree.transform.position = golemThreeSpawn.position;
            cloneGolemAudio.Play();

        }
        golemOne.SetActive(true);
        golemOne.transform.position = golemOneSpawn.position;
        golemAudio.Play();
        golemHex.SetActive(true);
    }


    #endregion

    #region AttackTwo
    public void AttackTwo()
    {
        if (bossInfoInfo.isMad)
        {
            corpsePillarParent.GetComponent<CorpsePillarParent>().isSpinning = true;

        }
        if (bossInfoInfo.isEnraged)
        {
            corpsePillarParent.GetComponent<CorpsePillarParent>().isSpinning = true;
            corpsePillarParent.GetComponent<CorpsePillarParent>().isEnraged = true;

        }
        corpsePillarParent.SetActive(true);
        corpseHex.SetActive(true);
        corpsePillarArt.SetActive(true);

    }

    #endregion

    #region AttackThree
    public void AttackThree()
    {
        if (bossInfoInfo.isMad)
        {
           portal.GetComponent<Portal>().isMad = true;
        }
        if (bossInfoInfo.isEnraged)
        {
            portal.GetComponent<Portal>().isMad = true;
            portal.GetComponent<Portal>().isEnraged = true;
        }
        portal.SetActive(true);
        portalHex.SetActive(true);
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

        corpsePillarParent.transform.position = corpseParentSpawn.position;
        corpsePillarParent.SetActive(false);
        corpsePillarArt.SetActive(false);


        portal.transform.position = portalSpawn.position;
        portal.SetActive(false);


        golemHex.SetActive(false);
        corpseHex.SetActive(false);
        portalHex.SetActive(false);
    }

    public void StopAttack()
    {
        bossAttacksInfo.EndAttack();
        bossAttacksInfo.isAttacking = false;
        CancelInvoke();
    }
}
