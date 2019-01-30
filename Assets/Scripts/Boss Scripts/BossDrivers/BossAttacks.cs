using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : BossInfo
{
    /// <summary>
    /// Lich , Pylon, Alchemist, Charmer, or Reflector
    /// </summary>
    public string bossName = "";

    private BossHealth bossHealthInfo;

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
    

   

    private bool isAttacking = false;
    private bool canAttack = true;

    private float actionTimer = 0f;
    private float actionRate = 3f;

    public enum State
    {
        IDLE,
        CHARGEUP,
        ATTACK,
        CHARGEDOWN,
        STUN,
    }

    public State state;
   



    // Use this for initialization
    void Start()
    {
        BossInitializer(bossName);   
        state = State.IDLE;
        bossHealthInfo = gameObject.GetComponent<BossHealth>();
        StartCoroutine("FSM");
    }



    IEnumerator FSM()
    {
        while (bossHealthInfo.isAlive)
        {
            switch (state)
            {
                case State.IDLE:
                    Idle();
                    break;

                case State.ATTACK:
                    AttackDriver(bossName);
                    break;

            }
        }
            yield return null;
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

        }

    }


    /// ///////////////////////////////////////////////////IDLE!

    public void Idle()
    {
        if (bossHealthInfo.isAlive)
        {
            actionTimer += Time.deltaTime;
            if(actionTimer >= actionRate)
            {
                actionTimer = 0;

                if(!isAttacking && canAttack)
                {
                   state = State.ATTACK;
                }
            }
        }
    }//Idle end

    /// /////////////////////////////////////////////////// ATTACK DRIVER!

    public void AttackDriver(string bossName)
    {
        switch (bossName)
        {
            case "Lich":
                lichAttackInfo.Attack();
                break;

            case "Pylon":
                pylonAttackInfo.Attack();
                break;

            case "Charmer":
                charmerAttackInfo.Attack();
                break;

            case "Reflector":
                reflectorAttackInfo.Attack();
                break;

            case "Alchemist":
                alchemistAttackInfo.Attack();
                break;
        }
    }//AttackDriver end


    /// /////////////////////////////////////////////////// CHARGEUP!

    public void ChargeUp(string bossName)
    {

    }//ChargeUp end


    /// /////////////////////////////////////////////////// WindDown!

}
