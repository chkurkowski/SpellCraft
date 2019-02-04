using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public float bombDamage = 25f;
    public float fireBallSpeed = 50;

    public GameObject fireBall;
    public float fireBallSpawnAmount;

    private void Start()
    {
     
        transform.Rotate(new Vector3(0, 0, 90));
        Invoke("Explode", 2);
    }
    private void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * fireBallSpeed);
    }

        // transform.localScale += new Vector3(1,0);
    
    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerHealth>().DamagePlayer(bombDamage);
            GameObject.Find("Player").GetComponent<PlayerHealth>().playerHealthBar.fillAmount -= .25f;

        }
        else if(col.gameObject.tag == "Simulacrum")
        {
            col.gameObject.GetComponent<SimulacrumAbilities>().AbsorbDamage(bombDamage);
            Explode();
        }

        else if (col.gameObject.tag != "Boss" || gameObject.tag != "CameraTrigger")
        {
            Explode();
        }
    }

    public void Explode()
    {
        for(int i = 0; i < fireBallSpawnAmount; i++)
        {
            transform.Rotate(0, 0, 25);
            Instantiate(fireBall, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
