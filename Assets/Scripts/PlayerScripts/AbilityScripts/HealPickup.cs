using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickup : MonoBehaviour {

	public float healAmount = 5f;
	public float force = 5f;
	public float radius = 1f;

	private bool insideField;
	private Transform player;

	private void Start()
	{
		player = GameObject.Find("Player").GetComponent<Transform>();
		insideField = false;
	}

	private void Update()
	{
		if(insideField)
		{
    		Vector3 magnetField = player.position - transform.position;
    		float index = (radius - magnetField.magnitude) / radius;
    		GetComponent<Rigidbody2D>().AddForce(-(force * magnetField * index));

    		if(Vector3.Distance(player.transform.position, transform.position) <= 8f)
			{
				player.GetComponent<PlayerHealth>().HealPlayer(healAmount);
				Destroy(gameObject);
			}
    	}
    	else if(GetComponent<Rigidbody2D>().velocity != Vector2.zero)
    	{
    		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    	}
	}

	public float AddHealAmount(float amt)
	{
		return healAmount += amt;
	}

	public float GetHealAmount()
	{
		return healAmount;
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Player")
		{
			insideField = true;
		}
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		if(col.gameObject.tag == "Player")
		{
			insideField = false;
		}
	}

}
