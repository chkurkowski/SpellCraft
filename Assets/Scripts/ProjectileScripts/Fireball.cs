using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {
	
    public float fireBallDamage = 5;
    public float fireBallSpeed = 50;
    private GameObject player;
    private bool reflected = false;

    private void Start()
    {
        transform.Rotate(new Vector3(0, 0, 90));
    }
    private void Update()
    {
        if(!reflected)
        {
            transform.Translate(Vector3.right * Time.deltaTime * fireBallSpeed);
        }
        else if(reflected)
        {
            transform.Translate(Vector3.left * Time.deltaTime * fireBallSpeed);
        }
       
       // transform.localScale += new Vector3(1,0);
    }
    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D col)
    {
       
        if(col.gameObject.tag == "Reflect")
        {
           // Debug.Log("Reflect happened");
            reflected = true;
            gameObject.tag = "Projectile";
            gameObject.layer = 12; //changes physics layers, do not touch or I stab you
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255);
        }
        else if(col.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerHealth>().playerHealth -= fireBallDamage;
            GameObject.Find("Player").GetComponent<PlayerHealth>().playerHealthBar.fillAmount -= .05f;
            Destroy(gameObject);
        }
        else if(col.gameObject.tag == "Absorb")
        {
            GameObject.Find("Player").GetComponent<PlayerHealth>().playerHealth += fireBallDamage/2;
            GameObject.Find("Player").GetComponent<PlayerHealth>().playerHealthBar.fillAmount += .025f;
            Destroy(gameObject);
        }
        else if (col.gameObject.tag != "Boss" || gameObject.tag != "CameraTrigger")
        {
            Destroy(gameObject);
        }
    }
}
