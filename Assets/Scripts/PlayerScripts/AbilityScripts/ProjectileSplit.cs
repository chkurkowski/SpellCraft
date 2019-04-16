using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSplit : MonoBehaviour {

	public int lifetime = 6;

	void Start()
	{
		Invoke("Destroy", lifetime);
	}

	private void Destroy()
	{
		Destroy(gameObject.transform.parent.gameObject);
	}

	private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "MagicMissile(Clone)")
        {
            GameObject gm1 = Instantiate(col.gameObject, col.gameObject.transform.position + (gameObject.transform.right * -8), col.gameObject.transform.rotation);
            gm1.GetComponent<MagicBall>().isSplitSimMissile = true;
            GameObject gm2 = Instantiate(col.gameObject, col.gameObject.transform.position + (gameObject.transform.right * 8), col.gameObject.transform.rotation);
            gm2.GetComponent<MagicBall>().isSplitSimMissile = true;

            gm1.GetComponent<Rigidbody2D>().velocity = col.gameObject.GetComponent<Rigidbody2D>().velocity; //* ((gameObject.transform.right) + (gameObject.transform.up));
            gm2.GetComponent<Rigidbody2D>().velocity = col.gameObject.GetComponent<Rigidbody2D>().velocity; //* (-(gameObject.transform.right) + (gameObject.transform.up));
        
            //Vector2 direction = cursorInWorldPos - new Vector2(transform.position.x, transform.position.y);
            //fb.GetComponent<Rigidbody2D>().velocity = (direction + new Vector2(tempX, 0)) * atkSpeed;
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
