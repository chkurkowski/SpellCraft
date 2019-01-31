﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonAttacks : MonoBehaviour
{
    private BossAttacks bossAttacksInfo;
    private Animator pylonAnimatorInfo;
  

    // Use this for initialization
    void Start ()
    {
        bossAttacksInfo = gameObject.GetComponent<BossAttacks>();
        pylonAnimatorInfo = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
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
        else if(bossAttacksInfo.bossRageLevel <= bossAttacksInfo.bosssRageThreshold3)
        {
            //invoke attack version 3
        }


        bossAttacksInfo.isAttacking = false;
    }

    public void AttackTwo()//Self Explosion
    {



        bossAttacksInfo.isAttacking = false;
    }

    public void AttackThree()//Energy Veins??
    {



        bossAttacksInfo.isAttacking = false;
    }

}
