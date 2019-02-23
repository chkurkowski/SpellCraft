using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemArtillery : MonoBehaviour {


    public float golemHealth = 50f;
    public float golemHealthMaximum = 50f;
    public float golemDeathDamage = 15f;
    public bool canFacePlayer = true;

    private Vector3 vectorToTarget;
    private float angle;
    private Quaternion rotAngle;
    private bool isAttacking = false;
 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void FacePlayer()
    {
        if (canFacePlayer)
        {
            Vector3 dir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, transform.forward);
        }
    }
}
