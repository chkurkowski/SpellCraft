using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonMovement : MonoBehaviour {
    //TODO 
    //write a function for spinning that takes a time variable for the laser.
    //also write a function that can be called to increase the rate at which the boss spins.
    // Use this for initialization
    private BossInfo bossInfo;
    public float rotationDirection = 1f;
    public float rotationSpeed = 0.002f;
    public float rotationAmount = 0.5f;

    void Start ()
    {
        bossInfo = gameObject.GetComponent<BossInfo>();
        //InvokeRepeating("PylonRotate", 0, rotationSpeed);//for testing
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void LaserAttackMovement(float durationTime)
    {
        Vector3 dir = bossInfo.GetPlayerLocation().transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 180, transform.forward);


        InvokeRepeating("PylonRotate", 0, rotationSpeed);
        Invoke("StopLaserAttackMovement", durationTime);
    }

    public void LaserAttackMovement(float durationTime, float rotationSpeedIncrease)
    {
        Vector3 dir = bossInfo.GetPlayerLocation().transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 180, transform.forward);

        rotationSpeed -= rotationSpeedIncrease;
        InvokeRepeating("PylonRotate", 0, rotationSpeed);
        Invoke("StopLaserAttackMovement", durationTime);
    }


    public void IncreaseLaserAttackSpinSpeed(float speedIncrease)
    {
        rotationSpeed += speedIncrease;
    }


    public void StopLaserAttackMovement()
    {
        CancelInvoke("PylonRotate");
    }

    public void PylonRotate()
    {
        ///Vector3 dir = bossInfo.GetPlayerLocation().transform.position - transform.position;
        ///float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        ///transform.rotation = Quaternion.AngleAxis(angle - 90, transform.forward);
        gameObject.transform.Rotate(0, 0, rotationAmount);// * rotationDirection);
    }

}
