using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonRage : MonoBehaviour
{
    private BossHealth bossHealthInfo;
    private BossInfo bossInfo;
    // Use this for initialization
    void Start()
    {
        bossHealthInfo = gameObject.GetComponent<BossHealth>();
        bossInfo = gameObject.GetComponent<BossInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossHealthInfo.bossHealth <= (bossHealthInfo.bossMaxHealth * 0.6f))
        {
            bossInfo.SetRageAmount(50);
        }
        if (bossHealthInfo.bossHealth <= (bossHealthInfo.bossMaxHealth / 3))
        {
            bossInfo.SetRageAmount(75);
        }
    }
}
