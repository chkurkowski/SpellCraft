using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTutorialScript : MonoBehaviour
{

    private LineRenderer lineRender;
    public Transform laserHit;
    private Transform reflectHit;
    public float laserDamage = .1f;
    public float laserDamageToBoss = .05f;

    // Use this for initialization
    void Awake ()
    {
        lineRender = GetComponent<LineRenderer>();
        lineRender.useWorldSpace = true;
        // reflectHit = GameObject.Find("Reflect").GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);
       // Debug.Log(hit.transform.name);
       // Debug.DrawLine(transform.position, transform.up);
        laserHit.position = hit.point;//makes the direction object(laserHit) move with the raycast
        lineRender.SetPosition(0, transform.position);
        lineRender.SetPosition(1, laserHit.position);
        //Debug.Log(hit.transform.tag);
       //if (hit.transform.tag == "Reflect")
       // {
           // Debug.Log("reflect detected 1!");
           // if (hit.transform.gameObject.activeSelf)
          //  {
                // Debug.Log("reflect detected 2!");
                //lineRender.positionCount++;
             //   hit.transform.gameObject.GetComponent<ReflectLaser>().isLasered = true;
                //Vector2 refDir = Vector2.Reflect(hit.transform.Find("Reflect").up, hit.normal);
              //  lineRender.SetPosition(2, refDir);
          //  }
       // }
       // else if(hit.transform.tag != "Reflect")
       // {
           // if(reflectHit.gameObject.activeSelf)
            //{
            //    reflectHit.gameObject.GetComponent<ReflectLaser>().isLasered = false;
           // }
            //lineRender.positionCount = 2;
      //  }

        if (hit.collider.transform.tag == "Absorb")
        {
            Debug.Log("Laser Detected Absorb");
            if (hit.transform.gameObject.activeSelf)
            {
                Debug.Log("Laser Attempted Heal");
                GameObject.Find("Player").GetComponent<PlayerHealth>().HealPlayer(laserDamage);
            }
        }
        if (hit.transform.tag == "Player")
        {
            if (hit.collider.gameObject.layer != 14)
            {
                hit.transform.gameObject.GetComponent<PlayerHealth>().DamagePlayer(laserDamage);
            }
        }

        if(hit.collider.transform.tag == "Boss")
        {
            if(gameObject.tag == "Projectile")
            {
                hit.transform.gameObject.GetComponent<BossHealth>().bossHealth -= laserDamageToBoss;
                hit.transform.gameObject.GetComponent<BossHealth>().healthBar.fillAmount -= (laserDamageToBoss / 100);
            } 
        }
    }





}
