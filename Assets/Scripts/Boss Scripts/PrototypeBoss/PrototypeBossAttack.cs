using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeBossAttack : BossAttacks
{
    private BossInfo bossInfoInfo;
    private BossAttacks bossAttacksInfo;
    private BossMovement bossMovement;
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
    public float chargeSpeed = 45f;
    public bool isCharging = false;

    // Use this for initialization
    void Start ()
    {
        bossMovement = gameObject.GetComponent<BossMovement>();
        bossInfoInfo = gameObject.GetComponent<BossInfo>();
        bossAttacksInfo = gameObject.GetComponent<BossAttacks>();
        player = GameObject.Find("Player");
        spriteInfo = bossArt.gameObject.GetComponent<SpriteRenderer>();
        // bossAnimatorInfo = gameObject.GetComponent<Animator>();
    }
	



    public void Attack(int attackNumber)
    {
       // Debug.Log("The PrototypeBossAttack script");
        bossAttacksInfo.isAttacking = true;
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
        spriteInfo.sprite = spinSprite;
        if (bossInfoInfo.isMad)
        {
            spinFireRate = .06f;
            bossMovement.facesPlayer = false;
            Debug.Log("Mad Spin To Win IF worked!");
            GameObject bomb1 = Instantiate(bomb, transform.position, transform.rotation);
            bomb1.transform.Rotate(0, 0, 180);
            GameObject bomb2 = Instantiate(bomb, transform.position, transform.rotation);
            bomb2.transform.Rotate(0, 0, 0);
            GameObject bomb3 = Instantiate(bomb, transform.position, transform.rotation);
            bomb3.transform.Rotate(0, 0, 45);
        }
        else if (bossInfoInfo.isEnraged)
        {
            bossMovement.facesPlayer = false;
            spinFireRate = .01f;
            GameObject bomb4 = Instantiate(bomb, transform.position, transform.rotation);
            bomb4.transform.Rotate(0, 0, -45);
            GameObject bomb5 = Instantiate(bomb, transform.position, transform.rotation);
            bomb5.transform.Rotate(0, 0, 90);
            GameObject bomb6 = Instantiate(bomb, transform.position, transform.rotation);
            bomb6.transform.Rotate(0, 0, -90);
        }

        StartCoroutine(StopAttack(chargeTimeLength));
        InvokeRepeating("SpinToWin", 0, spinFireRate);
        Invoke("CanFacePlayer", spinTimeLength);
    }

    public void CanFacePlayer()
    {
        bossMovement.facesPlayer = true;
    }

    public void SpinToWin()
    {
  
        spinRotationAmount = SPIN_DEFAULT;
     
      
        if(bossInfoInfo.isMad || bossInfoInfo.isEnraged)
        {
            transform.Rotate(0, 0, spinRotationAmount);
        }
      
        Instantiate(fireball, transform.position, transform.rotation);

        //spinRotationAmount = spinDefault;
        
        spinRotationAmount += .05f;
    }

    /// ///////////////////////////////////////ATTACK TWO STUFF

    public void AttackTwo()//Charge
    {
        bossMovement.facesPlayer = false;
        spriteInfo.sprite = chargeSprite;
        InvokeRepeating("ChargeAttack", 0, chargeFireRate);
        StartCoroutine(StopAttack(spinTimeLength));
        Invoke("CanFacePlayer", spinTimeLength);
    }


    public void ChargeAttack()
    {
        isCharging = true;
        Vector3 dir = bossInfoInfo.GetPlayerLocation().transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, transform.forward);
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * chargeSpeed, ForceMode2D.Impulse);
        Invoke("StopMovement", 1);
        if (bossInfoInfo.isMad)
        {
            GameObject bomb1 = Instantiate(bomb, transform.position, transform.rotation);
            bomb1.transform.Rotate(0, 0, 45);
            GameObject bomb2 = Instantiate(bomb, transform.position, transform.rotation);
            bomb2.transform.Rotate(0, 0, -45);

            if (bossInfoInfo.isEnraged)
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
        player.GetComponent<PlayerHealth>().DamagePlayer(10);
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
      
        InvokeRepeating("BombTown", 0, bombFireRate);
        StartCoroutine(StopAttack(bombTimeLength));
    }

    public void BombTown()
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

    /// /////////////////////////////////////// UTILITY STUFF

    public IEnumerator StopAttack(float time)
    {
        yield return new WaitForSeconds(time);
        bossAttacksInfo.EndAttack();
        bossAttacksInfo.isAttacking = false;
        isCharging = false;
        CancelInvoke();
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
    }
}
