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
    private SpriteRenderer colorInfo;

    public bool isNewBoss = false;

    public bool isPlayingMusic = false;
    public AudioSource bossMusic;
    public AudioSource pillarSFX;


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
    private int laserShutDown = 3;
    public GameObject laserMuzzleOne;
    public GameObject laserMuzzleTwo;
    public GameObject laserMuzzleThree;
   
   
   
    public GameObject shieldOne;
    public GameObject shieldTwo;
    public GameObject reflectShieldOne;
    public GameObject reflectShieldTwo;

    public float pillarSpriteOffset = .1f;
    public GameObject healOrb;
    public GameObject pillarSpriteOne;
    public GameObject pillarSpriteTwo;
    public GameObject pillarSpriteThree;
    public GameObject pillarSpriteFour;
    public GameObject pillarPylonOne;
    public GameObject pillarPylonTwo;
    public GameObject pillarPylonThree;
    public GameObject pillarPylonFour;

    private Transform ogPillarSpriteOne;
    private Transform ogPillarSpriteTwo;
    private Transform ogPillarSpriteThree;
    private Transform ogPillarSpriteFour;

    [HideInInspector]
    public bool pillarOneIsUp = false;
    [HideInInspector]
    public bool pillarTwoIsUp = false;
    [HideInInspector]
    public bool pillarThreeIsUp = false;
    [HideInInspector]
    public bool pillarFourIsUp = false;
    
   


    [Space(30)]
    [Header("Attack Two Info")]

    [Space(10)]
    [Header("Bomb Variables")]
    public GameObject fireball;
    public GameObject originalBomb;
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

    public GameObject attack3VFX;
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

    [Space(30)]
    [Header("VFX")]
    public GameObject pillarSmoke;
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

        colorInfo = GameObject.Find("PylonBossArt").gameObject.GetComponent<SpriteRenderer>();

        laserMuzzleOne.SetActive(false);
        laserMuzzleTwo.SetActive(false);
        laserMuzzleThree.SetActive(false);
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
        if(laserShutDown <= 0)
        {
            Debug.Log("ATTACK SHOULD HAVE BEEN STOPPED WITH LASERS");
            StopAttack();
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
            laserShutDown = 4;
            laserMuzzleOne.SetActive(true);

            laserAudioSource.Play();

            InvokeRepeating("SlowRotateToPlayer", 0, spinRotationRate);
        }
        else if (bossInfoInfo.isMad)
        {
            laserShutDown = 4;
            laserMuzzleOne.SetActive(true);
            laserMuzzleTwo.SetActive(true);
            laserMuzzleThree.SetActive(true);
            laserAudioSource.Play();

            shieldOne.SetActive(true);
            shieldTwo.SetActive(true);
            InvokeRepeating("SlowRotateToPlayer", 0, spinRotationRate);
        }
        else if (bossInfoInfo.isEnraged)
        {
            laserShutDown = 4;
            laserAudioSource.Play();
            laserMuzzleOne.SetActive(true);
            laserMuzzleTwo.SetActive(true);
            laserMuzzleThree.SetActive(true);
            reflectShieldOne.SetActive(true);
            reflectShieldTwo.SetActive(true);

            InvokeRepeating("SlowRotateToPlayer", 0, spinRotationRate);
        }
        Invoke("StopAttack", laserAttackDuration);


    }

    public void AttackOneExtra()
    {
        if (isNewBoss)
        {
            pillarSFX.Play();
            InvokeRepeating("MovePillarOne", 0, 0.01f);
            GameObject smoke1 = Instantiate(pillarSmoke, pillarPylonOne.transform.position, pillarPylonOne.transform.rotation);
            smoke1.GetComponent<Animator>().SetBool("hasFallen", false);

            InvokeRepeating("MovePillarTwo", 0, 0.01f);
            GameObject smoke2 = Instantiate(pillarSmoke, pillarPylonTwo.transform.position, pillarPylonTwo.transform.rotation);
            smoke2.GetComponent<Animator>().SetBool("hasFallen", false);

            InvokeRepeating("MovePillarThree", 0, 0.01f);
            GameObject smoke3 = Instantiate(pillarSmoke, pillarPylonThree.transform.position, pillarPylonThree.transform.rotation);
            smoke3.GetComponent<Animator>().SetBool("hasFallen", false);

            InvokeRepeating("MovePillarFour", 0, 0.01f);
            GameObject smoke4 = Instantiate(pillarSmoke, pillarPylonFour.transform.position, pillarPylonFour.transform.rotation);
            smoke4.GetComponent<Animator>().SetBool("hasFallen", false);
        }
    }

    #endregion

    #region AttackTwo

    public void AttackTwo()//Self Explosion
    {
        if (!bossInfoInfo.isMad && !bossInfoInfo.isEnraged)
        {
            bombFireRate = bombFireRateOriginal;
            InvokeRepeating("BombTown", 0, bombFireRate);//see if this works!
        }
        else if (bossInfoInfo.isMad)
        {
            InvokeRepeating("BombTown", 0, bombFireRate);// * 2);
            //bombFireRate = bombFireRateOriginal;//see if this works!
        }
        else if (bossInfoInfo.isEnraged)
        {
            InvokeRepeating("BombTown", 0, bombFireRate);// * 4);
           // bombFireRate = bombFireRateOriginal;//see if this works!
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
            bomb2.transform.Rotate(0, 0, 180);

           // GameObject bomb3 = Instantiate(bomb, transform.position, transform.rotation);
           //bomb3.transform.Rotate(0, 0, -90);
        }
        if (bossInfoInfo.isEnraged)
        {
            GameObject bomb1 = Instantiate(megaBomb, transform.position, transform.rotation);
            GameObject bomb2 = Instantiate(bomb, transform.position, transform.rotation);
           // GameObject bomb2 = Instantiate(megaBomb, transform.position, transform.rotation);
            GameObject bomb3 = Instantiate(megaBomb, transform.position, transform.rotation);
            GameObject bomb4 = Instantiate(megaBomb, transform.position, transform.rotation);
            bomb2.transform.Rotate(0, 0, 180);
            bomb3.transform.Rotate(0, 0, 90);
            bomb4.transform.Rotate(0, 0, -90);

        }
    }

    #endregion

    #region AttackThree

    public void AttackThree()//Energy Veins??
    {
        if (!bossInfoInfo.isMad && !bossInfoInfo.isEnraged)
        {
            laserAudioSource.Play();
            Instantiate(attack3VFX, transform.position, transform.rotation);
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
            Instantiate(attack3VFX, transform.position, transform.rotation);
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
            Instantiate(attack3VFX, transform.position, transform.rotation);
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

    private void MovePillarOne()
    {
        if (pillarSpriteOne.transform.localPosition.y >= .175f)
        {
            CancelInvoke("MovePillarOne");

            return;
        }
        else
        {
            pillarSpriteOne.transform.position += new Vector3(0, pillarSpriteOffset, 0);
            pillarSpriteOne.transform.parent.GetComponent<Collider2D>().enabled = false;
            pillarOneIsUp = true;
            //pillarSFX.Play();
        }
    }

    private void MovePillarTwo()
    {
        if (pillarSpriteTwo.transform.localPosition.y >= .175f)
        {
            CancelInvoke("MovePillarTwo");

            return;
        }
        else
        {
            pillarSpriteTwo.transform.position += new Vector3(0, pillarSpriteOffset, 0);
            pillarSpriteTwo.transform.parent.GetComponent<Collider2D>().enabled = false;
            pillarTwoIsUp = true;
         //   pillarSFX.Play();
        }
    }

    private void MovePillarThree()
    {
        if (pillarSpriteThree.transform.localPosition.y >= .175f)
        {
            CancelInvoke("MovePillarThree");

            return;
        }
        else
        {
            pillarSpriteThree.transform.position += new Vector3(0, pillarSpriteOffset, 0);
            pillarSpriteThree.transform.parent.GetComponent<Collider2D>().enabled = false;
            pillarThreeIsUp = true;
         //   pillarSFX.Play();
        }
    }

    private void MovePillarFour()
    {
        if (pillarSpriteFour.transform.localPosition.y >= .175f)
        {
            CancelInvoke("MovePillarFour");

            return;
        }
        else
        {
            pillarSpriteFour.transform.position += new Vector3(0, pillarSpriteOffset, 0);
            pillarSpriteFour.transform.parent.GetComponent<Collider2D>().enabled = false;
            pillarFourIsUp = true;
          //  pillarSFX.Play();
        }
    }

    private void ResetPillars()
    {
        if (pillarSpriteOne.transform.localPosition.y <= .035f && pillarSpriteTwo.transform.localPosition.y <= .035f && pillarSpriteThree.transform.localPosition.y <= .035f && pillarSpriteFour.transform.localPosition.y <= .035f)
        {
            pillarSpriteOne.transform.parent.GetComponent<Collider2D>().enabled = true;
            pillarSpriteTwo.transform.parent.GetComponent<Collider2D>().enabled = true;
            pillarSpriteThree.transform.parent.GetComponent<Collider2D>().enabled = true;
            pillarSpriteFour.transform.parent.GetComponent<Collider2D>().enabled = true;
            pillarOneIsUp = false;
            pillarTwoIsUp = false;
            pillarThreeIsUp = false;
            pillarFourIsUp = false;


            GameObject smoke1 = Instantiate(pillarSmoke, pillarPylonOne.transform.position, pillarPylonOne.transform.rotation);
            smoke1.GetComponent<Animator>().SetBool("hasFallen", true);
            GameObject smoke2 = Instantiate(pillarSmoke, pillarPylonTwo.transform.position, pillarPylonTwo.transform.rotation);
            smoke2.GetComponent<Animator>().SetBool("hasFallen", true);
            GameObject smoke3 = Instantiate(pillarSmoke, pillarPylonThree.transform.position, pillarPylonThree.transform.rotation);
            smoke3.GetComponent<Animator>().SetBool("hasFallen", true);
            GameObject smoke4 = Instantiate(pillarSmoke, pillarPylonFour.transform.position, pillarPylonFour.transform.rotation);
            smoke4.GetComponent<Animator>().SetBool("hasFallen", true);

            pillarSFX.Play();

            CancelInvoke("ResetPillars");
            return;
        }
        else
        {
            if(pillarOneIsUp)
            {
                pillarSpriteOne.transform.position -= new Vector3(0, pillarSpriteOffset, 0);
            }

            if(pillarTwoIsUp)
            {
                pillarSpriteTwo.transform.position -= new Vector3(0, pillarSpriteOffset, 0);
            }
           
            if(pillarThreeIsUp)
            {
                pillarSpriteThree.transform.position -= new Vector3(0, pillarSpriteOffset, 0);
            }
          
            if(pillarFourIsUp)
            {
                pillarSpriteFour.transform.position -= new Vector3(0, pillarSpriteOffset, 0);
            }
            
        }
    }

    public void ResetPillarOneActual()//called by pylonCoverThingies
    {
        if(pillarOneIsUp)
        {
            InvokeRepeating("ResetPillarOne", 0, 0.005f);
            CancelInvoke("MovePillarOne");
            pillarSFX.Play();
        }
    }

    public void ResetPillarOne()
    {
        if (pillarSpriteOne.transform.localPosition.y <= .035f)
        {
            GameObject healOrbSpawned = Instantiate(healOrb, pillarSpriteOne.transform.position, pillarSpriteOne.transform.rotation);
            healOrbSpawned.transform.Translate(-20, 20, 0);
            pillarSpriteOne.transform.parent.GetComponent<Collider2D>().enabled = true;
            pillarSpriteOne.transform.localPosition = new Vector3(pillarSpriteOne.transform.localPosition.x, .035f, 0);
            pillarOneIsUp = false;


            GameObject smoke1 = Instantiate(pillarSmoke, pillarPylonOne.transform.position, pillarPylonOne.transform.rotation);
            smoke1.GetComponent<Animator>().SetBool("hasFallen", true);

            Debug.Log("Laser shutdown was : " + laserShutDown);
            laserShutDown--;
            Debug.Log("Laser shutdown is : " + laserShutDown);
            CancelInvoke("ResetPillarOne");
           // pillarSFX.Play();
            return;
        }
        else
        {
            pillarSpriteOne.transform.position -= new Vector3(0, pillarSpriteOffset, 0);
        }
    }

    public void ResetPillarTwoActual()//called by pylonCoverThingies
    {
        if(pillarTwoIsUp)
        {
            InvokeRepeating("ResetPillarTwo", 0, 0.005f);
            CancelInvoke("MovePillarTwo");
            pillarSFX.Play();
        }
    }

    public void ResetPillarTwo()
    {
        if (pillarSpriteTwo.transform.localPosition.y <= .035f)
        {
            GameObject healOrbSpawned = Instantiate(healOrb, pillarSpriteTwo.transform.position, pillarSpriteTwo.transform.rotation);
            healOrbSpawned.transform.Translate(20, 20, 0);
            pillarSpriteTwo.transform.parent.GetComponent<Collider2D>().enabled = true;
            pillarTwoIsUp = false;

            GameObject smoke2 = Instantiate(pillarSmoke, pillarPylonTwo.transform.position, pillarPylonTwo.transform.rotation);
            smoke2.GetComponent<Animator>().SetBool("hasFallen", true);

            pillarSpriteTwo.transform.localPosition = new Vector3(pillarSpriteTwo.transform.localPosition.x, .035f, 0);
            Debug.Log("Laser shutdown was : " + laserShutDown);
            laserShutDown--;
            Debug.Log("Laser shutdown is : " + laserShutDown);
            CancelInvoke("ResetPillarTwo");
          //  pillarSFX.Play();
            return;
        }
        else
        {
            pillarSpriteTwo.transform.position -= new Vector3(0, pillarSpriteOffset, 0);
        }
    }

    public void ResetPillarThreeActual()//called by pylonCoverThingies
    {
        if(pillarThreeIsUp)
        {
            InvokeRepeating("ResetPillarThree", 0, 0.005f);
            CancelInvoke("MovePillarThree");
            pillarSFX.Play();
        }
    }

    public void ResetPillarThree()
    {
        if (pillarSpriteThree.transform.localPosition.y <= .035f)
        {
            GameObject healOrbSpawned = Instantiate(healOrb, pillarSpriteThree.transform.position, pillarSpriteThree.transform.rotation);
            healOrbSpawned.transform.Translate(-20, -20, 0);
            pillarSpriteThree.transform.parent.GetComponent<Collider2D>().enabled = true;
            pillarThreeIsUp = false;

            GameObject smoke3 = Instantiate(pillarSmoke, pillarPylonThree.transform.position, pillarPylonThree.transform.rotation);
            smoke3.GetComponent<Animator>().SetBool("hasFallen", true);

            pillarSpriteThree.transform.localPosition = new Vector3(pillarSpriteThree.transform.localPosition.x, .035f, 0);
            Debug.Log("Laser shutdown was : " + laserShutDown);
            laserShutDown--;
            Debug.Log("Laser shutdown is : " + laserShutDown);
            CancelInvoke("ResetPillarThree");
           // pillarSFX.Play();
            return;
        }
        else
        {
            pillarSpriteThree.transform.position -= new Vector3(0, pillarSpriteOffset, 0);
        }
    }


    public void ResetPillarFourActual()//called by pylonCoverThingies
    {
        if(pillarFourIsUp)
        {
            InvokeRepeating("ResetPillarFour", 0, 0.005f);
            CancelInvoke("MovePillarFour");
            pillarSFX.Play();
        }
    }
    public void ResetPillarFour()
    {
        if (pillarSpriteFour.transform.localPosition.y <= .035f)
        {
            GameObject healOrbSpawned = Instantiate(healOrb, pillarSpriteFour.transform.position, pillarSpriteFour.transform.rotation);
            healOrbSpawned.transform.Translate(20, -20, 0);
            pillarSpriteFour.transform.parent.GetComponent<Collider2D>().enabled = true;
            pillarFourIsUp = false;

            GameObject smoke4 = Instantiate(pillarSmoke, pillarPylonFour.transform.position, pillarPylonFour.transform.rotation);
            smoke4.GetComponent<Animator>().SetBool("hasFallen", true);

            pillarSpriteFour.transform.localPosition = new Vector3(pillarSpriteFour.transform.localPosition.x, .035f, 0);
            Debug.Log("Laser shutdown was : " + laserShutDown);
            laserShutDown--;
            Debug.Log("Laser shutdown is : " + laserShutDown);
            CancelInvoke("ResetPillarFour");
           // pillarSFX.Play();
            return;
        }
        else
        {
            pillarSpriteFour.transform.position -= new Vector3(0, pillarSpriteOffset, 0);
        }
    }
    #endregion

    #region Attack Three Explosions
    public void AttackThreeExplosionOne()
    {
        Debug.Log("Attack 3 Explode 1 happened!");
        GameObject exp1 = Instantiate(attackThreeExplosion, transform.position, Quaternion.identity);
       // GameObject exp2 = Instantiate(attackThreeExplosion, transform.position += new Vector3(50,0,0), Quaternion.identity);
       // GameObject exp3 = Instantiate(attackThreeExplosion, transform.position += new Vector3(-50, 0, 0), Quaternion.identity);
        exp1.transform.Rotate(0, 0, 0);
        //exp2.transform.Rotate(0, 0, 0);
      //  exp3.transform.Rotate(0, 0, 0);
        if (bossInfoInfo.isMad)
        {
            GameObject exp1Mad = Instantiate(attackThreeExplosion, transform.position, Quaternion.identity);
         //   GameObject exp2Mad = Instantiate(attackThreeExplosion, transform.position += new Vector3(30, 0, 0), Quaternion.identity);
          //  GameObject exp3Mad = Instantiate(attackThreeExplosion, transform.position += new Vector3(-30, 0, 0), Quaternion.identity);
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
        if (bossInfoInfo.isMad)
        {
            GameObject exp4Mad = Instantiate(attackThreeExplosion, transform.position, Quaternion.identity);
            exp4Mad.transform.Rotate(0, 0, 90);
            exp4Mad.GetComponent<PylonEnergyWave>().moveSpeed = 60;
        }
        if (bossInfoInfo.isEnraged)
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
        if (GameObject.Find("PylonAttack3Art(Clone)") != null)
        {
            Destroy(GameObject.Find("PylonAttack3Art(Clone)"));
        }
        laserShutDown = 3;
      
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
        laserMuzzleTwo.SetActive(false);
        laserMuzzleThree.SetActive(false);
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
        if (isNewBoss)
        {
            InvokeRepeating("ResetPillars", 0, 0.005f);
        }
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            colorInfo.color = Color.red;
            Invoke("ResetColor", 0.50f);
        }

    }


    private void ResetColor()
    {
        colorInfo.color = Color.white;
    }

}
