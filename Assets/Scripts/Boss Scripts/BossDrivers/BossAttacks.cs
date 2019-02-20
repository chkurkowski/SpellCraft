using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    /// <summary>
    /// Lich , Pylon, Alchemist, Charmer, or Reflector
    /// </summary>
    private string bossName = "";
    private BossInfo bossInfo;
    private BossHealth bossHealthInfo;
    /// <summary>
    /// Shows the previous attack. 0 means invalid attack, 1 is the first attack, 2 is the second attack, 3 is the 3rd
    /// </summary>
    public int previousAttack = 0;

    /// <summary>
    /// Pretty obvious, shows the if the boss is attacking
    /// </summary>
    public bool isAttacking = false;

    private bool canAttack = true;

    private float attackTimer = 0f;
    /// <summary>
    /// The time between attacks. Essentially the attack cooldown.
    /// </summary>
    public float attackRate = 3f;

    ///////////////////////////////////Lich Info
    private LichAttacks lichAttackInfo;


    /////////////////////////////////////Pylon Info
    private PylonAttacks pylonAttackInfo;


    /////////////////////////////////////////Charmer Info
    private CharmerAttacks charmerAttackInfo;


    /////////////////////////////////////////////Reflector Info
    private ReflectorAttacks reflectorAttackInfo;


    /////////////////////////////////////////////////Alchemist Info
    private AlchemistAttack alchemistAttackInfo;

    /////////////////////////////////////////////////PrototypeBoss Info
    private PrototypeBossAttack prototypeBossAttackInfo;



    public enum AttackState
    {
        IDLE,
        ATTACK,
        STUN,
    }

    public AttackState attackState;




    // Use this for initialization
    void Start()
    {
        bossName = gameObject.name;
        bossInfo = gameObject.GetComponent<BossInfo>();
        bossHealthInfo = gameObject.GetComponent<BossHealth>();
        BossInitializer(bossName);
        attackState = AttackState.IDLE;
        StartCoroutine("FSM");
    }



    IEnumerator FSM()
    {
        while (bossHealthInfo.GetAlive())
        {
            switch (attackState)
            {
                case AttackState.IDLE:
                    Idle();
                    break;

                case AttackState.ATTACK:
                    if(canAttack)
                    {
                        AttackDecider();
                        AttackDriver(bossName, previousAttack);
                    }
                    canAttack = false;
                    break;
            }
            yield return null;
        }
        
    }

    /// /////////////////////////////////////////////////// BossName stuff!
    public void BossInitializer(string bossName)
    {
        switch (bossName)
        {
            case "Lich":
                lichAttackInfo = gameObject.GetComponent<LichAttacks>();
                break;

            case "Pylon":
                pylonAttackInfo = gameObject.GetComponent<PylonAttacks>();
                break;

            case "Charmer":
                charmerAttackInfo = gameObject.GetComponent<CharmerAttacks>();
                break;

            case "Reflector":
                reflectorAttackInfo = gameObject.GetComponent<ReflectorAttacks>();
                break;

            case "Alchemist":
                alchemistAttackInfo = gameObject.GetComponent<AlchemistAttack>();
                break;

            case "PrototypeBoss":
                prototypeBossAttackInfo = gameObject.GetComponent<PrototypeBossAttack>();
                break;

        }

    }


    /// ///////////////////////////////////////////////////IDLE!

    public void Idle()
    {
        if (bossHealthInfo.GetAlive())
        {
            if(bossInfo.isActivated)
            {
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackRate)
                {
                    attackTimer = 0;

                    if (!isAttacking && canAttack)
                    {
                        attackState = AttackState.ATTACK;
                    }
                }
            }
            
        }
    }//Idle end

    /////////////////////////////////////////////////////// ATTACK DECIDER!

    public void AttackDecider()
    {
        int randAttack = Random.Range(1, 4);
        if (randAttack == previousAttack)
        {
            AttackDecider();
        }
        else
        {
            previousAttack = randAttack;
        }
    }

    /////////////////////////////////////////////////////// ATTACK DRIVER!

    public void AttackDriver(string bossName, int attackNumber)
    {
        //Debug.Log(bossName + " is the name that was passed");
       // Debug.Log(isAttacking + " is the value of isAttacking");
        switch (bossName)
        {
            case "Lich":
                if(!isAttacking)
                    lichAttackInfo.Attack(attackNumber);
                break;

            case "Pylon":
                if (!isAttacking)
                    pylonAttackInfo.Attack(attackNumber);
                break;

            case "Charmer":
                if (!isAttacking)
                    charmerAttackInfo.Attack(attackNumber);
                break;

            case "Reflector":
                if (!isAttacking)
                    reflectorAttackInfo.Attack(attackNumber);
                break;

            case "Alchemist":
                if (!isAttacking)
                    alchemistAttackInfo.Attack(attackNumber);
                break;

            case "PrototypeBoss":
                if (!isAttacking)
                {
                    //Debug.Log("The BossAttack script");
                    prototypeBossAttackInfo.Attack(attackNumber);
                }
                   
                break;
        }
    }//AttackDriver end

    public void CancelAttack()
    {
        canAttack = false;
        isAttacking = false;

        switch (bossName)
        {
            case "Lich":
                lichAttackInfo.StopAttack();
                break;

            case "Pylon":
                pylonAttackInfo.StopAttack();
                break;

            case "Charmer":
                charmerAttackInfo.StopAttack();
                break;

            case "Reflector":
                reflectorAttackInfo.StopAttack();
                break;

            case "Alchemist":
                alchemistAttackInfo.StopAttack();
                break;
        }
    }

    public void ResumeAttack()
    {
        canAttack = true;
    }


   public void EndAttack()
    {
        attackState = AttackState.IDLE;
        canAttack = true;
        isAttacking = false;
    }


}
