using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSplit : MonoBehaviour {

	public int lifetime = 6;

	void Start()
	{
		Invoke("Destroy", lifetime);
	}

	private void Destroy()
	{
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "MagicMissile(Clone)")
        {
            GameObject gm1 = Instantiate(col.gameObject, col.gameObject.transform.position + (Vector3.left * 8), Quaternion.identity);
            GameObject gm2 = Instantiate(col.gameObject, col.gameObject.transform.position + (Vector3.right * 8), Quaternion.identity);

            gm1.GetComponent<Rigidbody2D>().velocity = col.gameObject.GetComponent<Rigidbody2D>().velocity;
            gm2.GetComponent<Rigidbody2D>().velocity = col.gameObject.GetComponent<Rigidbody2D>().velocity;
        
            //Vector2 direction = cursorInWorldPos - new Vector2(transform.position.x, transform.position.y);
            //fb.GetComponent<Rigidbody2D>().velocity = (direction + new Vector2(tempX, 0)) * atkSpeed;
        }
        else if(col.gameObject.name == "PlayerGolem(Clone)")
        {
            GameObject gm1 = Instantiate(col.gameObject, col.gameObject.transform.position + (Vector3.left * 8), col.transform.rotation);
            GameObject gm2 = Instantiate(col.gameObject, col.gameObject.transform.position + (Vector3.right * 8), col.transform.rotation);

            gm1.GetComponent<GolemAbility>().timeTillCharge = 0;
            gm1.GetComponent<GolemAbility>().startingChargeSpeed = col.gameObject.GetComponent<GolemAbility>().startingChargeSpeed;
            gm1.GetComponent<GolemAbility>().chargeSlowRate = col.gameObject.GetComponent<GolemAbility>().chargeSlowRate;

            gm2.GetComponent<GolemAbility>().timeTillCharge = 0;
            gm2.GetComponent<GolemAbility>().startingChargeSpeed = col.gameObject.GetComponent<GolemAbility>().startingChargeSpeed;
            gm2.GetComponent<GolemAbility>().chargeSlowRate = col.gameObject.GetComponent<GolemAbility>().chargeSlowRate;
        }
    }
}
