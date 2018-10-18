using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShieldScript : MonoBehaviour {
    public float bossShieldHealth = 4f;
    private BossBehaviours bossBehaviourInfo;
	// Use this for initialization
	void Start () {
        bossBehaviourInfo = GameObject.Find("Boss").GetComponent<BossBehaviours>();
	}
	
	// Update is called once per frame
	void Update () {
		if(bossShieldHealth <=0)
        {
            bossBehaviourInfo.isActivated = true;
            Destroy(gameObject);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Projectile")
        {
            bossShieldHealth--;
        }
    }
}
