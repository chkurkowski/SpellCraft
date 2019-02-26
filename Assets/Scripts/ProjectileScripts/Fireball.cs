using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

    private ProjectileDamage projectileDamageInfo;
    public float fireBallDamage;
    public float fireBallSpeed = 50;
    public float lifeTime = 5f;
    private GameObject player;
    private bool reflected = false;
    public AudioSource reflectSource;
    public AudioClip reflectSound;
    private Color32 originalColor;

    private void Start()
    {
        projectileDamageInfo = gameObject.GetComponent<ProjectileDamage>();
        fireBallDamage = projectileDamageInfo.projectileDamage;
        transform.Rotate(new Vector3(0, 0, 90));
       // Destroy(gameObject, lifeTime);
        originalColor = gameObject.GetComponent<SpriteRenderer>().color;
        //Invoke("Disable", lifeTime);
    }

    public void OnObjectSpawn()
    {
     
    }

    public void OnEnable()
    {
        Invoke("Disable", lifeTime);
        if (reflected)
        {
            gameObject.tag = "EnemyProjectile";
            reflected = false;
            gameObject.GetComponent<SpriteRenderer>().color = originalColor;
            gameObject.layer = 9;
        }
    }

    private void Update()
    {
        if(!reflected)
        {
            transform.Translate(Vector3.right * Time.deltaTime * fireBallSpeed);
        }
        else if(reflected)
        {
            //transform.Rotate(0, 0, 180);
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
            // GameObject.Find("Player").GetComponent<PlayerHealth>().playerHealthBar.fillAmount -= .05f;

            gameObject.SetActive(false);
        }
        else if (col.gameObject.tag == "Simulacrum")
        {
            col.gameObject.GetComponent<SimulacrumAbilities>().AbsorbDamage(fireBallDamage);
            Debug.Log(gameObject.name + " was destroyed by " + col.gameObject.name);
            Disable();
        }
        else if(col.gameObject.tag == "Absorb")
        {
            GameObject.Find("Player").GetComponent<PlayerHealth>().HealPlayer(fireBallDamage/2);
            // GameObject.Find("Player").GetComponent<PlayerHealth>().playerHealthBar.fillAmount += .025f;

            Disable();
        }
        else if(col.gameObject.tag == "Boss" && gameObject.tag == "EnemyProjectile")
        {
            //do nothing
        }
        else if(col.gameObject.tag == "CameraTrigger" || col.gameObject.tag == "EnemyReflect")
        {
            //do nothing
        }
        else if(col.gameObject.GetComponent<GolemAbility>() != null)
        {
            if(col.gameObject.GetComponent<GolemAbility>().canTakeDamage)
            {
                Disable();
            }
        }  
        else if (col.gameObject.tag != "Boss" || gameObject.tag != "CameraTrigger" || gameObject.tag != "Projectile")
        {
            Disable();
        }
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
