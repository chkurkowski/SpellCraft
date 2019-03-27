using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonEnergyBlade : MonoBehaviour
{
    public float growthRate;
    public float growthAmount;
    private Vector3 ogLocalScale;

    private void Start()
    {
        ogLocalScale = gameObject.transform.localScale;
    }
    // Use this for initialization
    void OnEnable ()
    {
        //gameObject.transform.localScale = ogLocalScale;
        InvokeRepeating("GrowBlade", 0, growthRate);      
	}

    public void GrowBlade()
    {
        gameObject.transform.localScale += new Vector3(growthAmount, 0, 0);
    }




}
