using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour
{
    public int targetDummyNumber;
    public float dummyHealth;
    public GameObject dummyBullet;
    private bool isAlive = true;
    private TutorialManager tutorialManagerInfo;
    private Transform dummyMuzzle;

	// Use this for initialization
	void Start ()
    {
        tutorialManagerInfo = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();

      
	}
    private void OnEnable()
    {
        if (targetDummyNumber == 2)
        {
            dummyMuzzle = gameObject.transform.GetChild(0).transform;
            if (dummyMuzzle != null)
            {
              //  Debug.Log("Muzzle found!");
                InvokeRepeating("FireBullet", 0, 1);
            }
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    // Update is called once per frame
    void Update ()
    {
        if (dummyHealth <= 0 && isAlive)
        {
            isAlive = false;
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;//destroyed animation here?
            tutorialManagerInfo.NextTutorialStage();

            CancelInvoke();
        }
	}

    private void FireBullet()
    {
        Instantiate(dummyBullet, dummyMuzzle.position, dummyMuzzle.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(targetDummyNumber)
        {
            case 1:
                if(collision.tag == "Projectile")
                {
                    dummyHealth--;
                }
                break;
            case 2:
                if (collision.tag == "Projectile")
                {
                    if(collision.GetComponent<Fireball>().reflected)
                    {
                        dummyHealth--;
                    }
                    
                }
                break;
            case 3:
                if (collision.tag == "Projectile")
                {
                    if (collision.GetComponent<MagicBall>().magicBallDamage > .5)
                    {
                        dummyHealth--;
                    }

                }
                break;
            case 4:
                if (collision.tag == "Projectile")
                {
                    if (collision.GetComponent<MagicBall>().isSimulacrumMissle)
                    {
                        dummyHealth--;
                    }

                }
                break;
        }
    }
}
