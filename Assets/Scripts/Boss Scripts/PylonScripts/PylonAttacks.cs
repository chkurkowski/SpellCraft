using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonAttacks : MonoBehaviour
{
    private BossInfo bossInfoInfo;
    private BossAttacks bossAttacksInfo;
    private Animator pylonAnimatorInfo;
    private PylonMovement pylonMovementInfo;


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
    [Header("Vortex Variables")]
    public GameObject vortex;
    public GameObject microVortex1;
    private Quaternion microVortex1Rotation;
    public GameObject microVortex2;
    private Quaternion microVortex2Rotation;
    private Vector3 vortexSize;
    public float vortexDamage = 25f;
    public float vortexAttackDuration = 3f;

    [Space(30)]
    [Header("LaserShard Variables")]
    public GameObject laserShardOne;
    public GameObject laserShardTwo;
    public GameObject laserShardThree;
    public GameObject laserShardFour;

    [Space(30)]
    [Header("Attack 3 Info")]
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
    // public  float microVortexSpinRate = 1;
    //  public  float microVortexRotateAmount = 1;


    //[Space(30)]

    [Space(30)]
    [Header("Audio Info")]
    public AudioSource laserAudioSource;
    public AudioSource laserShardsAudioSource;
    public AudioSource vortexAudioSource;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("Player");
        bossInfoInfo = gameObject.GetComponent<BossInfo>();
        pylonMovementInfo = gameObject.GetComponent<PylonMovement>();
        bossAttacksInfo = gameObject.GetComponent<BossAttacks>();
        pylonAnimatorInfo = gameObject.GetComponent<Animator>();




        laserMuzzleOne.SetActive(false);
        shieldOne.SetActive(false);
        shieldTwo.SetActive(false);
        reflectShieldOne.SetActive(false);
        reflectShieldTwo.SetActive(false);


        laserShardOne.SetActive(false);
        laserShardTwo.SetActive(false);
        laserShardThree.SetActive(false);
        laserShardFour.SetActive(false);


        microVortex1Rotation = microVortex1.transform.rotation;
       // Debug.Log(microVortex1Rotation);
        microVortex2Rotation = microVortex2.transform.rotation;
      //  Debug.Log(microVortex2Rotation);
        vortexSize = vortex.transform.localScale;
        vortex.SetActive(false);
        microVortex1.SetActive(false);
        microVortex2.SetActive(false);




        explodingPylonOne.transform.position = explodingPylonSpawnOne.position;
        explodingPylonOne.SetActive(false);

        explodingPylonTwo.transform.position = explodingPylonSpawnTwo.position;
        explodingPylonTwo.SetActive(false);

        explodingPylonThree.transform.position = explodingPylonSpawnThree.position;
        explodingPylonThree.SetActive(false);

        explodingPylonFour.transform.position = explodingPylonSpawnFour.position;
        explodingPylonFour.SetActive(false);

    }

    #region Attack

    public void Attack(int attackNumber)
    {
        //attackNumber = 1;//for laser testing
       // attackNumber = 2; // for vortex testing
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
        if (!bossAttacksInfo.isAttacking)
        {
            if (!bossInfoInfo.isMad && !bossInfoInfo.isEnraged)
            {
                vortexAudioSource.Play();

                vortex.SetActive(true);
                microVortex2.SetActive(true);
               // pylonMovementInfo.LaserAttackMovement();
               // InvokeRepeating("GrowVortex", 0, microVortexSpinRate);
                InvokeRepeating("PylonRotate", 0, spinRotationRate);
                Invoke("StopAttack", vortexAttackDuration);

               // microVortex1.transform.rotation = microVortex1Rotation;
              //  microVortex2.transform.rotation = microVortex2Rotation;
            }
            else if (bossInfoInfo.isMad)
            {

                laserShardsAudioSource.Play();
                vortexAudioSource.Play();

                vortex.SetActive(true);
                microVortex1.SetActive(true);
                microVortex2.SetActive(true);
                laserShardOne.SetActive(true);
                laserShardThree.SetActive(true);

                // pylonMovementInfo.LaserAttackMovement();
                // InvokeRepeating("SpinMicroVortex", 0, (microVortexSpinRate));

              //  microVortex1.transform.rotation = microVortex1Rotation;
              //  microVortex2.transform.rotation = microVortex2Rotation;
                InvokeRepeating("PylonRotate", 0, spinRotationRate);
                Invoke("StopAttack", vortexAttackDuration);
            }
            else if (bossInfoInfo.isEnraged)
            {
                laserShardsAudioSource.Play();
                vortexAudioSource.Play();

                vortex.SetActive(true);
                microVortex1.SetActive(true);
                microVortex2.SetActive(true);
                laserShardOne.SetActive(true);
                laserShardTwo.SetActive(true);
                laserShardThree.SetActive(true);
                laserShardFour.SetActive(true);

               // microVortex1.transform.rotation = microVortex1Rotation;
               // microVortex2.transform.rotation = microVortex2Rotation;

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
            //Invoke("StopAttack", 0);
            //stop attack is called from inside the exploding pylons IF there are no pylons left
        }
        else if (bossInfoInfo.isMad)
        {
            laserAudioSource.Play();
            laserShardsAudioSource.Play();

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


            
            laserShardOne.SetActive(true);
            laserShardThree.SetActive(true);
            // InvokeRepeating("PylonRotate", 0, spinRotationRate);
            InvokeRepeating("SlowRotateToPlayer", 0, spinRotationRate);

            activeExplodingPylons = 4;
            // Invoke("StopAttack", 0);
            //stop attack is called from inside the exploding pylons IF there are no pylons left
        }
        else if(bossInfoInfo.isEnraged)
        {
            laserAudioSource.Play();
            laserShardsAudioSource.Play();

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


           
            laserShardOne.SetActive(true);
            laserShardThree.SetActive(true);
            shieldOne.SetActive(true);
            shieldTwo.SetActive(true);
            //InvokeRepeating("PylonRotate", 0, spinRotationRate);
            InvokeRepeating("SlowRotateToPlayer", 0, spinRotationRate);

            activeExplodingPylons = 4;
        }
       // Invoke("StopAttack", 0);
    }

    #endregion

 

    void SpinMicroVortex()
    {
       
            if (!bossInfoInfo.isMad && !bossInfoInfo.isEnraged)
            {
            //Vector3 dir = bossInfoInfo.GetPlayerLocation().transform.position - transform.position;
            //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.AngleAxis(angle - 90, transform.forward);
           // microVortex2.transform.Rotate(0, 0, microVortexRotateAmount);


            vortex.SetActive(true);
            }
            else if (bossInfoInfo.isMad)
            {
          //      microVortex1.transform.Rotate(0, 0, microVortexRotateAmount);
          //  microVortex2.transform.Rotate(0, 0, microVortexRotateAmount);
        }
            else if (bossInfoInfo.isEnraged)
            {
             //   microVortex1.transform.Rotate(0, 0, microVortexRotateAmount);
            //    microVortex2.transform.Rotate(0, 0, microVortexRotateAmount);
            }  
    }

    public void SlowRotateToPlayer()
    {
       // Debug.Log("Slow rotate was called!");
        vectorToTarget = player.transform.position - transform.position;
        angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        rotAngle = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotAngle, Time.deltaTime * laserLookAtSpeed);
    }

    public void PylonRotate()
    { 
            gameObject.transform.Rotate(0, 0, spinRotationAmount);
    }

    public void AttackThreeExplosionOne()
    {
        Debug.Log("Attack 3 Explode 1 happened!");
        GameObject exp1 = Instantiate(attackThreeExplosion, transform.position, Quaternion.identity); 
        exp1.transform.Rotate(0, 0, 0);
      //  exp1.GetComponent<explodingPylonScript>().SetId(1);
        //stop attack is called from inside the exploding pylons IF there are no pylons left
    }
    public void AttackThreeExplosionTwo()
    {
        Debug.Log("Attack 3 Explode 2 happened!");
        GameObject exp2 = Instantiate(attackThreeExplosion, transform.position, Quaternion.identity);    
        exp2.transform.Rotate(0, 0, 180);
       // exp2.GetComponent<explodingPylonScript>().SetId(2);
        //stop attack is called from inside the exploding pylons IF there are no pylons left
    }
    public void AttackThreeExplosionThree()
    {
        Debug.Log("Attack 3 Explode 3 happened!");
        GameObject exp3 = Instantiate(attackThreeExplosion, transform.position, Quaternion.identity); 
        exp3.transform.Rotate(0, 0, -90);
        //exp3.GetComponent<explodingPylonScript>().SetId(3);
        //stop attack is called from inside the exploding pylons IF there are no pylons left
    }
    public void AttackThreeExplosionFour()
    {
        Debug.Log("Attack 3 Explode 4 happened!");
        GameObject exp4 = Instantiate(attackThreeExplosion, transform.position, Quaternion.identity);
        exp4.transform.Rotate(0, 0, 90);
       // exp4.GetComponent<explodingPylonScript>().SetId(4);
        //stop attack is called from inside the exploding pylons IF there are no pylons left
    }


    #region StopAttack
    public void StopAttack()
    {
        laserAudioSource.Stop();
        laserShardsAudioSource.Stop();
        vortexAudioSource.Stop();



        bossAttacksInfo.EndAttack();
        bossAttacksInfo.isAttacking = false;

        laserMuzzleOne.SetActive(false);
        shieldOne.SetActive(false);
        shieldTwo.SetActive(false);
        reflectShieldOne.GetComponent<PylonReflectShield>().isLasered = false;
        reflectShieldTwo.GetComponent<PylonReflectShield>().isLasered = false;
        reflectShieldOne.SetActive(false);
        reflectShieldTwo.SetActive(false);

        laserShardOne.SetActive(false);
        laserShardTwo.SetActive(false);
        laserShardThree.SetActive(false);
        laserShardFour.SetActive(false);

        pylonMovementInfo.StopLaserAttackMovement();
        // microVortex1.transform.rotation = microVortex1Rotation;
        //  microVortex2.transform.rotation = microVortex2Rotation;
        microVortex1.SetActive(false);
        //  microVortex1.transform.rotation = Quaternion.identity;//resets any rotations
        microVortex2.SetActive(false);
        //  microVortex2.transform.rotation = Quaternion.identity;//resets any rotations
        vortex.transform.localScale = vortexSize;
        if (vortex.activeSelf)
        {
            vortex.GetComponent<PylonVortex>().FlushVortex();
            vortex.SetActive(false);
        }

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
