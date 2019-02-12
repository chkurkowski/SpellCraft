using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonLaserShard : MonoBehaviour, IPooledObject
{
    ProjectileDamage projectileDamageInfo;
    public float laserShardDamage = 1;
    public float laserShardSpeed = 50;
    public float minLaserSpread = -.5f;
    public float maxLaserSpread = .5f;
    private GameObject player;
    private bool reflected = false;
    private Color32 originalColor;
    private float randNum;

    public void Start()
    {
        projectileDamageInfo = gameObject.GetComponent<ProjectileDamage>();
        laserShardDamage = projectileDamageInfo.projectileDamage;
        originalColor = gameObject.GetComponent<SpriteRenderer>().color;
    }
    public void OnObjectSpawn()
    {
        randNum = Random.Range(minLaserSpread, maxLaserSpread);
        //transform.Rotate(new Vector3(0, 0, 90));
        //gotta undo any potential reflects!
        if(reflected)
        {
            gameObject.tag = "EnemyProjectile";
            reflected = false;
            gameObject.GetComponent<SpriteRenderer>().color = originalColor;
            gameObject.layer = 9;
        }

        transform.Rotate(0,0,randNum);
        
    }

    public void Update()
    {
        if (!reflected)
        {
            transform.Translate(Vector3.up * Time.deltaTime * laserShardSpeed);
        }
        else if (reflected)
        {
            transform.Translate(Vector3.up *-1 * Time.deltaTime * laserShardSpeed);
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
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
        else if (col.gameObject.tag == "Simulacrum")
        {
            col.gameObject.GetComponent<SimulacrumAbilities>().AbsorbDamage(laserShardDamage);
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
        else if (col.gameObject.tag == "Absorb")
        {
            GameObject.Find("Player").GetComponent<PlayerHealth>().HealPlayer(laserShardDamage / 2);
            GameObject.Find("Player").GetComponent<PlayerHealth>().playerHealthBar.fillAmount += .005f;
            // Destroy(gameObject);
            gameObject.SetActive(false);
        }
        else if (col.gameObject.tag != "EnemyProjectile" && col.gameObject.tag != "Boss" && col.gameObject.tag != "CameraTrigger")
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }


}
