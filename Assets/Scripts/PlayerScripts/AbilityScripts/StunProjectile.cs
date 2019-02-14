using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunProjectile : MonoBehaviour {

	public GameObject projectile;
	public float atkSpeed = 80;
	public bool fired = false;

	private int projectileAmount = 15;
	private float projSpeed = 75;
	private float chargeAmount = 0;
	private float stunAmount = 30;
	private float damageAmount = 30;

	void Update()
	{
		if(fired)
		{
			transform.Translate(Vector3.forward * Time.deltaTime * projSpeed);
		}

		if(chargeAmount >= 30)
		{
			Invoke("Destroy", 1);
		}
	}

	public float AddAmount(float charge)
	{
		return chargeAmount += charge;
	}

	public float GetAmount()
	{
		return chargeAmount;
	}

	public void DestroySoon()
	{
		Invoke("Destroy", 3);
	}

	private void Explode()
	{
		for (int i = 0; i < chargeAmount / 2; i++)
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
