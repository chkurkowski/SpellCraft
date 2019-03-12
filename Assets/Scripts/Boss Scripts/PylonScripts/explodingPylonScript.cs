using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explodingPylonScript : MonoBehaviour
{
    //private LineRenderer lineRender;
    //public Transform laserStart;
    public float moveSpeed = 10f;
    public float pylonMaxHealth = 6f;
    public float pylonHealth = 6f; // main player attack deals .5 damage
    private int pylonIdNumber;
    PylonAttacks pylonInfo;
    private SpriteRenderer colorInfo;
	// Use this for initialization
	void Start ()
    {
      //  lineRender = GetComponent<LineRenderer>();
      //  lineRender.useWorldSpace = true;
        colorInfo = gameObject.GetComponent<SpriteRenderer>();
        pylonInfo =  GameObject.Find("Pylon").GetComponent<PylonAttacks>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up);
       // lineRender.SetPosition(0, laserStart.position);
       // lineRender.SetPosition(1, hit.point);
        Debug.DrawLine(transform.position, Vector2.up);
        transform.Translate(Vector2.up * Time.deltaTime * moveSpeed);
        if(pylonHealth <= 0)
        {
            pylonInfo.activeExplodingPylons -= 1;
            if (pylonInfo.activeExplodingPylons <= 0)
            {
                pylonInfo.StopAttack();
            }
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Projectile")
        {
            colorInfo.color = Color.red;
            Invoke("ResetColor", 1);
            pylonHealth -= collision.gameObject.GetComponent<ProjectileDamage>().projectileDamage;
        }


        if(collision.gameObject.tag == "Boss")
        {
            pylonInfo.activeExplodingPylons -= 1;
          
            if (pylonInfo.activeExplodingPylons <= 0)
            {
                pylonInfo.StopAttack();
            }
            ExplodePylon();
            gameObject.SetActive(false);
        }
    }
    private void ResetColor()
    {
        colorInfo.color = Color.white;
    }

    public void SetId(int Id)
    {
        pylonIdNumber = Id;
    }

    public void ExplodePylon()
    {
        switch(pylonIdNumber)
        {
            case 1:
                 {
                    pylonInfo.AttackThreeExplosionOne();
                    break;
                 }
            case 2:
                {
                    pylonInfo.AttackThreeExplosionTwo();
                    break;
                }
            case 3:
                {
                    pylonInfo.AttackThreeExplosionThree();
                    break;
                }
            case 4:
                {
                    pylonInfo.AttackThreeExplosionFour();
                    break;
                }
        }
    }
}
