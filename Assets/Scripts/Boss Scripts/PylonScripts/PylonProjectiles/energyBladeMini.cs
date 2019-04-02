using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energyBladeMini : MonoBehaviour
{

    private GameObject parentEnergy;

    private void Start()
    {
        parentEnergy = gameObject.transform.parent.gameObject;
    }

    public void OnTriggerEnter2D(Collider2D trig)
    {
        Debug.Log("Energy Blade Detected a collision");
        if (trig.gameObject.tag == "Environment")
        {
            Debug.Log("Energy Blade Detected an environment");
            Destroy(gameObject.transform.parent.gameObject);
        }

    }

}
