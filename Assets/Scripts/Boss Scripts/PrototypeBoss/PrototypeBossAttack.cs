using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeBossAttack : BossAttacks
{
    private BossAttacks bossAttacksInfo;
    private GameObject player;
    //private Animator bossAnimatorInfo;

    ///////////////////////////Game Objects
    public GameObject fireball;
    public GameObject bomb;
    public GameObject megaBomb;

    [Space(25)]//////////////Art Stuff
    public GameObject bossArt;
    private SpriteRenderer spriteInfo;
    public Sprite idleSprite;
    public Sprite chargeSprite;
    public Sprite spinSprite;


    private const float SPIN_DEFAULT = 10;

    public float fireBallSpeed = 50f;

    ///////////////////////////Attack Lengths
    [Space(25)]
    public float spinTimeLength = 8f;
    public float chargeTimeLength = 6f;
    public float bombTimeLength = 4f;
    public float comboTimeLength = 4f;

    [Space(25)]
    public float spinFireRate = .5f;
    public float spinRotationAmount = 10f;
    [Space(25)]
    public float bombFireRate = 3f;
    [Space(25)]
    public float chargeFireRate = 2f;
    public float chargeSpeed = 25f;
    public bool isCharging = false;

    // Use this for initialization
    void Start ()
    {
        bossAttacksInfo = gameObject.GetComponent<BossAttacks>();
        player = GameObject.Find("Player");
        // bossAnimatorInfo = gameObject.GetComponent<Animator>();
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

 
    /// ///////////////////////////////////////ATTACK ONE STUFF
 
    public void AttackOne()//Spin to Win
    {
        isAttacking = true;
        spriteInfo.sprite = spinSprite;
        InvokeRepeating("SpinToWin", 0, spinFireRate);
        Invoke("StopAttacking", spinTimeLength);
    }


    public void SpinToWin()
    {

        transform.Rotate(0, 0, spinRotationAmount);
        Instantiate(fireball, transform.position, transform.rotation);

        //spinRotationAmount = spinDefault;
        spinRotationAmount = SPIN_DEFAULT;
        spinRotationAmount += .05f;

        if (bossAttacksInfo.rageState == RageState.MAD)
        {
            //invoke attack version 2
        }
        else if (bossAttacksInfo.rageState == RageState.ENRAGED)
        {
            //invoke attack version 3
        }
    }

    /// ///////////////////////////////////////ATTACK TWO STUFF

    public void AttackTwo()//Charge
    {
        isAttacking = true;
        InvokeRepeating("ChargeAttack", 0, chargeFireRate);
        Invoke("StopAttack", chargeTimeLength);
    }


    public void ChargeAttack()
    {
        isCharging = true;
        Vector3 dir = playerLocation.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, transform.forward);
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * chargeSpeed, ForceMode2D.Impulse);
        Invoke("StopMovement", 1);
        if (bossAttacksInfo.rageState == RageState.MAD)
        {
            GameObject bomb1 = Instantiate(bomb, transform.position, transform.rotation);
            bomb1.transform.Rotate(0, 0, 45);
            GameObject bomb2 = Instantiate(bomb, transform.position, transform.rotation);
            bomb2.transform.Rotate(0, 0, -45);

            if (bossAttacksInfo.rageState == RageState.ENRAGED)
            {
                GameObject bomb3 = Instantiate(bomb, transform.position, transform.rotation);
                bomb3.transform.Rotate(0, 0, 135);
                GameObject bomb4 = Instantiate(bomb, transform.position, transform.rotation);
                bomb4.transform.Rotate(0, 0, -135);
            }

        }

    }

    public void MeleeDamage()
    {
        player.GetComponent<PlayerHealth>().playerHealth -= 10;
        player.GetComponent<PlayerHealth>().playerHealthBar.fillAmount -= .10f;
    }

    public void SimulacrumMelee()
    {
        GameObject.FindWithTag("Simulacrum").GetComponent<SimulacrumAbilities>().AbsorbDamage(10);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isCharging)
            {
                InvokeRepeating("MeleeDamage", 0, .5f);
            }
        }
        else if (collision.gameObject.tag == "Simulacrum")
        {
            if (isCharging)
            {
                InvokeRepeating("SimulacrumMelee", 0, .5f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Simulacrum")
        {
            CancelInvoke("MeleeDamage");
        }
    }

    public void StopMovement()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    /// ///////////////////////////////////////ATTACK THREE STUFF

    public void AttackThree()//Bomb
    {
        isAttacking = true;
        InvokeRepeating("BombTown", 0, bombFireRate);
        Invoke("StopAttack", bombTimeLength);
    }

    public void BombTown()
    {
        Vector3 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, transform.forward);
        Instantiate(bomb, transform.position, transform.rotation);
        if (bossAttacksInfo.rageState == RageState.MAD)
        {
            GameObject bomb1 = Instantiate(bomb, transform.position, transform.rotation);
            GameObject bomb2 = Instantiate(bomb, transform.position, transform.rotation);
            bomb2.transform.Rotate(0, 0, 90);
            GameObject bomb3 = Instantiate(bomb, transform.position, transform.rotation);
            bomb3.transform.Rotate(0, 0, -90);
        }
        if (bossAttacksInfo.rageState == RageState.ENRAGED)
        {
            GameObject bomb1 = Instantiate(megaBomb, transform.position, transform.rotation);
            GameObject bomb2 = Instantiate(megaBomb, transform.position, transform.rotation);
            GameObject bomb3 = Instantiate(megaBomb, transform.position, transform.rotation);

            bomb2.transform.Rotate(0, 0, 120);
            bomb3.transform.Rotate(0, 0, 240);
        }

    }

    /// /////////////////////////////////////// UTILITY STUFF

    public void StopAttack()
    {
        isCharging = false;
        isAttacking = false;
        CancelInvoke();
    }
}
