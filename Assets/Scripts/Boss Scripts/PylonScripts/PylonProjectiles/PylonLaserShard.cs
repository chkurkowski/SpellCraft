using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonLaserShard : MonoBehaviour, IPooledObject
{
    public float laserShardDamage = 1;
    public float laserShardSpeed = 50;
    private GameObject player;
    private bool reflected = false;

    public void OnObjectSpawn()
    {
        transform.Rotate(new Vector3(0, 0, 90));
    }

    public void Update()
    {
        if (!reflected)
        {
            transform.Translate(Vector3.right * Time.deltaTime * laserShardSpeed);
        }
        else if (reflected)
        {
            transform.Translate(Vector3.left * Time.deltaTime * laserShardSpeed);
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Reflect")
        {
            // Debug.Log("Reflect happened");
            reflected = true;
            gameObject.tag = "Projectile";
            gameObject.layer = 12; //changes physics layers, do not touch or I stab you
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255);
        }
        else if (col.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerHealth>().DamagePlayer(laserShardDamage);
            GameObject.Find("Player").GetComponent<PlayerHealth>().playerHealthBar.fillAmount -= (laserShardDamage/ 100f);
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "Simulacrum")
        {
            col.gameObject.GetComponent<SimulacrumAbilities>().AbsorbDamage(laserShardDamage);
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "Absorb")
        {
            GameObject.Find("Player").GetComponent<PlayerHealth>().HealPlayer(laserShardDamage / 2);
            GameObject.Find("Player").GetComponent<PlayerHealth>().playerHealthBar.fillAmount += .005f;
            Destroy(gameObject);
        }
        else if (col.gameObject.tag != "Boss" || gameObject.tag != "CameraTrigger")
        {
            Destroy(gameObject);
        }
    }


}
