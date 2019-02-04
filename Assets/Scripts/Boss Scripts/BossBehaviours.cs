using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviours : MonoBehaviour {
    private BossHealth bossHealthInfo;
    private GameObject player;
    

    public GameObject fireball;
    public GameObject bomb;
    public GameObject megaBomb;
    public GameObject bossArt;
    private SpriteRenderer spriteInfo;
    public Sprite idleSprite;
    public Sprite chargeSprite;
    public Sprite spinSprite;
    private const float SPIN_DEFAULT = 10;
    private bool isCharging = false;

    public bool isActivated = false;

    public float fireBallSpeed = 50f;

    private float actionRate = 3f;
    private float actionTimer = 0f;
    private bool isBusy = false;

    public float spinTimeLength = 8f;
    public float chargeTimeLength = 6f;
    public float bombTimeLength = 4f;
    public float comboTimeLength = 4f;

    public float spinFireRate = .5f;
    public float spinRotationAmount = 10f;

    public float bombFireRate = 3f;

    public float chargeFireRate = 2f;
    public float chargeSpeed = 25f;

    public float comboFireRate = 3f;


    public enum State
    {
        IDLE,
        CHARGE,
        SPIN,
        BOMB,
        COMBINED
    }

    public State state;

	// Use this for initialization
	void Start () 
    {
        //spinDefault = spinRotationAmount;
        player = GameObject.Find("Player");
        bossHealthInfo = gameObject.GetComponent<BossHealth>();
        spriteInfo = bossArt.gameObject.GetComponent<SpriteRenderer>();
        state = State.IDLE;
        StartCoroutine("FSM");
        
	}

    IEnumerator FSM()
    {

        while (bossHealthInfo.isAlive)
        {
           

                //Debug.Log("The Boss's current state is: " + state);
            switch (state)
            {
                case State.IDLE:
                    Idle();
                    break;
                case State.CHARGE:
                    Charge();
                    break;
                case State.SPIN:
                    Spin();
                    break;
                case State.BOMB:
                    Bomb();
                    break;
            }
            yield return null;

            
        }
        
    }

    private void Idle()
    {
        if (bossHealthInfo.isAlive)
        {

            spriteInfo.sprite = idleSprite;
            actionTimer += Time.deltaTime;
            if (actionTimer >= actionRate)
            {
                actionTimer = 0f;

                int randomAction = Random.Range(0, 3);
                //int randomAction = 0;
              
                if (!isBusy && randomAction == 0 && isActivated)
                {
                    isBusy = true;
                    state = State.SPIN;
                }
                else if (!isBusy && randomAction == 1 && isActivated)
                {
                    isBusy = true;
                    state = State.CHARGE;
                }
                else if (!isBusy && randomAction == 2 && isActivated)
                {
                    isBusy = true;
                    state = State.BOMB;
                }
            }
        }
    }

    private void Charge()
    {
        spriteInfo.sprite = chargeSprite;
        if (isBusy)
        {
            isBusy = false;
            isCharging = true;


            InvokeRepeating("ChargeAttack", 0, chargeFireRate);
            Invoke("ResetState", chargeTimeLength);
        }

    }

    private void Spin()
    {
        spriteInfo.sprite = spinSprite;
        if (isBusy)
        {
            isBusy = false;
            // spinRotationAmount = spinDefault;
            if (bossHealthInfo.isFrenzied)
            {
                spinFireRate = .01f;
                GameObject bomb4 = Instantiate(bomb, transform.position, transform.rotation);
                bomb4.transform.Rotate(0, 0, -45);
                GameObject bomb5 = Instantiate(bomb, transform.position, transform.rotation);
                bomb5.transform.Rotate(0, 0, 90);
                GameObject bomb6 = Instantiate(bomb, transform.position, transform.rotation);
                bomb6.transform.Rotate(0, 0, -90);
            }
            if (bossHealthInfo.isMad)
            {
                GameObject bomb1 = Instantiate(bomb, transform.position, transform.rotation);
                bomb1.transform.Rotate(0, 0, 180);
                GameObject bomb2 = Instantiate(bomb, transform.position, transform.rotation);
                bomb2.transform.Rotate(0, 0, 0);
                GameObject bomb3 = Instantiate(bomb, transform.position, transform.rotation);
                bomb3.transform.Rotate(0, 0, 45);
             
            }
            InvokeRepeating("SpinToWin", 0, spinFireRate);
            Invoke("ResetState", spinTimeLength);
            

        }

        
    }

    public void SpinToWin()
    {
       
        transform.Rotate(0, 0, spinRotationAmount);
        Instantiate(fireball, transform.position, transform.rotation);
        
        //spinRotationAmount = spinDefault;
        spinRotationAmount = SPIN_DEFAULT;
        spinRotationAmount += .05f;

    }

    private void Bomb()
    {

        if (isBusy)
        {
            isBusy = false;

            InvokeRepeating("BombTown", 0, bombFireRate);
            Invoke("ResetState", bombTimeLength);
        }

    }


    public void BombTown()
    {
        Vector3 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, transform.forward);
        if(bossHealthInfo.isFrenzied)
        {
            GameObject bomb1 = Instantiate(megaBomb, transform.position, transform.rotation);
            GameObject bomb2 = Instantiate(megaBomb, transform.position, transform.rotation);
          
            bomb2.transform.Rotate(0, 0, 180);
      
        }
        else if(bossHealthInfo.isMad)
        {
            Debug.Log("BOMB IS MAD HAPPENED!");
           GameObject bomb1 = Instantiate(bomb, transform.position, transform.rotation);
           GameObject bomb2 = Instantiate(bomb, transform.position, transform.rotation);
            bomb2.transform.Rotate(0, 0, 90);
           GameObject bomb3 = Instantiate(bomb, transform.position, transform.rotation);
            bomb3.transform.Rotate(0, 0, -90);
        }
        else
        {
            Instantiate(bomb, transform.position, transform.rotation);
        }
      
    }

    private void ResetState()
    {
        isCharging = false;
        CancelInvoke();
        state = State.IDLE;
        //spinRotationAmount = spinDefault;
    }

    ////////////////////////////ACTUAL ATTACKS
    public void ChargeAttack()
    {
        Vector3 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle-90, transform.forward);
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * chargeSpeed, ForceMode2D.Impulse);
        Invoke("StopMovement", 1);
        if(bossHealthInfo.isMad)
        {
            GameObject bomb1 = Instantiate(bomb, transform.position, transform.rotation);
            bomb1.transform.Rotate(0,0,45);
            GameObject bomb2 = Instantiate(bomb, transform.position, transform.rotation);
            bomb2.transform.Rotate(0, 0, -45);
          
            if(bossHealthInfo.isFrenzied)
            {
                GameObject bomb3 = Instantiate(bomb, transform.position, transform.rotation);
                bomb3.transform.Rotate(0, 0, 135);
                GameObject bomb4 = Instantiate(bomb, transform.position, transform.rotation);
                bomb4.transform.Rotate(0, 0, -135);
            }
            
        }
        
    }

    public void StopMovement()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
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
        if(collision.gameObject.tag == "Player")
        {
            if(isCharging)
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


}
