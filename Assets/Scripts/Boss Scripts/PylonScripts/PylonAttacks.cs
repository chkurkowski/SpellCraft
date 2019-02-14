using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonAttacks : BossAttacks
{
    private BossInfo bossInfoInfo;
    private BossAttacks bossAttacksInfo;
    private Animator pylonAnimatorInfo;
    private PylonMovement pylonMovementInfo;

    public float spinRotationAmount = 0.1f;
    public float spinRotationRate = 0.1f;

    public const float laserAttackDurationCONST = 5f;
    public float laserAttackDuration = 5f;
    public float laserProjectileDamageSetter = 1f;
    [Space(10)]
    public GameObject laserMuzzleOne;
    public GameObject laserMuzzleTwo;
    public GameObject laserMuzzleThree;
    public GameObject laserMuzzleFour;
    public GameObject laserMuzzleFive;
    public GameObject laserMuzzleSix;
    public GameObject laserMuzzleSeven;
    public GameObject laserMuzzleEight;
    [Space(30)]
    public GameObject laserShardOne;
    public GameObject laserShardTwo;
    public GameObject laserShardThree;
    public GameObject laserShardFour;


    [Space(30)]
    public GameObject vortex;
    public GameObject microVortex1;
    public GameObject microVortex2;
    private Vector3 vortexSize;
    public float vortexDamage = 25f;
    public float vortexAttackDuration = 3f;
    public  float microVortexSpinRate = 1;
    public  float microVortexRotateAmount = 1;


    //[Space(30)]



    // Use this for initialization
    void Start ()
    {
        bossInfoInfo = gameObject.GetComponent<BossInfo>();
        pylonMovementInfo = gameObject.GetComponent<PylonMovement>();
        bossAttacksInfo = gameObject.GetComponent<BossAttacks>();
        pylonAnimatorInfo = gameObject.GetComponent<Animator>();

        laserMuzzleOne.SetActive(false);
        laserMuzzleTwo.SetActive(false);
        laserMuzzleThree.SetActive(false);
        laserMuzzleFour.SetActive(false);
        laserMuzzleFive.SetActive(false);
        laserMuzzleSix.SetActive(false);
        laserMuzzleSeven.SetActive(false);
        laserMuzzleEight.SetActive(false);


        laserShardOne.SetActive(false);
        laserShardTwo.SetActive(false);
        laserShardThree.SetActive(false);
        laserShardFour.SetActive(false);
    


        vortexSize = vortex.transform.localScale;
        vortex.SetActive(false);
        microVortex1.SetActive(false);
        microVortex2.SetActive(false);
        

 
    }

    #region Attack

    public void Attack(int attackNumber)
    {
        //attackNumber = 1;//for laser testing
        attackNumber = 2; // for vortex testing
        //attackNumber = 3; // for third attack testing
        //attackNumber = Random.Range(1, 3);
        //if(attackNumber >= 2)
        //{
        //    attackNumber = 2;
        //}
        switch (attackNumber)
        {
            case 0:

                Debug.Log("An incorrect attackNumber was passed as 0");
                break;

            case 1:
                AttackOne();
                break;

            case 2:
                AttackTwo();
                break;

            case 3:
                AttackThree();
                break;
        }
    }

    #endregion

    #region AttackOne

    public void AttackOne()//Laser Beam
    {
        if(!bossInfoInfo.isMad && !bossInfoInfo.isEnraged)
        {
            laserMuzzleOne.SetActive(true);
            laserMuzzleFour.SetActive(true);
            //laserAttackDuration = laserAttackDurationCONST;
            //pylonMovementInfo.LaserAttackMovement();
            InvokeRepeating("PylonRotate", 0, spinRotationRate);
        }
       else if (bossInfoInfo.isMad)
        {
            laserMuzzleOne.SetActive(true);
            laserMuzzleTwo.SetActive(true);
            // laserMuzzleThree.SetActive(true);
            laserMuzzleFour.SetActive(true);
            laserMuzzleSix.SetActive(true);
            laserMuzzleSix.SetActive(true);
            // pylonMovementInfo.LaserAttackMovement(); // the number subtracted from the frequency of turns
            InvokeRepeating("PylonRotate", 0, spinRotationRate);
        }
        else if (bossInfoInfo.isEnraged)
        {
            laserMuzzleOne.SetActive(true);
            laserMuzzleTwo.SetActive(true);
            laserMuzzleThree.SetActive(true);
            laserMuzzleFour.SetActive(true);
            laserMuzzleFive.SetActive(true);
            laserMuzzleSix.SetActive(true);
            // pylonMovementInfo.LaserAttackMovement(); // the number subtracted from the frequency of turns
            InvokeRepeating("PylonRotate", 0, spinRotationRate);

        }
        Invoke("StopAttack", laserAttackDuration);
    }

    #endregion

    #region AttackTwo

    public void AttackTwo()//Self Explosion
    {
        if (!bossAttacksInfo.isAttacking)
        {
            if (!bossInfoInfo.isMad && !bossInfoInfo.isEnraged)
            {
                vortex.SetActive(true);
                microVortex2.SetActive(true);
               // pylonMovementInfo.LaserAttackMovement();
               // InvokeRepeating("GrowVortex", 0, microVortexSpinRate);
                InvokeRepeating("PylonRotate", 0, spinRotationRate);
                Invoke("StopAttack", vortexAttackDuration);
            }
            else if (bossInfoInfo.isMad)
            {
                vortex.SetActive(true);
                microVortex1.SetActive(true);
                microVortex2.SetActive(true);
                laserShardOne.SetActive(true);
                laserShardFour.SetActive(true);
                // pylonMovementInfo.LaserAttackMovement();
               // InvokeRepeating("SpinMicroVortex", 0, (microVortexSpinRate));
                InvokeRepeating("PylonRotate", 0, spinRotationRate);
                Invoke("StopAttack", vortexAttackDuration);
            }
            else if (bossInfoInfo.isEnraged)
            {
                vortex.SetActive(true);
                microVortex1.SetActive(true);
                microVortex2.SetActive(true);
                laserShardOne.SetActive(true);
                laserShardTwo.SetActive(true);
                laserShardThree.SetActive(true);
                laserShardFour.SetActive(true);
            
                //pylonMovementInfo.LaserAttackMovement();
                // InvokeRepeating("SpinMicroVortex", 0, (microVortexSpinRate));
                InvokeRepeating("PylonRotate", 0, spinRotationRate);
                Invoke("StopAttack", vortexAttackDuration);
            }
        }
    }

    #endregion

    #region AttackThree

    public void AttackThree()//Energy Veins??
    {
        if (!bossInfoInfo.isMad && !bossInfoInfo.isEnraged)
        {
            Invoke("StopAttack", 0);
        }
        else if (bossInfoInfo.isMad)
        {
            Invoke("StopAttack", 0);
        }
        else if (bossInfoInfo.isEnraged)
        {
            Invoke("StopAttack", 0);
        }
        Invoke("StopAttack", 0);
    }

    #endregion

    #region StopAttack
    public void StopAttack()
    {
        bossAttacksInfo.EndAttack();
        bossAttacksInfo.isAttacking = false;

        laserMuzzleOne.SetActive(false);
        laserMuzzleTwo.SetActive(false);
        laserMuzzleThree.SetActive(false);
        laserMuzzleFour.SetActive(false);
        laserMuzzleFive.SetActive(false);
        laserMuzzleSix.SetActive(false);
        laserMuzzleSeven.SetActive(false);
        laserMuzzleEight.SetActive(false);

        laserShardOne.SetActive(false);
        laserShardTwo.SetActive(false);
        laserShardThree.SetActive(false);
        laserShardFour.SetActive(false);

        pylonMovementInfo.StopLaserAttackMovement();

        microVortex1.SetActive(false);
        microVortex1.transform.rotation = Quaternion.identity;//resets any rotations
        microVortex2.SetActive(false);
        microVortex2.transform.rotation = Quaternion.identity;//resets any rotations
        vortex.transform.localScale = vortexSize;
        if(vortex.activeSelf)
        {
            vortex.GetComponent<PylonVortex>().FlushVortex();
            vortex.SetActive(false);
        }
        
       
        CancelInvoke();
    }

    #endregion

    void SpinMicroVortex()
    {
       
            if (!bossInfoInfo.isMad && !bossInfoInfo.isEnraged)
            {
            //Vector3 dir = bossInfoInfo.GetPlayerLocation().transform.position - transform.position;
            //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.AngleAxis(angle - 90, transform.forward);
            microVortex2.transform.Rotate(0, 0, microVortexRotateAmount);


            vortex.SetActive(true);
            }
            else if (bossInfoInfo.isMad)
            {
                microVortex1.transform.Rotate(0, 0, microVortexRotateAmount);
            microVortex2.transform.Rotate(0, 0, microVortexRotateAmount);
        }
            else if (bossInfoInfo.isEnraged)
            {
                microVortex1.transform.Rotate(0, 0, microVortexRotateAmount);
                microVortex2.transform.Rotate(0, 0, microVortexRotateAmount);
            }  
    }

    public void PylonRotate()
    { 
            gameObject.transform.Rotate(0, 0, spinRotationAmount);
    }


}
