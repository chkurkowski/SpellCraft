using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonLaserNode : MonoBehaviour
{
    private ObjectPoolerScript objectPooler;
   // public float laserOffsetRight = .05f;
   // public float laserOffsetLeft = -.05f;
	// Use this for initialization
	void Start ()
    {
        objectPooler = ObjectPoolerScript.Instance;
	}

    private void OnEnable()
    {
        InvokeRepeating("SpawnLaser", 0, .05f);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }

    // Update is called once per frame
    void Update ()
    {
        
        // objectPooler.SpawnFromPool("PylonLaser", new Vector3(transform.position.x + laserOffsetRight, transform.position.y, transform.position.z),
        //  transform.rotation);
        // objectPooler.SpawnFromPool("PylonLaser", new Vector3(transform.position.x + laserOffsetLeft, transform.position.y, transform.position.z),
        // transform.rotation);

    }

    private void SpawnLaser()
    {
        objectPooler.SpawnFromPool("PylonLaser", transform.position, transform.rotation);
    }
}
