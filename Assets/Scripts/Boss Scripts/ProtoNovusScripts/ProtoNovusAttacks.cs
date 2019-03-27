using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoNovusAttacks : MonoBehaviour
{
    private BossHealth bossHealthInfo;
    private BossInfo bossInfoInfo;
    private BossAttacks bossAttacksInfo;
    private Animator pylonAnimatorInfo;
    private ProtoNovusMovement protoNovusMovementInfo;
    public bool isPlayingMusic = false;
    public AudioSource bossMusic;

    //Animator Variables
    private bool canStopAttack = true; // set to true at the start of each attack 

    ////// ////////variables used to lookat stuff
    private Vector3 vectorToTarget;
    private float angle;
    private GameObject player;
    private Quaternion rotAngle;

    [Tooltip("How much the Pylon rotates their body.")]
    public float spinRotationAmount = 0.1f;

    [Tooltip("How often per second the Pylon rotates their body by the spinRotationAmount increment.")]
    public float spinRotationRate = 0.1f;


    // public float laserProjectileDamageSetter = 1f;
    [Space(10)]
    [Header("Attack One Info")]

    [Tooltip("How fast the laser rotates to the player.")]
    public float laserLookAtSpeed;
    [Tooltip("How long the laser is active for. After those seconds are up, the attack is stopped.")]
    public float laserAttackDuration = 5f;
    public GameObject laserMuzzleOne;
    public GameObject shieldOne;
    public GameObject shieldTwo;
    public GameObject reflectShieldOne;
    public GameObject reflectShieldTwo;


    [Space(30)]
    [Header("Attack Two Info")]

    [Space(10)]
    [Header("Bomb Variables")]
    public GameObject fireball;
    public GameObject bomb;
    public GameObject megaBomb;
    public float bombFireRate = 3f;
    public float bombFireRateOriginal;
    public float bombTimeLength = 4f;

    [Space(30)]
    [Header("Attack 3 Info")]
    public GameObject pylonParent;
    public float pPRotateSpeed;
    public float pPRotateAmount;
    [Space(15)]
    public GameObject attackThreeExplosion;
    public GameObject explodingPylonOne;
    public GameObject explodingPylonTwo;
    public GameObject explodingPylonThree;
    public GameObject explodingPylonFour;
    public Transform explodingPylonSpawnOne;
    public Transform explodingPylonSpawnTwo;
    public Transform explodingPylonSpawnThree;
    public Transform explodingPylonSpawnFour;
    public int activeExplodingPylons = 0;

    [Space(30)]
    [Header("Audio Info")]
    public AudioSource laserAudioSource;
    //public AudioSource laserShardsAudioSource;
    //public AudioSource vortexAudioSource;

    void Start()
    {
        player = GameObject.Find("Player");
        bossInfoInfo = gameObject.GetComponent<BossInfo>();
        protoNovusMovementInfo = gameObject.GetComponent<ProtoNovusMovement>();
        bossAttacksInfo = gameObject.GetComponent<BossAttacks>();
        pylonAnimatorInfo = GameObject.Find("PylonBossArt").gameObject.GetComponent<Animator>();
        bossHealthInfo = gameObject.GetComponent<BossHealth>();

        laserMuzzleOne.SetActive(false);
        shieldOne.SetActive(false);
        shieldTwo.SetActive(false);
        reflectShieldOne.SetActive(false);
        reflectShieldTwo.SetActive(false);

        bombFireRateOriginal = bombFireRate;

        pylonParent.transform.eulerAngles = new Vector3(0, 0, 0);

        explodingPylonOne.transform.position = explodingPylonSpawnOne.position;
        explodingPylonOne.SetActive(false);

        explodingPylonTwo.transform.position = explodingPylonSpawnTwo.position;
        explodingPylonTwo.SetActive(false);

        explodingPylonThree.transform.position = explodingPylonSpawnThree.position;
        explodingPylonThree.SetActive(false);

        explodingPylonFour.transform.position = explodingPylonSpawnFour.position;
        explodingPylonFour.SetActive(false);

    }

    private void Update()
    {
        if (bossInfoInfo.isActivated && !isPlayingMusic)
        {
            isPlayingMusic = true;
            bossMusic.Play();
        }
        else if (bossHealthInfo.bossHealth <= 0)
        {
            bossMusic.Stop();
            isPlayingMusic = true;
        }
        else if (!bossInfoInfo.isActivated)
        {
            bossMusic.Stop();
            isPlayingMusic = false;
        }
        if (!bossAttacksInfo.isAttacking)
        {
            pylonAnimatorInfo.SetBool("attackOneEnd", false);
            pylonAnimatorInfo.SetBool("attackTwoEnd", false);
            pylonAnimatorInfo.SetBool("attackThreeEnd", false);
        }
    }

    #region Attack

    public void Attack(int attackNumber)
    {
        canStopAttack = true;
        switch (attackNumber)
        {
            case 0:

                Debug.Log("An incorrect attackNumber was passed as 0");
                break;

            case 1:
                pylonAnimatorInfo.SetBool("attackOneStart", true);
                pylonAnimatorInfo.SetBool("attackOneEnd", false);
              //  AttackOne();
                break;

            case 2:
                pylonAnimatorInfo.SetBool("attackTwoStart", true);
                pylonAnimatorInfo.SetBool("attackTwoEnd", false);
             //   AttackTwo();
                break;

            case 3:
                pylonAnimatorInfo.SetBool("attackThreeStart", true);
                pylonAnimatorInfo.SetBool("attackThreeEnd", false);
               // AttackThree();
                break;
        }
    }

    #endregion

    #region AttackOne

    public void AttackOne()//Laser Beam
    {
        
            if (!bossInfoInfo.isMad && !bossInfoInfo.isEnraged)
            {
                laserMuzzleOne.SetActive(true);

                laserAudioSource.Play();

                InvokeRepeating("SlowRotateToPlayer", 0, spinRotationRate);
            }
            else if (bossInfoInfo.isMad)
            {
                laserMuzzleOne.SetActive(true);

                laserAudioSource.Play();

                shieldOne.SetActive(true);
                shieldTwo.SetActive(true);
                InvokeRepeating("SlowRotateToPlayer", 0, spinRotationRate);
            }
            else if (bossInfoInfo.isEnraged)
            {
                laserAudioSource.Play();

                laserMuzzleOne.SetActive(true);
                reflectShieldOne.SetActive(true);
                reflectShieldTwo.SetActive(true);

                InvokeRepeating("SlowRotateToPlayer", 0, spinRotationRate);
            }
            Invoke("StopAttack", laserAttackDuration);
        
       
    }

    #endregion

    #region AttackTwo

    public void AttackTwo()//Self Explosion
    {
        if(!bossInfoInfo.isMad && !bossInfoInfo.isEnraged)
        {
            bombFireRate = bombFireRateOriginal;
            InvokeRepeating("BombTown", 0, bombFireRate);//see if this works!
        }
        else if(bossInfoInfo.isMad)
        {
            InvokeRepeating("BombTown", 0, bombFireRate*2);
            bombFireRate = bombFireRateOriginal;//see if this works!
        }
        else if(bossInfoInfo.isEnraged)
        {
            InvokeRepeating("BombTown", 0, bombFireRate*3);
            bombFireRate = bombFireRateOriginal;//see if this works!
        }
       
        Invoke("StopAttack", bombTimeLength);
    }

    private void BombTown()
    {
     
        Vector3 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, transform.forward);
        Instantiate(bomb, transform.position, transform.rotation);
        if (bossInfoInfo.isMad)
        {
            GameObject bomb1 = Instantiate(bomb, transform.position, transform.rotation);
            GameObject bomb2 = Instantiate(bomb, transform.position, transform.rotation);
            bomb2.transform.Rotate(0, 0, 90);
            GameObject bomb3 = Instantiate(bomb, transform.position, transform.rotation);
            bomb3.transform.Rotate(0, 0, -90);
        }
        if (bossInfoInfo.isEnraged)
        {
            GameObject bomb1 = Instantiate(megaBomb, transform.position, transform.rotation);
            GameObject bomb2 = Instantiate(megaBomb, transform.position, transform.rotation);
            GameObject bomb3 = Instantiate(megaBomb, transform.position, transform.rotation);
            bomb2.transform.Rotate(0, 0, 120);
            bomb3.transform.Rotate(0, 0, 240);
        }
    }

    #endregion

    #region AttackThree

    public void AttackThree()//Energy Veins??
    {
        if (!bossInfoInfo.isMad && !bossInfoInfo.isEnraged)
        {
            laserAudioSource.Play();
            Debug.Log("Calm Attack 3 happened!");
            explodingPylonOne.SetActive(true);
            explodingPylonOne.transform.position = explodingPylonSpawnOne.position;
            explodingPylonOne.transform.rotation = explodingPylonSpawnOne.rotation;
            explodingPylonOne.GetComponent<explodingPylonScript>().SetId(1);
            explodingPylonOne.GetComponent<explodingPylonScript>().pylonHealth = explodingPylonOne.GetComponent<explodingPylonScript>().pylonMaxHealth;

            explodingPylonTwo.SetActive(true);
            explodingPylonTwo.transform.position = explodingPylonSpawnTwo.position;
            explodingPylonTwo.transform.rotation = explodingPylonSpawnTwo.rotation;
            explodingPylonTwo.GetComponent<explodingPylonScript>().SetId(2);
            explodingPylonTwo.GetComponent<explodingPylonScript>().pylonHealth = explodingPylonTwo.GetComponent<explodingPylonScript>().pylonMaxHealth;

            explodingPylonThree.SetActive(true);
            explodingPylonThree.transform.position = explodingPylonSpawnThree.position;
            explodingPylonThree.transform.rotation = explodingPylonSpawnThree.rotation;
            explodingPylonThree.GetComponent<explodingPylonScript>().SetId(3);
            explodingPylonThree.GetComponent<explodingPylonScript>().pylonHealth = explodingPylonThree.GetComponent<explodingPylonScript>().pylonMaxHealth;

            explodingPylonFour.SetActive(true);
            explodingPylonFour.transform.position = explodingPylonSpawnFour.position;
            explodingPylonFour.transform.rotation = explodingPylonSpawnFour.rotation;
            explodingPylonFour.GetComponent<explodingPylonScript>().SetId(4);
            explodingPylonFour.GetComponent<explodingPylonScript>().pylonHealth = explodingPylonFour.GetComponent<explodingPylonScript>().pylonMaxHealth;

            activeExplodingPylons = 4;
            //  Invoke("StopAttack", 0);
            //stop attack is called from inside the exploding pylons IF there are no pylons left
        }
        else if (bossInfoInfo.isMad)
        {
            laserAudioSource.Play();
            //laserShardsAudioSource.Play();

            Debug.Log("Mad Attack 3 happened!");
            explodingPylonOne.SetActive(true);
            explodingPylonOne.transform.position = explodingPylonSpawnOne.position;
            explodingPylonOne.transform.rotation = explodingPylonSpawnOne.rotation;
            explodingPylonOne.GetComponent<explodingPylonScript>().SetId(1);
            explodingPylonOne.GetComponent<explodingPylonScript>().pylonHealth = explodingPylonOne.GetComponent<explodingPylonScript>().pylonMaxHealth;

            explodingPylonTwo.SetActive(true);
            explodingPylonTwo.transform.position = explodingPylonSpawnTwo.position;
            explodingPylonTwo.transform.rotation = explodingPylonSpawnTwo.rotation;
            explodingPylonTwo.GetComponent<explodingPylonScript>().SetId(2);
            explodingPylonTwo.GetComponent<explodingPylonScript>().pylonHealth = explodingPylonTwo.GetComponent<explodingPylonScript>().pylonMaxHealth;

            explodingPylonThree.SetActive(true);
            explodingPylonThree.transform.position = explodingPylonSpawnThree.position;
            explodingPylonThree.transform.rotation = explodingPylonSpawnThree.rotation;
            explodingPylonThree.GetComponent<explodingPylonScript>().SetId(3);
            explodingPylonThree.GetComponent<explodingPylonScript>().pylonHealth = explodingPylonThree.GetComponent<explodingPylonScript>().pylonMaxHealth;

            explodingPylonFour.SetActive(true);
            explodingPylonFour.transform.position = explodingPylonSpawnFour.position;
            explodingPylonFour.transform.rotation = explodingPylonSpawnFour.rotation;
            explodingPylonFour.GetComponent<explodingPylonScript>().SetId(4);
            explodingPylonFour.GetComponent<explodingPylonScript>().pylonHealth = explodingPylonFour.GetComponent<explodingPylonScript>().pylonMaxHealth;

            // InvokeRepeating("PylonRotate", 0, spinRotationRate);
            // InvokeRepeating("SlowRotateToPlayer", 0, spinRotationRate);
            InvokeRepeating("RotatePylonParent", 0, pPRotateSpeed);
            activeExplodingPylons = 4;
            //Invoke("StopAttack", 0);
            //stop attack is called from inside the exploding pylons IF there are no pylons left
        }
        else if (bossInfoInfo.isEnraged)
        {
            laserAudioSource.Play();
          //  laserShardsAudioSource.Play();

            Debug.Log("Enraged Attack 3 happened!");
            explodingPylonOne.SetActive(true);
            explodingPylonOne.transform.position = explodingPylonSpawnOne.position;
            explodingPylonOne.transform.rotation = explodingPylonSpawnOne.rotation;
            explodingPylonOne.GetComponent<explodingPylonScript>().SetId(1);
            explodingPylonOne.GetComponent<explodingPylonScript>().pylonHealth = explodingPylonOne.GetComponent<explodingPylonScript>().pylonMaxHealth;

            explodingPylonTwo.SetActive(true);
            explodingPylonTwo.transform.position = explodingPylonSpawnTwo.position;
            explodingPylonTwo.transform.rotation = explodingPylonSpawnTwo.rotation;
            explodingPylonTwo.GetComponent<explodingPylonScript>().SetId(2);
            explodingPylonTwo.GetComponent<explodingPylonScript>().pylonHealth = explodingPylonTwo.GetComponent<explodingPylonScript>().pylonMaxHealth;

            explodingPylonThree.SetActive(true);
            explodingPylonThree.transform.position = explodingPylonSpawnThree.position;
            explodingPylonThree.transform.rotation = explodingPylonSpawnThree.rotation;
            explodingPylonThree.GetComponent<explodingPylonScript>().SetId(3);
            explodingPylonThree.GetComponent<explodingPylonScript>().pylonHealth = explodingPylonThree.GetComponent<explodingPylonScript>().pylonMaxHealth;

            explodingPylonFour.SetActive(true);
            explodingPylonFour.transform.position = explodingPylonSpawnFour.position;
            explodingPylonFour.transform.rotation = explodingPylonSpawnFour.rotation;
            explodingPylonFour.GetComponent<explodingPylonScript>().SetId(4);
            explodingPylonFour.GetComponent<explodingPylonScript>().pylonHealth = explodingPylonFour.GetComponent<explodingPylonScript>().pylonMaxHealth;

            //  shieldOne.SetActive(true);
            //  shieldTwo.SetActive(true);
            //InvokeRepeating("PylonRotate", 0, spinRotationRate);
            // InvokeRepeating("SlowRotateToPlayer", 0, spinRotationRate);

            InvokeRepeating("RotatePylonParent", 0, pPRotateSpeed);

            activeExplodingPylons = 4;
        }
        
    }

    #endregion

    #region Misc Functions


    public void SlowRotateToPlayer()
    {
        // Debug.Log("Slow rotate was called!");
        vectorToTarget = player.transform.position - transform.position;
        angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        rotAngle = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotAngle, Time.deltaTime * laserLookAtSpeed);
    }

   // public void PylonRotate()
   // {
   //     gameObject.transform.Rotate(0, 0, spinRotationAmount);
   // }

    public void RotatePylonParent()
    {
        pylonParent.transform.Rotate(0, 0, pPRotateAmount);
    }
    #endregion

    #region Attack Three Explosions
    public void AttackThreeExplosionOne()
    {
        Debug.Log("Attack 3 Explode 1 happened!");
        GameObject exp1 = Instantiate(attackThreeExplosion, transform.position, Quaternion.identity);
        exp1.transform.Rotate(0, 0, 0);
        if (bossInfoInfo.isMad)
        {
            GameObject exp1Mad = Instantiate(attackThreeExplosion, transform.position, Quaternion.identity);
            exp1Mad.transform.Rotate(0, 0, 0);
            exp1Mad.GetComponent<PylonEnergyWave>().moveSpeed = 60;
        }
        if (bossInfoInfo.isEnraged)
        {
            GameObject exp1Enraged = Instantiate(attackThreeExplosion, transform.position, Quaternion.identity);
            exp1Enraged.transform.Rotate(0, 0, 0);
            exp1Enraged.GetComponent<PylonEnergyWave>().moveSpeed = 60;

            GameObject exp1EnragedTwo = Instantiate(attackThreeExplosion, transform.position, Quaternion.identity);
            exp1EnragedTwo.transform.Rotate(0, 0, 0);
            exp1EnragedTwo.GetComponent<PylonEnergyWave>().moveSpeed = 100;
        }
        //  exp1.GetComponent<explodingPylonScript>().SetId(1);
        //stop attack is called from inside the exploding pylons IF there are no pylons left
    }
    public void AttackThreeExplosionTwo()
    {
        Debug.Log("Attack 3 Explode 2 happened!");
        GameObject exp2 = Instantiate(attackThreeExplosion, transform.position, Quaternion.identity);
        exp2.transform.Rotate(0, 0, 180);
        if (bossInfoInfo.isMad)
        {
            GameObject exp2Mad = Instantiate(attackThreeExplosion, transform.position, Quaternion.identity);
            exp2Mad.transform.Rotate(0, 0, 180);
            exp2Mad.GetComponent<PylonEnergyWave>().moveSpeed = 60;
        }
        if (bossInfoInfo.isEnraged)
        {
            GameObject exp2Enraged = Instantiate(attackThreeExplosion, transform.position, Quaternion.identity);
            exp2Enraged.transform.Rotate(0, 0, 180);
            exp2Enraged.GetComponent<PylonEnergyWave>().moveSpeed = 60;

            GameObject exp2EnragedTwo = Instantiate(attackThreeExplosion, transform.position, Quaternion.identity);
            exp2EnragedTwo.transform.Rotate(0, 0, 180);
            exp2EnragedTwo.GetComponent<PylonEnergyWave>().moveSpeed = 100;
        }
        // exp2.GetComponent<explodingPylonScript>().SetId(2);
        //stop attack is called from inside the exploding pylons IF there are no pylons left
    }
    public void AttackThreeExplosionThree()
    {
        Debug.Log("Attack 3 Explode 3 happened!");
        GameObject exp3 = Instantiate(attackThreeExplosion, transform.position, Quaternion.identity);
        exp3.transform.Rotate(0, 0, -90);
        if (bossInfoInfo.isMad)
        {
            GameObject exp3Mad = Instantiate(attackThreeExplosion, transform.position, Quaternion.identity);
            exp3Mad.transform.Rotate(0, 0, -90);
            exp3Mad.GetComponent<PylonEnergyWave>().moveSpeed = 60;
        }
        if (bossInfoInfo.isEnraged)
        {
            GameObject exp3Enraged = Instantiate(attackThreeExplosion, transform.position, Quaternion.identity);
            exp3Enraged.transform.Rotate(0, 0, -90);
            exp3Enraged.GetComponent<PylonEnergyWave>().moveSpeed = 60;

            GameObject exp3EnragedTwo = Instantiate(attackThreeExplosion, transform.position, Quaternion.identity);
            exp3EnragedTwo.transform.Rotate(0, 0, -90);
            exp3EnragedTwo.GetComponent<PylonEnergyWave>().moveSpeed = 100;
        }
        //exp3.GetComponent<explodingPylonScript>().SetId(3);
        //stop attack is called from inside the exploding pylons IF there are no pylons left
    }
    public void AttackThreeExplosionFour()
    {
        Debug.Log("Attack 3 Explode 4 happened!");
        GameObject exp4 = Instantiate(attackThreeExplosion, transform.position, Quaternion.identity);
        exp4.transform.Rotate(0, 0, 90);
        if(bossInfoInfo.isMad)
        {
            GameObject exp4Mad = Instantiate(attackThreeExplosion, transform.position, Quaternion.identity);
            exp4Mad.transform.Rotate(0, 0, 90);
            exp4Mad.GetComponent<PylonEnergyWave>().moveSpeed = 60;
        }
        if(bossInfoInfo.isEnraged)
        {
            GameObject exp4Enraged = Instantiate(attackThreeExplosion, transform.position, Quaternion.identity);
            exp4Enraged.transform.Rotate(0, 0, 90);
            exp4Enraged.GetComponent<PylonEnergyWave>().moveSpeed = 60;

            GameObject exp4EnragedTwo = Instantiate(attackThreeExplosion, transform.position, Quaternion.identity);
            exp4EnragedTwo.transform.Rotate(0, 0, 90);
            exp4EnragedTwo.GetComponent<PylonEnergyWave>().moveSpeed = 100;

        }
        // exp4.GetComponent<explodingPylonScript>().SetId(4);
        //stop attack is called from inside the exploding pylons IF there are no pylons left
    }

    #endregion

    #region StopAttack
    public void StopAttack()
    {
        pylonAnimatorInfo.SetBool("attackOneStart", false);
        pylonAnimatorInfo.SetBool("attackTwoStart", false);
        pylonAnimatorInfo.SetBool("attackThreeStart", false);
        pylonAnimatorInfo.SetBool("attackOneEnd", true);
        pylonAnimatorInfo.SetBool("attackTwoEnd", true);
        pylonAnimatorInfo.SetBool("attackThreeEnd", true);

        laserAudioSource.Stop();
        //laserShardsAudioSource.Stop();
        //vortexAudioSource.Stop();

        bossAttacksInfo.EndAttack();
        bossAttacksInfo.isAttacking = false;

        laserMuzzleOne.SetActive(false);
        shieldOne.SetActive(false);
        shieldTwo.SetActive(false);
        reflectShieldOne.GetComponent<PylonReflectShield>().isLasered = false;
        reflectShieldTwo.GetComponent<PylonReflectShield>().isLasered = false;
        reflectShieldOne.SetActive(false);
        reflectShieldTwo.SetActive(false);

        protoNovusMovementInfo.StopLaserAttackMovement();
      

        explodingPylonOne.transform.position = explodingPylonSpawnOne.position;
        explodingPylonOne.SetActive(false);

        explodingPylonTwo.transform.position = explodingPylonSpawnTwo.position;
        explodingPylonTwo.SetActive(false);

        explodingPylonThree.transform.position = explodingPylonSpawnThree.position;
        explodingPylonThree.SetActive(false);

        explodingPylonFour.transform.position = explodingPylonSpawnFour.position;
        explodingPylonFour.SetActive(false);

        CancelInvoke();
    }

    #endregion



}
