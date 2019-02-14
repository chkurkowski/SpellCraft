﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealStunHandler : MonoBehaviour {

	public enum State
    {
    	NULL,
        STUNABSORB,
        HEALABSORB
    }
    public State state;

    public GameObject stunPrefab;
    public GameObject healPrefab;

    private GameObject stun;
    private GameObject heal;
    
    private const float STUNCAP = 25f;
    private const float HEALCAP = 25f;

    private float stunAmount = 0f;
    private float healAmount = 0f;
    private float lifetime = 8f;

	// Use this for initialization
	void Start () 
	{
		Invoke("Destroy", lifetime);
		state = State.NULL;
	}

    /* Testing
    void Update()
    {
        if(Input.GetKey(KeyCode.T))
        {
            StunAbsorb();
        }

        if(Input.GetKey(KeyCode.G))
        {
            HealAbsorb();
        }
    }
    */

    private bool StunAbsorb()
    {
    	if(state == State.NULL)
    		state = State.STUNABSORB;
        
        if(state == State.STUNABSORB)
        {
            if(stun == null)
            {
                stun = Instantiate(stunPrefab, transform.position + (transform.up * 10f), Quaternion.identity);
            }
            else
            {
                stun.transform.localScale += new Vector3(1.0f, 1.0f, 0f);
            }
            return true;
        }
        return false;
    }

    private bool HealAbsorb()
    {
    	if(state == State.NULL)
    		state = State.HEALABSORB;
        
        if(state == State.HEALABSORB)
        {
            if(heal == null)
            {
                heal = Instantiate(healPrefab, transform.position + -(transform.up * 7.5f), Quaternion.identity);
            }
            else
            {
                heal.transform.localScale += new Vector3(1.0f, 1.0f, 0f);
            }
            return true;
        }
        return false;
    }

    private void Destroy()
    {
    	Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "PlayerProjectile")
        {
            if(StunAbsorb())
                Destroy(col.gameObject);
        }

        if(col.gameObject.tag == "EnemyProjectile")
        {
            if(HealAbsorb())
                Destroy(col.gameObject);
        }
    }

}
