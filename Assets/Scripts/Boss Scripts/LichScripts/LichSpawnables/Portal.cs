using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    private BossHealth lichBossHealth;
    private SpriteRenderer colorInfo;

    public float portalHealth = 15f;
    public float portalMaxHealth = 15f;

    [Space(25)]

    public float portalDeathDamage = 15f;

    [Space(25)]

    public float skeletonSpawnRate;
    public float hellKnightSpawnRate;
    public float infernalSoulSpawnRate;

    [Space(25)]

    public bool isMad = false;
    public bool isEnraged = false;

	// Use this for initialization
	void Start ()
    {
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

        }
    }

    // Update is called once per frame
    void Update ()
    {
		if(portalHealth <= 0)
        {
            portalHealth = portalMaxHealth;
            lichBossHealth.DealDamage(portalDeathDamage);
            gameObject.SetActive(false);
        }
	}
}
