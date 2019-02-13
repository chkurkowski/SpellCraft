using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonMicroVortex : MonoBehaviour
{
    private PylonVortex parentVortex;
    public ParticleSystem vortexParticle;

    // Use this for initialization
    void Start()
    {
        vortexParticle.Stop();
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

            vortexParticle.Play();
            Invoke("StopWaveParticle", 2f);
            parentVortex.vortexDamageNOTFOREDITING += collision.gameObject.GetComponent<ProjectileDamage>().projectileDamage;
            parentVortex.vortexGrowthLimit += parentVortex.vortexLimitIncreaseAmount;
            parentVortex.vortexDamageNOTFOREDITING += collision.gameObject.GetComponent<ProjectileDamage>().projectileDamage;
        }
    }
    public void StopWaveParticle()
    {
        vortexParticle.Stop();

    }

}
