using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonAttacks : BossAttacks
{
    private BossInfo bossInfoInfo;
    private BossAttacks bossAttacksInfo;
    private Animator pylonAnimatorInfo;
    private PylonMovement pylonMovementInfo;

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
    [Space(30)]
    private GameObject spawnedVortex;
    public GameObject vortex;
    private Vector3 vortexSize;
    public float vortexDamage = 25f;
    public float vortexGrowthRate = .001f;
    public float vortexGrowthAmount = .1f;
    public float vortexRotateAmount = 1f;
    public float vortexAttackDuration = 3f;
    public float vortexGrowthLimit = 20f;
   

    //[Space(30)]



    // Use this for initialization
    void Start ()
    {
        laserMuzzleOne.SetActive(false);
        laserMuzzleTwo.SetActive(false);
        laserMuzzleThree.SetActive(false);
        laserMuzzleFour.SetActive(false);
        laserMuzzleFive.SetActive(false);
        laserMuzzleSix.SetActive(false);
        bossInfoInfo = gameObject.GetComponent<BossInfo>();
        pylonMovementInfo = gameObject.GetComponent<PylonMovement>();
        bossAttacksInfo = gameObject.GetComponent<BossAttacks>();
        pylonAnimatorInfo = gameObject.GetComponent<Animator>();
        spawnedVortex = Instantiate(vortex, transform.position, transform.rotation);
        vortexSize = vortex.transform.localScale;
        spawnedVortex.SetActive(false);
      
    }
	


    public void Attack(int attackNumber)
    {
        //attackNumber = 1;//for laser testing
        attackNumber = 2; // for vortex testing
        //attackNumber = 3; // for third attack testing
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
     
        if(!bossInfoInfo.isMad && !bossInfoInfo.isEnraged)
        {
            laserMuzzleOne.SetActive(true);
            laserMuzzleFour.SetActive(true);
           //laserAttackDuration = laserAttackDurationCONST;
            pylonMovementInfo.LaserAttackMovement(laserAttackDuration);
        }
       else if (bossInfoInfo.isMad)
        {
            laserMuzzleOne.SetActive(true);
            laserMuzzleTwo.SetActive(true);
            laserMuzzleFour.SetActive(true);
            laserMuzzleSix.SetActive(true);
            pylonMovementInfo.LaserAttackMovement(laserAttackDuration); // the number subtracted from the frequency of turns
        }
        else if (bossInfoInfo.isEnraged)
        {
            laserMuzzleOne.SetActive(true);
            laserMuzzleTwo.SetActive(true);
            laserMuzzleThree.SetActive(true);
            laserMuzzleFour.SetActive(true);
            laserMuzzleFive.SetActive(true);
            laserMuzzleSix.SetActive(true);
            pylonMovementInfo.LaserAttackMovement(laserAttackDuration); // the number subtracted from the frequency of turns

        }
        Invoke("StopAttack", laserAttackDuration);
    }

    public void AttackTwo()//Self Explosion
    {
        if (!bossInfoInfo.isMad && !bossInfoInfo.isEnraged)
        {
            spawnedVortex.SetActive(true);
            InvokeRepeating("GrowVortex", 0, vortexGrowthRate);
            Invoke("StopAttack", vortexAttackDuration);
        }
        else if (bossInfoInfo.isMad)
        {

        }
        else if (bossInfoInfo.isEnraged)
        {

        }
    }

    public void AttackThree()//Energy Veins??
    {
        if (!bossInfoInfo.isMad && !bossInfoInfo.isEnraged)
        {

        }
        else if (bossInfoInfo.isMad)
        {

        }
        else if (bossInfoInfo.isEnraged)
        {

        }
    }


    public void StopAttack()
    {
        laserMuzzleOne.SetActive(false);
        laserMuzzleTwo.SetActive(false);
        laserMuzzleThree.SetActive(false);
        laserMuzzleFour.SetActive(false);
        laserMuzzleFive.SetActive(false);
        laserMuzzleSix.SetActive(false);
        bossAttacksInfo.EndAttack();
        bossAttacksInfo.isAttacking = false;
        spawnedVortex.transform.localScale = vortexSize;
        spawnedVortex.SetActive(false);
        CancelInvoke();
    }

    void GrowVortex()
    {
        if(spawnedVortex.transform.localScale.x <= vortexGrowthLimit)
        {
            spawnedVortex.transform.localScale += new Vector3(vortexGrowthAmount, vortexGrowthAmount, 0);
        }
        Vector3 dir = bossInfoInfo.GetPlayerLocation().transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, transform.forward);

        spawnedVortex.transform.Rotate(0, 0, vortexRotateAmount);
    }
}
