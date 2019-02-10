using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonLaserParticleCollisions : MonoBehaviour {
    public float laserDamagePerParticle;
    // Use this for initialization

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().DamagePlayer(laserDamagePerParticle);
        }
        if (other.gameObject.tag == "Reflect")
        {
             Debug.Log("Reflect Particle happened");

            Destroy(gameObject);
        }
    }
    private void OnParticleTrigger()
    {
        
    }
}
