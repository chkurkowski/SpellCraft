using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : BossInfo
{

    //private BossHealth bossHealthInfo;

    private BossInfo bossInfo;
    private BossAttacks bossAttackInfoInfo;
    public bool canMove = true;

    public bool SHOULDMOVE = true;

    public bool movesRandomly = true;
    public bool facesPlayer = true;
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


    void Start ()
    {
        bossInfo = gameObject.GetComponent<BossInfo>();
        bossAttackInfoInfo = gameObject.GetComponent<BossAttacks>();
        StartCoroutine("Movement");
    }

    private void Update()
    {
        if(bossAttackInfoInfo.isAttacking)
        {
            SHOULDMOVE = false;
            moveState = MoveState.IDLE;
        }
        else if(bossAttackInfoInfo.isAttacking == false)
        {
            SHOULDMOVE = true;
        }
        if(facesPlayer)
        {
            Vector3 dir = bossInfo.GetPlayerLocation().transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, transform.forward);
        }
    }

    IEnumerator Movement()
    {
        while(bossHealthInfo.GetAlive())
        { 
            if (bossInfo.isActivated)
            {
                switch (moveState)
                {
                    case MoveState.IDLE:
                        Idle();
                        break;

                    case MoveState.MOVE:
                        if(!isMoving)
                        {
                            Move();
                        }
                        StartCoroutine(EndMovement());
                        break;

                    case MoveState.STUN:
                        Stun();
                        break;
                }
            }

            yield return null;
        }///end while loop
    }


    /// //////////////////////////////////////IDLE

    public void Idle()
    {
        if (bossHealthInfo.GetAlive())
        {
            isMoving = false;
            CancelInvoke();
            ///also cancel any boss specific movement!
            moveTimer += Time.deltaTime;
            if (moveTimer >= moveRate)
            {
                moveTimer = 0;

                if (canMove && SHOULDMOVE)
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
        isMoving = true;
        if (movesRandomly)
        {
           InvokeRepeating("RandomMovement",0, randomMoveFrequency);
        }
    }

    public void FacePlayer()
    {
        Vector3 dir = bossInfo.GetPlayerLocation().transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, transform.forward);
    }

    public void RandomMovement()
    {
        Vector3 dir = bossInfo.GetPlayerLocation().transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, transform.forward);

        int randDirection = Random.Range(0, 4);
        if(randDirection == 0)
        {
            //Debug.Log("Boss moves towards player.");
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * moveSpeed, ForceMode2D.Impulse);
        }
        else if(randDirection == 1)
        {
            //Debug.Log("Boss moves away from player.");
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * moveSpeed * -1, ForceMode2D.Impulse);
        }
        else if(randDirection == 2)
        {
           //Debug.Log("Boss strafes right.");
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * moveSpeed, ForceMode2D.Impulse);
        }
        else if (randDirection >= 3)
        {
            //Debug.Log("Boss strafes left.");
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * moveSpeed * -1, ForceMode2D.Impulse);
        }

    }

    /// //////////////////////////////////////STUN FUNCTIONS
    
    public void Stun()
    {
        CancelInvoke();
        canMove = false;
        isMoving = false;
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


    IEnumerator EndMovement()
    {
        yield return new WaitForSeconds(moveDuration);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        moveState = MoveState.IDLE;
    }
}
