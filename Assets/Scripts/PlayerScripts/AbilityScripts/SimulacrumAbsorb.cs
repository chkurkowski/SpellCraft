using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulacrumAbsorb : MonoBehaviour {

	public GameObject fireball;
	public int atkSpeed = 80;

	private float damageTaken = 0;
    public float damageCap {private get; set;}
    private Vector2 cursorInWorldPos;
    private float lifetime = 8f;


	// Use this for initialization
	void OnEnable () 
	{
		Invoke("Destroy", lifetime);
	}
	
	// Update is called once per frame
	void Update () 
	{
		cursorInWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	public void AbsorbDamage(float amt)
    {
        damageTaken += amt;
        if(damageTaken >= damageCap)
            Destroy();
    }

    public void Explode()
    {
        int explodeAmount = Mathf.CeilToInt(damageTaken/4);

        for (int i = 0; i < explodeAmount; i++)
        {
            Vector2 direction = new Vector3(Random.Range(cursorInWorldPos.x - 20, cursorInWorldPos.x + 20), 
            	Random.Range(cursorInWorldPos.y - 10, cursorInWorldPos.y + 10), cursorInWorldPos.x) - transform.position;
            direction.Normalize();
            GameObject fb = Instantiate(fireball, transform.position, PlayerAbilities.instance.handlers.rotator.transform.rotation);
            fb.GetComponent<MagicBall>().isAbsorbSimMissile = true;
            fb.GetComponent<Rigidbody2D>().velocity = direction * Random.Range(atkSpeed - 20, atkSpeed + 20);
        }
    }

    private void Destroy()
    {
        Explode();
        damageTaken = 0;
        damageCap = 0;
        gameObject.SetActive(false);
    }
}
