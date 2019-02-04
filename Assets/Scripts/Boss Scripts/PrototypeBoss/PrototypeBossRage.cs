using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeBossRage : BossHealth
{
    private BossInfo bossInfo;
	// Use this for initialization
	void Start ()
    {
        bossInfo = gameObject.GetComponent<BossInfo>();
	}
	
	// Update is called once per frame
	void Update () {
        if (bossHealth <= (bossMaxHealth / 2))
        {
            bossInfo.SetRageAmount(50);
        }
        if (bossHealth <= (bossMaxHealth / 5))
        {
            bossInfo.SetRageAmount(75);
        }
    }
}
