using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonLaserShard : MonoBehaviour, IPooledObject
{
    private ProjectileDamage projectileDamageInfo;
  
    public float laserShardDamage;
    public float laserShardDamageToBoss;

    [Tooltip("How fast the laser shards fly through the air")]
    public float laserShardSpeed = 50;
   
    [Tooltip("The range of the /shotgun spread/ effect. Bigger numbers means more spread on the crystals.")]
    public float laserSpread = .5f;
    private GameObject player;
    private bool reflected = false;
    private Color32 originalColor;
    private float randNum;
    public GameObject vfxObject;

    [Space(20)]
    [Header("Sound Variables")]
    public AudioSource reflectSource;
    public AudioClip reflectSound;
    public AudioClip shardSound;

    public void Start()
    {
        projectileDamageInfo = gameObject.GetComponent<ProjectileDamage>();
        laserShardDamage = projectileDamageInfo.projectileDamage;
        originalColor = gameObject.GetComponent<SpriteRenderer>().color;
    }
    public void OnObjectSpawn()
    {
       // reflectSource.PlayOneShot(shardSound);
        randNum = Random.Range(-laserSpread, laserSpread);
        //transform.Rotate(new Vector3(0, 0, 90));
        //gotta undo any potential reflects!
        if(reflected)
        {
            projectileDamageInfo.projectileDamage = laserShardDamage;
           gameObject.tag = "EnemyProjectile";
            reflected = false;
            gameObject.GetComponent<SpriteRenderer>().color = originalColor;
            gameObject.layer = 9;
        }

        transform.Rotate(0,0,randNum);
        
    }

    public void OnEnable()
    {
        // reflectSource.PlayOneShot(shardSound);
        randNum = Random.Range(-laserSpread, laserSpread);
        //transform.Rotate(new Vector3(0, 0, 90));
        //gotta undo any potential reflects!
        if (reflected)
        {
            gameObject.tag = "EnemyProjectile";
            reflected = false;
            gameObject.GetComponent<SpriteRenderer>().color = originalColor;
            gameObject.layer = 9;
        }

        transform.Rotate(0, 0, randNum);

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
            projectileDamageInfo.projectileDamage = laserShardDamageToBoss;
            reflectSource.clip = reflectSound;
            reflectSource.PlayOneShot(reflectSound);
            // Debug.Log("Reflect happened");
            reflected = true;
            gameObject.tag = "Projectile";
            gameObject.layer = 12; //changes physics layers, do not touch or I stab you
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255);
        }
        else if (col.gameObject.tag == "Player")
        {
           // GameObject.Find("Player").GetComponent<PlayerHealth>().DamagePlayer(laserShardDamage);
            col.gameObject.GetComponent<PlayerHealth>().DamagePlayer(laserShardDamage);
            // GameObject.Find("Player").GetComponent<PlayerHealth>().playerHealthBar.fillAmount -= (laserShardDamage/ 100f);
            //Destroy(gameObject);
            Instantiate(vfxObject, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
        else if (col.gameObject.tag == "Simulacrum")
        {
            col.gameObject.GetComponent<SimulacrumAbsorb>().AbsorbDamage(laserShardDamage);
            //Destroy(gameObject);
            Instantiate(vfxObject, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
        else if (col.gameObject.tag == "Absorb")
        {
            GameObject.Find("Player").GetComponent<PlayerHealth>().HealPlayer(laserShardDamage / 2);
            // GameObject.Find("Player").GetComponent<PlayerHealth>().playerHealthBar.fillAmount += .005f;
            // Destroy(gameObject);
            Instantiate(vfxObject, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
        else if (col.gameObject.tag != "EnemyProjectile" && col.gameObject.tag != "Boss" 
            && col.gameObject.tag != "CameraTrigger" && col.gameObject.tag != "Vortex" && col.gameObject.tag != "Split")
        {
            //Destroy(gameObject);
            Instantiate(vfxObject, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }


}
