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

	private void OnTriggerStay2D(Collider2D col)
	{
		print(Vector3.Distance(col.transform.position, transform.position));
		if(Vector3.Distance(col.transform.position, transform.position) <= 8f)
		{
			col.GetComponent<PlayerHealth>().HealPlayer(healAmount);
			Destroy(gameObject);
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
