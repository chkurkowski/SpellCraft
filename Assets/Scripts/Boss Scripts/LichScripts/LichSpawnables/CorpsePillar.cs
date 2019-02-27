using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpsePillar : MonoBehaviour
{
    private BossHealth lichBossHealth;

    private CorpsePillarParent corpseParent;
    public AudioSource fleshPillarAudio;
    public AudioSource floatingPillarAudio;

    public float pillarDeathDamage = 1f;
    public SpriteRenderer colorInfo;
    public float pillarHealth = 5f;
    public float pillarMaxHealth = 5f;
    public bool isEnraged = false;

    public GameObject bloodSprayer;
  //  public float bloodSprayDuration = 2f;
    //public GameObject bomb;
    //private bool canBomb = true;
    //private float canBombReset = 2f;

    // Update is called once per frame
    private void Start()
    {
        corpseParent = GameObject.Find("CorpseParent").GetComponent<CorpsePillarParent>();
        fleshPillarAudio.Play();
        lichBossHealth = GameObject.Find("Lich").GetComponent<BossHealth>();
        //bloodSprayer.SetActive(false);
    }

    private void OnEnable()
    {
        corpseParent = GameObject.Find("CorpseParent").GetComponent<CorpsePillarParent>();
        if(corpseParent.isEnraged)
        {
            Debug.Log("is Enraged blood if is happening!");
            isEnraged = true;
        }
    }





    void Update()
    {

        if (pillarHealth <= 0)
        {
            lichBossHealth.DealDamage(pillarDeathDamage);
            pillarHealth = pillarMaxHealth;
            bloodSprayer.GetComponent<BloodSprayScript>().canSprayBlood = false;
            bloodSprayer.SetActive(false);
            gameObject.SetActive(false);
            isEnraged = false;
            fleshPillarAudio.Stop();
            floatingPillarAudio.Stop();


        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            if(isEnraged)
            {
                floatingPillarAudio.Play();
                bloodSprayer.SetActive(true);
               // Invoke("TurnOffSpray", bloodSprayDuration);
            }

           /* if (isEnraged && canBomb)
            {
              //  Debug.Log("pillar bomb happened!");
                canBomb = false;
                Invoke("ResetCanBomb", canBombReset);
                GameObject spawnedBomb = Instantiate(bomb, transform.position, transform.rotation);
                spawnedBomb.GetComponent<Bomb>().Explode();
            }*/
            colorInfo.color = Color.red;
            Invoke("ResetColor", 0.50f);
            pillarHealth -= collision.gameObject.GetComponent<ProjectileDamage>().projectileDamage;
          
        }

    }

    public void SetEnraged(bool tof)
    {
        isEnraged = tof;
    }

  

  /*private void ResetCanBomb()
    {
        canBomb = true;
    }*/

    private void ResetColor()
    {
        colorInfo.color = Color.white;
    }
}
