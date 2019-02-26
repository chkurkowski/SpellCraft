﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public AudioSource bombSource;
    public AudioClip explosionSound;
    private ObjectPoolerScript objectPooler;


    private ProjectileDamage projectileDamageInfo;
    public float bombDamage = 25f;
    public float fireBallSpeed = 50;

    //public GameObject fireBall;
    public float fireBallSpawnAmount;

    private void Start()
    {
        objectPooler = ObjectPoolerScript.Instance;
        projectileDamageInfo = gameObject.GetComponent<ProjectileDamage>();
        bombDamage = projectileDamageInfo.projectileDamage;
        transform.Rotate(new Vector3(0, 0, 90));
        Invoke("Explode", 2);
    }

    public void OnObjectSpawn()
    {
        
    }

    public void OnEnable()
    {
        Invoke("Explode", 2);
    }

    public void OnDisable()
    {
        CancelInvoke();
    }
    private void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * fireBallSpeed);
    }

        // transform.localScale += new Vector3(1,0);
    
    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Reflect")
        {
            Explode();
        }
        
        if (col.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerHealth>().DamagePlayer(bombDamage);
            // GameObject.Find("Player").GetComponent<PlayerHealth>().playerHealthBar.fillAmount -= .25f;

        }
        else if(col.gameObject.tag == "Simulacrum")
        {
            col.gameObject.GetComponent<SimulacrumAbilities>().AbsorbDamage(bombDamage);
            Explode();
        }
        else if (col.gameObject.tag == "Absorb")
        {
            GameObject.Find("Player").GetComponent<PlayerHealth>().HealPlayer(bombDamage / 2);
            // GameObject.Find("Player").GetComponent<PlayerHealth>().playerHealthBar.fillAmount += .025f;
            gameObject.SetActive(false);
        }
        else if(col.gameObject.tag == "Environment")
        {
            Explode();
        }
        else if (col.gameObject.tag == "CameraTrigger" || col.gameObject.tag != "HealStun")
        {
            //do nothing
        }
        else if (col.gameObject.tag != "Boss" || gameObject.tag != "CameraTrigger")
        {
            Explode();
        }
    }

    public void Explode()
    {
        bombSource.clip = explosionSound;
        bombSource.PlayOneShot(explosionSound);
        for (int i = 0; i < fireBallSpawnAmount; i++)
        {
            transform.Rotate(0, 0, 25);
            objectPooler.SpawnFromPool("Fireball", transform.position, transform.rotation);
        }
        gameObject.SetActive(false);
    }
}