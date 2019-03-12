using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulacrumHeal : MonoBehaviour {

	public GameObject heal;
	public float lifetime = 6f;

	private float timer = 0f;
	private float MAXTIMER = 1f;

	private float offsetX;
	private float offsetY;

	void Start()
	{
		Invoke("Destroy", lifetime);
		offsetX = Random.Range(-5f, 5f);
		offsetY = Random.Range(9f, 15f);
	}

	void Update()
	{
		timer += Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "EnemyProjectile" && timer >= MAXTIMER)
        {
            Instantiate(heal, transform.position + (transform.up * -offsetY) + (transform.right * offsetX), Quaternion.identity);
            timer = 0;
        }
    }

    private void Destroy()
    {
    	Destroy(gameObject);
    }
}
