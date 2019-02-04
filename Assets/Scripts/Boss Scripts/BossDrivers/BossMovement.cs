using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : BossInfo
{

    private BossHealth bossHealthInfo;
    public bool canMove = false;
    public bool movesRandomly = true;
    public bool isMoving = false;
    [Space(25)]
    private float moveTimer = 0f;
    public float moveRate = 3f;
    public float moveSpeed = 10f;
    public float moveDuration = 3f;
    public float randomMoveFrequency = 1f;

    public enum MoveState
    {
        IDLE,
        MOVE,
        STUN,
    }

    public MoveState moveState;

    // Use this for initialization
    void Start ()
    {
        moveState = MoveState.IDLE;
    }
	
	// Update is called once per frame
	void Update ()
    {
    if(isActivated)
        {
            switch (moveState)
            {
                case MoveState.IDLE:
                    Idle();
                    break;

                case MoveState.MOVE:
                    Move();
                    Invoke("Idle", moveDuration);
                    break;

                case MoveState.STUN:
                    Stun();
                    break;
            }
        }
		
	}

    /// //////////////////////////////////////IDLE

    public void Idle()
    {
        if (bossHealthInfo.isAlive)
        {
            moveTimer += Time.deltaTime;
            if (moveTimer >= moveRate)
            {
                moveTimer = 0;

                if (canMove)
                {
                    moveState = MoveState.MOVE;
                }
            }
            else if (moveTimer < moveRate)
            {
                isMoving = false;
                ///stop movement here
                /////isMoving = false might be enough, if i do the movement through an invoke?
                ///might have to do a cancelInvoke("Movement") instead
                ///dont forget, bosses might have their own movements!!
            }
        }
    }

    /// //////////////////////////////////////MOVE FUNCTIONS

    public void Move()
    {
        if(movesRandomly)
        {
           InvokeRepeating("RandomMovement",0, randomMoveFrequency);
        }

    }

    public void RandomMovement()
    {
        Vector3 dir = playerLocation.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, transform.forward);

        int randDirection = Random.Range(0, 4);
        if(randDirection == 0)
        {
            //moves towards player
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * moveSpeed, ForceMode2D.Impulse);
        }
        else if(randDirection == 1)
        {
            //moves away from player
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * moveSpeed * -1, ForceMode2D.Impulse);
        }
        else if(randDirection == 2)
        {
            //strafes right relative to the boss
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * moveSpeed, ForceMode2D.Impulse);
        }
        else if (randDirection >= 3)
        {
            //strafes left relative to the boss
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * moveSpeed * -1, ForceMode2D.Impulse);
        }

    }

    /// //////////////////////////////////////STUN FUNCTIONS
    
    public void Stun()
    {
        canMove = false;
    }

    public void CancelMovement()
    {
        moveState = MoveState.STUN;
    }

    public void ResumeMovement()
    {
        canMove = true;
        moveState = MoveState.IDLE;
    }
}
