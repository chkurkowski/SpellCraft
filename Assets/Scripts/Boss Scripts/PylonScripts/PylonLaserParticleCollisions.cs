using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonLaserParticleCollisions : MonoBehaviour {
    public float laserDamagePerParticle;
    private ParticleSystem pSystem;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    // Use this for initialization

    private void OnEnable()
    {
        pSystem = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.transform.name == "Reflect")
        {
            Debug.Log("reflect detected");
            // pSystem.startSpeed *= -1;
            int numEnter = pSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);



            for (int i = 0; i < numEnter; i++)
            {
                ParticleSystem.Particle p = enter[i];

                p.startColor = new Color32(255, 0, 0, 255);
                p.velocity *= -1;
                enter[i] = p;
            }

            pSystem.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        }

        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Player Detected by Collisions");
           // other.gameObject.GetComponent<PlayerHealth>().DamagePlayer(laserDamagePerParticle);
        }
      
    }

    private void OnParticleTrigger()
    {
        

    }


}
