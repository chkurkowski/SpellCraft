using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemistAttack : MonoBehaviour
{
    private BossAttacks bossAttacksInfo;
    private Animator alchemistAnimatorInfo;

    // Use this for initialization
    void Start ()
    {
        bossAttacksInfo = gameObject.GetComponent<BossAttacks>();
        alchemistAnimatorInfo = gameObject.GetComponent<Animator>();
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



        bossAttacksInfo.isAttacking = false;
    }

    public void AttackTwo()
    {




        bossAttacksInfo.isAttacking = false;
    }

    public void AttackThree()
    {




        bossAttacksInfo.isAttacking = false;
    }

}
