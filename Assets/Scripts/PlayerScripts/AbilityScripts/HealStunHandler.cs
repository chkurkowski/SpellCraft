using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealStunHandler : MonoBehaviour {

	public enum State
    {
    	NULL,
        STUNABSORB,
        HEALABSORB
    }
    public State state;

    public GameObject stunPrefab;
    public GameObject healPrefab;

    protected GameObject stun;
    protected GameObject heal;
    
    protected const float STUNCAP = 25f;
    protected const float HEALCAP = 25f;

    [SerializeField]
    protected float lifetime = 5f;

	// Use this for initialization
	public virtual void Start () 
	{
		Invoke("Destroy", lifetime);
		state = State.NULL;
	}

    /* Testing
    void Update()
    {
        if(Input.GetKey(KeyCode.T))
        {
            StunAbsorb();
        }

        if(Input.GetKey(KeyCode.G))
        {
            HealAbsorb();
        }
    }
    */

    protected virtual bool StunAbsorb()
    {
    	if(state == State.NULL)
    		state = State.STUNABSORB;
        
        if(state == State.STUNABSORB)
        {
            if(stun == null)
            {
                // stun = Instantiate(stunPrefab, transform.position + (transform.up * 10f), Quaternion.identity);
                stun = Instantiate(stunPrefab, transform.position, Quaternion.identity);
                if(stun.GetComponent<StunProjectile>().AddAmount(10) >= 30)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                stun.transform.localScale += new Vector3(12.0f, 12.0f, 0f);
                gameObject.transform.localScale += new Vector3(5.0f, 5.0f, 0f);
                if(stun.GetComponent<StunProjectile>().AddAmount(10) >= 30)
                {
                    Destroy(gameObject);
                }
            }
            return true;
        }
        return false;
    }

    protected virtual bool HealAbsorb()
    {
    	if(state == State.NULL)
    		state = State.HEALABSORB;
        
        if(state == State.HEALABSORB)
        {
            if(heal == null)
            {
                // heal = Instantiate(healPrefab, transform.position + -(transform.up * 7.5f), Quaternion.identity);
                heal = Instantiate(healPrefab, transform.position, Quaternion.identity);
                if(heal.GetComponent<HealPickup>().AddHealAmount(8) >= 40)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                heal.transform.localScale += new Vector3(12.0f, 12.0f, 0f);
                gameObject.transform.localScale += new Vector3(5.0f, 5.0f, 0f);
                if(heal.GetComponent<HealPickup>().AddHealAmount(8) >= 40)
                {
                    Destroy(gameObject);
                }
            }
            return true;
        }
        return false;
    }

    private void Destroy()
    {
	   Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Projectile")
        {
            if(StunAbsorb())
                Destroy(col.gameObject);
        }

        if(col.gameObject.tag == "EnemyProjectile")
        {
            if(HealAbsorb())
                Destroy(col.gameObject);
        }
    }
}
