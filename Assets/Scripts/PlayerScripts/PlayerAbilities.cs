using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {

    //Public World Variables
    public GameObject fireball;
    public GameObject reflect;
    public GameObject absorb;
    public GameObject dashCollider;
    public PlayerMovement movement;
    public PlayerHealth health;
    public Vector2 cursorInWorldPos;
    public GameObject simulacrum;


    //FSM Variables
    public enum State
    {
        IDLE,
        LONGATK,
        EVADE,
        REFLECT,
        ATKSIM,
        ATKHANDLER,
        ABSORB,
        REFLECTSIM
    }
    public State state;

    //Attack Variables
    public float atkSpeed = 25;

    //Dash Variables
    public float dashSpeed;
    public float dashDistance;

    //Booleans
    private bool alive;
    private bool evadeCalled;

    //Ability timers
    private const float LONGATKCOOLDOWN = .35f;
    private const float EVADECOOLDOWN = .5f;
    private const float REFLECTCOOLDOWN = 3f;
    private const float ATKDASHCOOLDOWN = 3f;
    private const float ABSORBCOOLDOWN = 6f;
    private const float SWAPTELEPORTCOOLDOWN = 6f;

    private const float ATKDASHEND = .6f;
    private const float ABSORBEND = 3f;

    private float longATKTimer, evadeTimer, reflectTimer, 
    atkDashTimer, absorbTimer, swapTeleportTimer;


	// Use this for initialization
	void Start () {
        alive = true;
        evadeCalled = false;
        longATKTimer = LONGATKCOOLDOWN;
        evadeTimer = EVADECOOLDOWN;
        reflectTimer = REFLECTCOOLDOWN;
        atkDashTimer = ATKDASHCOOLDOWN;
        absorbTimer = ABSORBCOOLDOWN;
        swapTeleportTimer = SWAPTELEPORTCOOLDOWN;

        movement = GetComponent<PlayerMovement>();
        health = GetComponent<PlayerHealth>();

        state = State.IDLE;

        StartCoroutine("FSM");
	}
	
    IEnumerator FSM()
    {
        while(health.isAlive)
        {
            //print(state);
            switch (state)
            {
                case State.IDLE:
                    Idle();
                    break;
                case State.LONGATK:
                    LongAttack();
                    break;
                case State.EVADE:
                    Evade();
                    break;
                case State.REFLECT:
                    Reflect();
                    break;
                case State.ATKSIM:
                    AttackSimulacrum();
                    break;
                case State.ATKHANDLER:
                    AttackDashHandler();
                    break;
                case State.ABSORB:
                    Absorb();
                    break;
                case State.REFLECTSIM:
                    ReflectSimulacrum();
                    break;
            }
            yield return null;
        }
    }

    public void Idle()
    {
        TimerHandler();
        
        cursorInWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (absorb.activeSelf && reflect.activeSelf)
            reflect.SetActive(false);

        if(Input.GetKeyDown(KeyCode.Mouse0) && evadeTimer <= EVADECOOLDOWN && atkDashTimer >= ATKDASHCOOLDOWN)
        {
            atkDashTimer = 0f;
            state = State.ATKHANDLER;
        }
        else if (Input.GetKey(KeyCode.Mouse0) && longATKTimer >= LONGATKCOOLDOWN)
        {
            longATKTimer = 0f;
            state = State.LONGATK;
        }

        if(Input.GetKeyDown(KeyCode.Space) && reflectTimer <= REFLECTCOOLDOWN && absorbTimer >= ABSORBCOOLDOWN)
        {
            absorbTimer = 0f;
            state = State.ABSORB;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && longATKTimer <= LONGATKCOOLDOWN && atkDashTimer >= ATKDASHCOOLDOWN)
        {
            atkDashTimer = 0f;
            state = State.ATKSIM;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && evadeTimer >= EVADECOOLDOWN)
        {
            evadeTimer = 0f;
            //gameObject.GetComponent<Collider2D>().isTrigger = true;
            state = State.EVADE;
        }

        if(Input.GetKeyDown(KeyCode.Mouse1) && evadeTimer <= EVADECOOLDOWN && absorbTimer >= ABSORBCOOLDOWN)
        {
            absorbTimer = 0f;
            state = State.ABSORB;
        }
        else if(Input.GetKeyDown(KeyCode.Mouse1) && reflectTimer >= REFLECTCOOLDOWN)
        {
            reflectTimer = 0f;
            state = State.REFLECT;
        }
    }

    public void LongAttack()
    {
        Vector2 direction = cursorInWorldPos - new Vector2(transform.position.x, transform.position.y);
        direction.Normalize();
        GameObject fb = Instantiate(fireball, transform.position, Quaternion.identity);
        fb.GetComponent<Rigidbody2D>().velocity = direction * atkSpeed;
        state = State.IDLE;
    }

    public void Evade()
    {
        Vector2 direction = new Vector2(movement.horizontalMovement, movement.verticalMovement);
        direction.Normalize();
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction * dashSpeed, ForceMode2D.Impulse);
        gameObject.layer = 14;// changes physics layers to avoid collision
        Invoke("ResetPhysicsLayer", 1);//basically delays physics layer reset to give player invincibility frames.
        state = State.IDLE;
    }

    public void ResetPhysicsLayer()
    {
        if (gameObject.layer != 0)
        {
            gameObject.layer = 0;//reset's the player's physics layer.
        }
    }

    public void Reflect()
    {
        reflect.SetActive(true);
        state = State.IDLE;
    }

    private void AttackSimulacrum()
    {

        state = State.IDLE;
    }

    private void AttackDashHandler()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        dashCollider.SetActive(true);
        state = State.IDLE;
    }

    private void Absorb()
    {
        print("absorb");
        absorb.SetActive(true);
        state = State.IDLE;
    }

    public void ReflectSimulacrum()
    {

    }

    private void TimerHandler()
    {
        longATKTimer += Time.deltaTime;
        evadeTimer += Time.deltaTime;
        reflectTimer += Time.deltaTime;
        atkDashTimer += Time.deltaTime;
        absorbTimer += Time.deltaTime;

        if(evadeTimer >= EVADECOOLDOWN)
            gameObject.GetComponent<Collider2D>().isTrigger = false;
        if (reflectTimer >= 2f)
            reflect.SetActive(false);
        if(atkDashTimer >= ATKDASHEND)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
            dashCollider.SetActive(false);
        }
        if(absorbTimer >= ABSORBEND)
        {
            absorb.SetActive(false);
        }
    }

    public float GetTimer(string str)
    {
        if (str == "attack")
            return longATKTimer;
        else if (str == "evade")
            return evadeTimer;
        else if (str == "reflect")
            return reflectTimer;
        else if (str == "atkdash")
            return atkDashTimer;
        else if (str == "absorb")
            return absorbTimer;
        else
            return swapTeleportTimer;
    }

    public float GetCooldown(string str)
    {
        if (str == "attack")
            return LONGATKCOOLDOWN;
        else if (str == "evade")
            return EVADECOOLDOWN;
        else if (str == "reflect")
            return REFLECTCOOLDOWN;
        else if (str == "atkdash")
            return ATKDASHEND;
        else if (str == "absorb")
            return ABSORBCOOLDOWN;
        else
            return SWAPTELEPORTCOOLDOWN;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Environment" && evadeTimer <= EVADECOOLDOWN)
        {
            //print("Hit wall");
            evadeTimer += EVADECOOLDOWN;
        }
    }
}
