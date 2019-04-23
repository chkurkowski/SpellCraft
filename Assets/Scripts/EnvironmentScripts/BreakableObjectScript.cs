using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObjectScript : MonoBehaviour
{
    public float objectHealth = 6f;
    public bool isReinforced = false;
    public bool isTutorialIncrementor = false;
    public bool isAttackSimDummy = false;
    public bool isSplitSimDummy = false;
    public bool isAbsorbSimDummy = false;
    public bool isMultiDestroyDummy = false;
    private SpriteRenderer colorInfo;
    public bool canMoveUpDown = false;
    public bool canMoveLeftRight = false;
    public bool isReflectable = false;
    public bool isSwitch = false;
    public float moveRate = 1f;
    public float moveDistance = 1f;
    private int moveDirection = 1;
    public Transform topLimit;
    public Transform bottomLimit;
    public Transform leftLimit;
    public Transform rightLimit;

    public GameObject switchObject;

    private TutorialManager tutorialInfo;

    private bool isFirstDummy = false;

    public GameObject multiDestroyDummy1;
    public GameObject multiDestroyDummy2;


    // Use this for initialization
    void Start()
    {
        tutorialInfo = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
        colorInfo = gameObject.GetComponent<SpriteRenderer>();
        if (canMoveUpDown)
        {
            canMoveLeftRight = false;
            InvokeRepeating("MoveUpDown", 0, moveRate);
        }
        if (canMoveLeftRight)
        {
            canMoveUpDown = false;
            InvokeRepeating("MoveLeftRight", 0, moveRate);
        }
        if(gameObject.name == "DestroyableDoorFirst")
        {
            isFirstDummy = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (objectHealth <= 0)
        {
            if(isFirstDummy)
            {
                tutorialInfo.tutorialStage = 3;
            }

            if(isMultiDestroyDummy)
            {
                if(multiDestroyDummy1 != null)
                {
                    Destroy(multiDestroyDummy1.gameObject);
                }

                if(multiDestroyDummy2 != null)
                {
                    Destroy(multiDestroyDummy2.gameObject);
                }
            }
                 
            CancelInvoke();
            if (isSwitch)
            {
                if (switchObject != null)
                {
                    Destroy(switchObject.gameObject);//play destroy or door open anim here;
                }

            }
            if (isTutorialIncrementor)
            {
                tutorialInfo.NextTutorialStage();
            }
            Destroy(gameObject);//play shatter anim here
        }
    }

    private void MoveUpDown()
    {
        if (gameObject.transform.position.y > topLimit.position.y)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, topLimit.position.y, gameObject.transform.position.z);
            moveDirection *= -1;
        }
        if (gameObject.transform.position.y < bottomLimit.position.y)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, bottomLimit.position.y, gameObject.transform.position.z);
            moveDirection *= -1;
        }
        gameObject.transform.Translate(0, moveDistance * moveDirection, 0);
    }

    private void MoveLeftRight()
    {
        if (gameObject.transform.position.x > rightLimit.position.x)
        {
            gameObject.transform.position = new Vector3(rightLimit.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            moveDirection *= -1;
        }
        if (gameObject.transform.position.x < leftLimit.position.x)
        {
            gameObject.transform.position = new Vector3(leftLimit.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            moveDirection *= -1;
        }
        gameObject.transform.Translate(moveDistance * moveDirection, 0, 0);

    }


    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Projectile")
        {
            //if(!isReflectable)
            //{
            //    if (!isReinforced)
            //    {
            //        objectHealth--;
            //        colorInfo.color = Color.red;
            //        Invoke("ResetColor", 0.50f);
            //    }
            //    else if (isReinforced)
            //    {
            //        if (trig.GetComponent<MagicBall>().magicBallDamage > .5)
            //        {
            //            objectHealth--;
            //            colorInfo.color = Color.red;
            //            Invoke("ResetColor", 0.50f);
            //        }
            //    }
            //}
            //else if(isReflectable)
            //{
            //    if (trig.GetComponent<Fireball>().reflected)
            //    {
            //        objectHealth--;
            //    }
            //}
            if (isReflectable)
            {
                if (trig.GetComponent<Fireball>().reflected)
                {
                    objectHealth--;
                    colorInfo.color = Color.red;
                    Invoke("ResetColor", 0.50f);
                }
            }
            else if (isAttackSimDummy)
            {
                if (trig.GetComponent<MagicBall>().isAttackSimMissile)
                {
                    objectHealth--;
                    colorInfo.color = Color.red;
                    Invoke("ResetColor", 0.50f);
                }
            }
            else if (isAbsorbSimDummy)
            {
                if (trig.GetComponent<MagicBall>().isAbsorbSimMissile)
                {
                    objectHealth--;
                    colorInfo.color = Color.red;
                    Invoke("ResetColor", 0.50f);
                }
            }
            else if (isSplitSimDummy)
            {
                if (trig.GetComponent<MagicBall>().isSplitSimMissile)
                {
                    objectHealth--;
                    colorInfo.color = Color.red;
                    Invoke("ResetColor", 0.50f);
                }
            }
            else if (isReinforced)
            {
                if (trig.GetComponent<MagicBall>().magicBallDamage > .5f)
                {
                    objectHealth--;
                    colorInfo.color = Color.red;
                    Invoke("ResetColor", 0.50f);
                }
            }
            else
            {
                objectHealth--;
                colorInfo.color = Color.red;
                Invoke("ResetColor", 0.50f);
            }

        }
    }


    private void ResetColor()
    {
        colorInfo.color = Color.white;
    }
}
