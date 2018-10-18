using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviours : MonoBehaviour {
    private BossHealth bossHealthInfo;
    private GameObject player;

    public GameObject fireball;
    public GameObject bomb;
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
        player = GameObject.Find("Player");
        bossHealthInfo = gameObject.GetComponent<BossHealth>();
        state = State.IDLE;
        StartCoroutine("FSM");
        
	}

    IEnumerator FSM()
    {
        while (bossHealthInfo.isAlive)
        {
            Debug.Log("The Boss's current state is: " + state);
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
                case State.COMBINED:
                    Combined();
                    break;
            }
            yield return null;
        }
    }

    private void Idle()
    {
        actionTimer += Time.deltaTime;
        if(actionTimer >= actionRate)
        {
            actionTimer = 0f;
            int randomAction = Random.Range(0, 3);
            //int randomAction = 2;////CHANGE THIS BEFORE I GO
            if(!isBusy && randomAction == 0)
            {
                isBusy = true;
                state = State.SPIN;
            }
            else if(!isBusy && randomAction == 1)
            {
                isBusy = true;
                state = State.CHARGE;
            }
            else if(!isBusy && randomAction == 2)
            {
                isBusy = true;
                state = State.BOMB;
            }
            else if(!isBusy && bossHealthInfo.isFrenzied && randomAction >= 3)
            {
                isBusy = true;
                state = State.COMBINED;
            }
        }
    }

    private void Charge()
    {
        if (isBusy)
        {
            isBusy = false;


            InvokeRepeating("ChargeAttack", 0, chargeFireRate);
            Invoke("ResetState", chargeTimeLength);
        }

    }

    private void Spin()
    {
       if(isBusy)
        {
            isBusy = false;

            InvokeRepeating("SpinToWin", 0, spinFireRate);
            Invoke("ResetState", spinTimeLength);

        }

        
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



    private void Combined()
    {
        if (isBusy)
        {
            isBusy = false;

            InvokeRepeating("WomboCombo", 0, comboFireRate);
            Invoke("ResetState", comboTimeLength);
        }


    }

    private void ResetState()
    {
        CancelInvoke();
        state = State.IDLE;
    }




    ////////////////////////////ACTUAL ATTACKS
    public void ChargeAttack()
    {
        Vector3 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle-90, transform.forward);

        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * chargeSpeed, ForceMode2D.Impulse);

        Invoke("StopMovement", 1);
        
    }

    public void StopMovement()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }


    public void SpinToWin()
    {
        transform.Rotate(0, 0, spinRotationAmount);

        Instantiate(fireball, transform.position, transform.rotation);
    }

    public void BombTown()
    {
        Vector3 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, transform.forward);

        Instantiate(bomb, transform.position, transform.rotation);
    }







}
