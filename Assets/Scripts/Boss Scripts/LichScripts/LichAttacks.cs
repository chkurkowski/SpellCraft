using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichAttacks : BossAttacks
{
    private BossInfo bossInfoInfo;
    private BossAttacks bossAttacksInfo;
    private Animator lichAnimatorInfo;
    // Use this for initialization
    void Start ()
    {
        bossInfoInfo = gameObject.GetComponent<BossInfo>();
        bossAttacksInfo = gameObject.GetComponent<BossAttacks>();
        lichAnimatorInfo = gameObject.GetComponent<Animator>();
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

    public void AttackOne()
    {
        if (bossInfoInfo.isMad)
        {

        }
        else if (bossInfoInfo.isEnraged)
        {

        }
    }

    public void AttackTwo()
    {
        if (bossInfoInfo.isMad)
        {

        }
        else if (bossInfoInfo.isEnraged)
        {

        }
    }

    public void AttackThree()
    {
        if (bossInfoInfo.isMad)
        {

        }
        else if (bossInfoInfo.isEnraged)
        {

        }
    }

    public void StopAttack()
    {
        bossAttacksInfo.EndAttack();
        bossAttacksInfo.isAttacking = false;
        CancelInvoke();
    }
}
