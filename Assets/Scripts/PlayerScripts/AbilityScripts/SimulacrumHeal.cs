using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulacrumHeal : MonoBehaviour {

	public GameObject heal;
	public float lifetime = 6f;

	private float offsetX;
	private float offsetY;

	void Start()
	{
		Invoke("Destroy", lifetime);
		offsetX = Random.Range(-1f, 1f);
		offsetY = Random.Range(7f, 9f);
	}

	private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "EnemyProjectile")
        {
            Instantiate(heal, transform.position + (transform.up * -offsetY) + (transform.right * offsetX), Quaternion.identity);
        }
    }

    private void Destroy()
    {
    	Destroy(gameObject);
    }
}
