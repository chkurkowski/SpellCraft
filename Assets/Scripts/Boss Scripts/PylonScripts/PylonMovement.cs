using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonMovement : MonoBehaviour {
    //TODO 
    //write a function for spinning that takes a time variable for the laser.
    //also write a function that can be called to increase the rate at which the boss spins.
    // Use this for initialization
    private BossInfo bossInfo;
    private BossAttacks bossAttacksInfo;
    public float rotationDirection = 1f;
    private float rotationSpeed = 0.01f;
   private float rotationAmount = 0.5f;
    private bool isSpinning = false;

    void Start ()
    {
        bossInfo = gameObject.GetComponent<BossInfo>();
        bossAttacksInfo = gameObject.GetComponent<BossAttacks>();
       
    }

    public void LaserAttackMovement()
    {

        if (!isSpinning)
        {
            isSpinning = true;
            Vector3 dir = bossInfo.GetPlayerLocation().transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 180, transform.forward);

            Debug.Log("LaserMove was called");
            InvokeRepeating("PylonRotate", 0, rotationSpeed);
        }
        else
        {
            Debug.Log("LaserMove was attempted to be called twice");
        }


    }

   





    public void StopLaserAttackMovement()
    {
        isSpinning = false;
        Debug.Log("LaserMove was stopped");
        CancelInvoke();
    }

    public void PylonRotate()
{
   
            ///Vector3 dir = bossInfo.GetPlayerLocation().transform.position - transform.position;
            ///float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            ///transform.rotation = Quaternion.AngleAxis(angle - 90, transform.forward);
            gameObject.transform.Rotate(0, 0, rotationAmount);// * rotationDirection);
       
    }


}
