using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpeed : MonoBehaviour {

	public int lifetime = 6;
	public float speedMultiplier = 2.5f;
	public float damageMultiplier = 1.5f;

	void Start()
	{
		Invoke("EndAnimation", lifetime - 1);
        Destroy(gameObject, lifetime);
	}

	private void Destroy()
	{
		Destroy(gameObject);
	}

    public void EndAnimation()
    {
        gameObject.GetComponent<Animator>().SetBool("isDead", true);
    }

	private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "MagicMissile(Clone)")
        {
            col.GetComponent<Rigidbody2D>().velocity *= speedMultiplier;
            col.GetComponent<MagicBall>().magicBallDamage *= damageMultiplier;
            PlayerAbilities.instance.AddToResource(.025f);
        }
        else if(col.gameObject.tag == "EnemyProjectile")
        {
            if(col.GetComponent<Bomb>())
            {
                col.GetComponent<Bomb>().fireBallSpeed = col.GetComponent<Bomb>().fireBallSpeed/2;
            }
            else if(col.GetComponent<Fireball>())
            {
                col.GetComponent<Fireball>().fireBallSpeed = col.GetComponent<Fireball>().fireBallSpeed/2;
            }
            else if(col.GetComponent<MegaBomb>())
            {
                col.GetComponent<MegaBomb>().fireBallSpeed = col.GetComponent<MegaBomb>().fireBallSpeed/2;
            }
            else if(col.GetComponent<PylonLaserShard>())
            {
                col.GetComponent<PylonLaserShard>().laserShardSpeed = col.GetComponent<PylonLaserShard>().laserShardSpeed/2;
            }
        }
    }
}