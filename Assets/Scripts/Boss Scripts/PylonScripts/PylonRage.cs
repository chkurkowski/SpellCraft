using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonRage : BossHealth
{

    private BossInfo bossInfo;
    // Use this for initialization
    void Start()
    {
        bossInfo = gameObject.GetComponent<BossInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossHealth <= (bossMaxHealth / (1.5f)))
        {
            bossInfo.SetRageAmount(50);
        }
        if (bossHealth <= (bossMaxHealth / 3))
        {
            bossInfo.SetRageAmount(75);
        }
    }
}
