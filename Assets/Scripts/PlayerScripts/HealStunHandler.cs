using System.Collections;
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

    private void StunAbsorb()
    {
    	if(state == State.NULL)
    		state = State.STUNABSORB;


    }

    private void HealAbsorb()
    {
    	if(state == State.NULL)
    		state = State.HEALABSORB;


    }

    private void Destroy()
    {
    	Destroy(gameObject);
    }

}
