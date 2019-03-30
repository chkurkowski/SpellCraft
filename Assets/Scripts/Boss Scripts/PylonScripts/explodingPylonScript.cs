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
    ProtoNovusAttacks protoNovusInfo;
    private SpriteRenderer colorInfo;
	// Use this for initialization
	void Start ()
    {
      //  lineRender = GetComponent<LineRenderer>();
      //  lineRender.useWorldSpace = true;
        colorInfo = gameObject.GetComponent<SpriteRenderer>();
        protoNovusInfo = GameObject.Find("ProtoNovus").GetComponent<ProtoNovusAttacks>();
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
            protoNovusInfo.activeExplodingPylons -= 1;
            if (protoNovusInfo.activeExplodingPylons <= 0)
            {
                protoNovusInfo.StopAttack();
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
            protoNovusInfo.activeExplodingPylons -= 1;
            if (protoNovusInfo.activeExplodingPylons <= 0)
            {
                protoNovusInfo.StopAttack();
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
                    protoNovusInfo.AttackThreeExplosionOne();
                    break;
                 }
            case 2:
                {
                    protoNovusInfo.AttackThreeExplosionTwo();
                    break;
                }
            case 3:
                {
                    protoNovusInfo.AttackThreeExplosionThree();
                    break;
                }
            case 4:
                {
                    protoNovusInfo.AttackThreeExplosionFour();
                    break;
                }
        }
    }
}
