using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    //audio stuff
    public AudioSource portalAudio;
    public AudioSource skeleSpawnAudio;
    public AudioSource knightSpawnAudio;
    public AudioSource soulSpawnAudio;

    private ObjectPoolerScript objectPooler;

    private BossHealth lichBossHealth;
    private SpriteRenderer colorInfo;
    private bool canSpawn = false;

    public float portalHealth = 15f;
    public float portalMaxHealth = 15f;

    [Space(25)]

    public float portalDeathDamage = 15f;

    [Space(25)]

    public GameObject skeleton;
    public GameObject hellKnight;
    public GameObject infernalSoul;

    [Space(25)]

    public float skeletonSpawnRate = .3f;
    public float hellKnightSpawnRate = 5f;
    public float infernalSoulSpawnRate = 2f;

    [Space(25)]

    public bool isMad = false;
    public bool isEnraged = false;

	// Use this for initialization
	void Start ()
    {
        objectPooler = ObjectPoolerScript.Instance;
        canSpawn = false;
        colorInfo = gameObject.GetComponent<SpriteRenderer>();
        lichBossHealth = GameObject.Find("Lich").GetComponent<BossHealth>();
    }

    private void OnEnable()
    {
       
          InvokeRepeating("SpawnSkeletons", 0, skeletonSpawnRate);
        if(isMad)
        {
           InvokeRepeating("SpawnHellKnights", 0, hellKnightSpawnRate);
        }
        if(isEnraged)
        {
            InvokeRepeating("SpawnInfernalSouls", 0, infernalSoulSpawnRate);
        }
    canSpawn = true;
        portalAudio.Play();
    }

    // Update is called once per frame
    void Update ()
    {
		if(portalHealth <= 0)
        {
            portalHealth = portalMaxHealth;
            lichBossHealth.DealDamage(portalDeathDamage);
            canSpawn = false;
            CancelInvoke();
            gameObject.SetActive(false);

        }
        if(!gameObject.activeSelf)
        {
            canSpawn = false;
            CancelInvoke();
        }
        
	}


    public void SpawnSkeletons()
    {
        if(canSpawn)
        {

        GameObject spawnedSkeleton1 = objectPooler.SpawnFromPool("Skeleton", transform.position, transform.rotation);
            spawnedSkeleton1.transform.Rotate(0, 0, Random.Range(0, 90));
        spawnedSkeleton1.transform.Translate(transform.up * -1);
            skeleSpawnAudio.Play();

        GameObject spawnedSkeleton2 = objectPooler.SpawnFromPool("Skeleton", transform.position, transform.rotation);
            spawnedSkeleton2.transform.Rotate(0, 0, Random.Range(90, 180));
        spawnedSkeleton2.transform.Translate(transform.up * -1);
            skeleSpawnAudio.Play();

            GameObject spawnedSkeleton3 = objectPooler.SpawnFromPool("Skeleton", transform.position, transform.rotation);
            spawnedSkeleton3.transform.Rotate(0, 0, Random.Range(180, 270));
        spawnedSkeleton3.transform.Translate(transform.up * -1);
            skeleSpawnAudio.Play();

            GameObject spawnedSkeleton4 = objectPooler.SpawnFromPool("Skeleton", transform.position, transform.rotation);
            spawnedSkeleton4.transform.Rotate(0, 0, Random.Range(270, 360));
        spawnedSkeleton4.transform.Translate(transform.up * -1);
            skeleSpawnAudio.Play();
        }
    }

    public void SpawnHellKnights()
    {
        if (canSpawn)
        {
           // Debug.Log("Skeletons should be spawning");
            GameObject hellKnight1 = objectPooler.SpawnFromPool("HellKnight", transform.position, transform.rotation);
            knightSpawnAudio.Play();
            hellKnight1.transform.Translate(0, 0, 0);

           // GameObject hellKnight2 = Instantiate(hellKnight, transform.position, transform.rotation);

            //hellKnight2.transform.Translate(0,10,0);
        }
    }

    public void SpawnInfernalSouls()
    {
        if (canSpawn)
        {
            GameObject infernalSoul1 = objectPooler.SpawnFromPool("InfernalSoul", transform.position, transform.rotation);
            infernalSoul1.transform.Rotate(0, 0, Random.Range(0, 90));
            infernalSoul1.transform.Translate(transform.up * -1);
            soulSpawnAudio.Play();

            GameObject infernalSoul2 = objectPooler.SpawnFromPool("InfernalSoul", transform.position, transform.rotation);
            infernalSoul2.transform.Rotate(0, 0, Random.Range(90, 180));
            infernalSoul2.transform.Translate(transform.up * -1);
            soulSpawnAudio.Play();

            GameObject infernalSoul3 = objectPooler.SpawnFromPool("InfernalSoul", transform.position, transform.rotation);
            infernalSoul3.transform.Rotate(0, 0, Random.Range(180, 270));
            infernalSoul3.transform.Translate(transform.up * -1);
            soulSpawnAudio.Play();

            GameObject infernalSoul4 = objectPooler.SpawnFromPool("InfernalSoul", transform.position, transform.rotation);
            infernalSoul4.transform.Rotate(0, 0, Random.Range(270, 360));
            infernalSoul4.transform.Translate(transform.up * -1);
            soulSpawnAudio.Play();
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            portalHealth -= collision.gameObject.GetComponent<ProjectileDamage>().projectileDamage;
            colorInfo.color = Color.red;
            Invoke("ResetColor", 0.50f);
        }
    }


    private void ResetColor()
    {
        colorInfo.color = Color.white;
    }
}
