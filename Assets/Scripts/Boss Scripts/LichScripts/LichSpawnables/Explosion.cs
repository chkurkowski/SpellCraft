using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionDamage = 15f;
    public float explosionTime = 0.30f;
    public float explosionRate = .020f;

    // Use this for initialization
    void Start ()
    {
        InvokeRepeating("Grow", 0, explosionRate);
        Destroy(gameObject, explosionTime);
    }


    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            trig.GetComponent<PlayerHealth>().DamagePlayer(explosionDamage);  
        }
        if(trig.gameObject.tag == "Skeleton" || trig.gameObject.name == "HellKnight(Clone)")
        {
            trig.gameObject.SetActive(false);
        }
    }

    void Grow()
    {
        transform.localScale += new Vector3(1f, 1f, 0);
    }
}
