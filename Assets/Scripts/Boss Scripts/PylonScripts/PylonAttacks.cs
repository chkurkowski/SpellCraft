using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonAttacks : BossAttacks
{
    private BossAttacks bossAttacksInfo;
    private Animator pylonAnimatorInfo;

    public GameObject laserAttackOne;
    public GameObject laserOneHitpoint;
    public GameObject laserAttackTwo;
    public GameObject laserTwoHitpoint;
    public GameObject laserAttackThree;
    public GameObject laserThreeHitpoint;

    public Transform laserMuzzleOne;
    public Transform laserMuzzleTwo;
    public Transform laserMuzzleThree;
    public Transform laserMuzzleFour;
    public Transform laserMuzzleFive;



    // Use this for initialization
    void Start ()
    {
        bossAttacksInfo = gameObject.GetComponent<BossAttacks>();
        pylonAnimatorInfo = gameObject.GetComponent<Animator>();
    }
	


    public void Attack(int attackNumber)
    {
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


    public void AttackOne()//Laser Beam
    {
        if(bossAttacksInfo.bossRageLevel <= bossAttacksInfo.bossRageThreshold1)
        {
            //invoke attack version 1
        }
        else if(bossAttacksInfo.bossRageLevel <= bossAttacksInfo.bossRageThreshold2)
        {
            //invoke attack version 2
        }
        else if(bossAttacksInfo.bossRageLevel <= bossAttacksInfo.bossRageThreshold3)
        {
            //invoke attack version 3
        }
    }

    public void AttackTwo()//Self Explosion
    {

    }

    public void AttackThree()//Energy Veins??
    {

    }


    public void StopAttack()
    {
        bossAttacksInfo.EndAttack();
        bossAttacksInfo.isAttacking = false;
        CancelInvoke();
    }
}
