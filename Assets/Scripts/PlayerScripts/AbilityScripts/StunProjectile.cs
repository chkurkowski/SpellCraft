using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunProjectile : MonoBehaviour {

	public GameObject projectile;
	public float atkSpeed = 80;


	private int projectileAmount = 15;
	private float chargeAmount = 0;
	private float stunAmount = 30;
	private float damageAmount = 30;

	void Update()
	{
		if(chargeAmount >= 30)
		{
			Invoke("Destroy", 1);
		}
	}

	public float AddAmount(float charge)
	{
		return chargeAmount += charge;
	}

	private void Explode()
	{
		for (int i = 0; i < projectileAmount; i++)
        {
            transform.Rotate(0, 0, 25);
            GameObject gm = Instantiate(projectile, transform.position, transform.rotation);
            gm.GetComponent<MagicBall>().firedFromPlayer = false;
        }
	}

	private void Destroy()
	{
		Explode();
		Destroy(gameObject);
	}

}
