using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviours : MonoBehaviour {
    private BossHealth bossHealthInfo;
    private GameObject player;

    public GameObject fireball;
    public float fireBallSpeed = 50f;

    private float actionRate = 6f;
    private float actionTimer = 0f;
    private bool isBusy = false;

    public float spinTimeLength;
    public float chargeTimeLength;
    public float bombTimeLength;
    public float comboTimeLength;

    public float spinFireRate = 1f;
    public float spinRotationAmount;

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
        state = State.IDLE;
        StartCoroutine("FSM");
        bossHealthInfo = gameObject.GetComponent<BossHealth>();
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
        if(actionTimer == actionRate)
        {
            actionTimer = 0f;
            //int randomAction = Random.Range(0, 4);
            int randomAction = 1;
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
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.down);

        GameObject fb = Instantiate(fireball, transform.position + Vector3.down * 10, Quaternion.identity);
        fb.GetComponent<Rigidbody2D>().velocity = Vector2.down * fireBallSpeed;
    }







}
