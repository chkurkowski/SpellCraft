using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

    private ProjectileDamage projectileDamageInfo;
    public float fireBallDamage;
    public float fireBallSpeed = 50;
    private GameObject player;
    private bool reflected = false;
    public AudioSource reflectSource;
    public AudioClip reflectSound;

    private void Start()
    {
        projectileDamageInfo = gameObject.GetComponent<ProjectileDamage>();
        fireBallDamage = projectileDamageInfo.projectileDamage;
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
            reflectSource.clip = reflectSound;
            reflectSource.PlayOneShot(reflectSound);
            // Debug.Log("Reflect happened");
            reflected = true;
            gameObject.tag = "Projectile";
            gameObject.layer = 12; //changes physics layers, do not touch or I stab you
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255);
        }
        else if(col.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerHealth>().DamagePlayer(fireBallDamage);
            GameObject.Find("Player").GetComponent<PlayerHealth>().playerHealthBar.fillAmount -= .05f;
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "Simulacrum")
        {
            col.gameObject.GetComponent<SimulacrumAbilities>().AbsorbDamage(fireBallDamage);
            Destroy(gameObject);
        }
        else if(col.gameObject.tag == "Absorb")
        {
            GameObject.Find("Player").GetComponent<PlayerHealth>().HealPlayer(fireBallDamage/2);
            GameObject.Find("Player").GetComponent<PlayerHealth>().playerHealthBar.fillAmount += .025f;
            Destroy(gameObject);
        }
        else if(col.gameObject.tag == "Boss" || col.gameObject.tag == "Projectile")
        {
            //do nothing
        }
        else if(col.gameObject.tag == "CameraTrigger" && col.gameObject.tag == "HealStun")
        {
            //do nothing
        }
              
        else if (col.gameObject.tag != "Boss" || col.gameObject.tag != "CameraTrigger")
        {
            Destroy(gameObject);
        }
    }
}
