using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*======================================================
TODO:
* Seperate out all the combos and attacks
* Build out the combo system to fit any given feedback
=======================================================*/

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
        REFLECTSIM,
        RITUALCAST,
        BURSTCAST
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

    private List<string> lastAttacks = new List<string>();
    private List<string> ritualList = new List<string>();

    //Ability timers
    private const float LONGATKCOOLDOWN = .35f;
    private const float EVADECOOLDOWN = .5f;
    private const float REFLECTCOOLDOWN = 3f;
    private const float ATKSIMCOOLDOWN = 2f;
    private const float ABSORBCOOLDOWN = 6f;
    private const float REFLECTSIMCOOLDOWN = 12f;
    private const float BURSTCOOLDOWN = 2f;

    private const float ATKDASHEND = .6f;
    private const float ABSORBEND = 3f;

    private float longATKTimer, evadeTimer, reflectTimer, 
    atkSimTimer, absorbTimer, reflectSimTimer, burstTimer;


	// Use this for initialization
	void Start () {
        alive = true;
        evadeCalled = false;
        longATKTimer = LONGATKCOOLDOWN;
        evadeTimer = EVADECOOLDOWN;
        reflectTimer = REFLECTCOOLDOWN;
        atkSimTimer = ATKSIMCOOLDOWN;
        absorbTimer = ABSORBCOOLDOWN;
        reflectSimTimer = REFLECTSIMCOOLDOWN;
        burstTimer = BURSTCOOLDOWN;

        movement = GetComponent<PlayerMovement>();
        health = GetComponent<PlayerHealth>();

        state = State.IDLE;

        StartCoroutine("FSM");
	}

    #region FiniteStateMachine

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
                case State.ABSORB:
                    Absorb();
                    break;
                case State.REFLECTSIM:
                    ReflectSimulacrum();
                    break;
                case State.RITUALCAST:
                    RitualCast();
                    break;
                case State.BURSTCAST:
                    BurstCast();
                    break;
                    
            }
            yield return null;
        }
    }

    #endregion

    #region GeneralStateFunctions

    public void Idle()
    {
        movement.slowed = false;

        BasicHandlers();

        if (absorb.activeSelf && reflect.activeSelf)
            reflect.SetActive(false);

        // Left click abilities || Base long attack
        if (Input.GetKey(KeyCode.Mouse0) && longATKTimer >= LONGATKCOOLDOWN)
        {
            longATKTimer = 0f;
            AttackArrayHandler("Projectile", lastAttacks);
            state = State.LONGATK;
        }


        //Space abilities || Base evade
        if(Input.GetKeyDown(KeyCode.Space) && evadeTimer >= EVADECOOLDOWN)
        {
            evadeTimer = 0f;
            AttackArrayHandler("Zone", lastAttacks);
            state = State.EVADE;
        }

        //Right Click Abilities || Base Reflect
        if(Input.GetKeyDown(KeyCode.Mouse1) && reflectTimer >= REFLECTCOOLDOWN)
        {
            reflectTimer = 0f;
            AttackArrayHandler("Self", lastAttacks);
            state = State.REFLECT;
        }

        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            state = State.RITUALCAST;
        }

        if (Input.GetKeyDown(KeyCode.Mouse2) || Input.GetKeyDown(KeyCode.Q))
        {
            state = State.BURSTCAST;
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

    #endregion

    #region BaseCombos

    //Offensive Simulacrum = attack + evade
    private void AttackSimulacrum()
    {
        atkSimTimer = 0f;
        GameObject sim = Instantiate(simulacrum, transform.position, Quaternion.identity);
        sim.GetComponent<SimulacrumAbilities>().type = "Attack";
        state = State.IDLE;
    }

    //Absorb = reflect + evade
    private void Absorb()
    {
        absorbTimer = 0f;
        absorb.SetActive(true);
        state = State.IDLE;
    }

    //Defensive Simulacrum = reflect + attack
    public void ReflectSimulacrum()
    {
        reflectSimTimer = 0f;
        GameObject sim = Instantiate(simulacrum, transform.position, Quaternion.identity);
        sim.GetComponent<SimulacrumAbilities>().type = "Absorb";
        state = State.IDLE;
    }

    #endregion

    #region ComboHandling

    private void RitualCast()
    {
        print("Ritual Casting");
        print("List has " + ritualList.Count + " moves.");

        movement.slowed = true;

        BasicHandlers();

        InputHandler();

        if (ritualList.Count == 2)
        {
            if (ritualList.Contains("Projectile") && ritualList.Contains("Zone") && atkSimTimer >= ATKSIMCOOLDOWN)
            {
                print("AtkSim Cast");
                atkSimTimer = 0;
                ritualList.Clear();
                state = State.ATKSIM;
            }
            else if (ritualList.Contains("Projectile") && ritualList.Contains("Self") && reflectSimTimer >= REFLECTSIMCOOLDOWN)
            {
                print("Reflect Cast");
                reflectSimTimer = 0;
                ritualList.Clear();
                state = State.REFLECTSIM;
            }
            else if (ritualList.Contains("Self") && ritualList.Contains("Zone") && absorbTimer >= ABSORBCOOLDOWN)
            {
                print("Absorb Cast");
                absorbTimer = 0;
                ritualList.Clear();
                state = State.ABSORB;
            }
            else
                ritualList.Clear();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            ritualList.Clear();
            state = State.IDLE;
        }
    }

    private void InputHandler()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            AttackArrayHandler("Projectile", ritualList);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            AttackArrayHandler("Self", ritualList);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            AttackArrayHandler("Zone", ritualList);
        }
    }

    private void BurstCast()
    {
        if (lastAttacks.Contains("Projectile") && lastAttacks.Contains("Zone") && burstTimer >= BURSTCOOLDOWN)
        {
            print("AtkSim Cast");
            //LastAttacks.Clear();
            burstTimer = 0;
            state = State.ATKSIM;
        }
        else if(lastAttacks.Contains("Projectile") && lastAttacks.Contains("Self") && burstTimer >= BURSTCOOLDOWN)
        {
            print("Reflect Cast");
            //LastAttacks.Clear();
            burstTimer = 0;
            state = State.REFLECTSIM;
        }
        else if(lastAttacks.Contains("Self") && lastAttacks.Contains("Zone") && burstTimer >= BURSTCOOLDOWN)
        {
            print("Absorb Cast");
            //LastAttacks.Clear();
            burstTimer = 0;
            state = State.ABSORB;
        }
        else
            state = State.IDLE;
    }

    private void AttackArrayHandler(string newAttack, List<string> list)
    {
        if(!list.Contains(newAttack))
        {
            if (list.Count >= 2)
            {
                list.RemoveAt(1);
            }
            list.Insert(0, newAttack);
        }
    }

    #endregion

    #region Handlers

    private void BasicHandlers()
    {
        TimerHandler();

        cursorInWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void TimerHandler()
    {
        longATKTimer += Time.deltaTime;
        evadeTimer += Time.deltaTime;
        reflectTimer += Time.deltaTime;
        atkSimTimer += Time.deltaTime;
        absorbTimer += Time.deltaTime;
        reflectSimTimer += Time.deltaTime;
        burstTimer += Time.deltaTime;

        if (evadeTimer >= EVADECOOLDOWN)
            gameObject.GetComponent<Collider2D>().isTrigger = false;
        if (reflectTimer >= 2f)
            reflect.SetActive(false);
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
            return atkSimTimer;
        else if (str == "absorb")
            return absorbTimer;
        else
            return reflectSimTimer;
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
            return ATKSIMCOOLDOWN;
        else if (str == "absorb")
            return ABSORBCOOLDOWN;
        else
            return REFLECTSIMCOOLDOWN;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Environment" && evadeTimer <= EVADECOOLDOWN)
        {
            //print("Hit wall");
            evadeTimer += EVADECOOLDOWN;
        }
    }

    #endregion
}
