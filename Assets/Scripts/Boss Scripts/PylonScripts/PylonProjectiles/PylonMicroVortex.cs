using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonMicroVortex : MonoBehaviour
{
    private PylonVortex parentVortex;

    // Use this for initialization
    void Start()
    {
        parentVortex = GameObject.Find("Vortex").GetComponent<PylonVortex>();
        if (parentVortex == null)
        {
            Debug.Log("parentVortex was null on: " + gameObject.transform.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Projectile")
        {
           
            parentVortex.vortexDamageNOTFOREDITING += collision.gameObject.GetComponent<ProjectileDamage>().projectileDamage;
            parentVortex.vortexGrowthLimit += parentVortex.vortexLimitIncreaseAmount;
            parentVortex.vortexDamageNOTFOREDITING += collision.gameObject.GetComponent<ProjectileDamage>().projectileDamage;
        }
    }


}
