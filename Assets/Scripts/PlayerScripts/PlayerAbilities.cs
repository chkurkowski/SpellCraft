using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {

    //Public World Variables
    public GameObject fireball;
    public GameObject reflect;
    public PlayerMovement movement;
    public Vector2 cursorInWorldPos;

    //FSM Variables
    public enum State {
        IDLE,
        LONGATK,
        EVADE,
        REFLECT,
        ATKDASH,
        ABSORB,
        LR
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
    private const float EVADECOOLDOWN = .6f;
    private const float REFLECTCOOLDOWN = 3f;
    private float longATKTimer;
    private float evadeTimer;
    private float reflectTimer;

	// Use this for initialization
	void Start () {
        alive = true;
        evadeCalled = false;
        longATKTimer = LONGATKCOOLDOWN;
        evadeTimer = EVADECOOLDOWN;
        reflectTimer = REFLECTCOOLDOWN;

        movement = GetComponent<PlayerMovement>();

        state = State.IDLE;

        StartCoroutine("FSM");
	}
	
    IEnumerator FSM()
    {
        while(alive)
        {
            print(state);
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
            }
            yield return null;
        }
    }

    public void Idle()
    {
        TimerHandler();

        cursorInWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0) && longATKTimer >= LONGATKCOOLDOWN)
        {
            longATKTimer = 0f;
            state = State.LONGATK;
        }

        if(Input.GetKeyDown(KeyCode.Space) && evadeTimer >= EVADECOOLDOWN)
        {
            evadeTimer = 0f;
            gameObject.GetComponent<Collider2D>().enabled = false;
            state = State.EVADE;
        }

        if(Input.GetKeyDown(KeyCode.Mouse1) && reflectTimer >= REFLECTCOOLDOWN)
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
        Vector2 direction = cursorInWorldPos - new Vector2(transform.position.x, transform.position.y);
        direction.Normalize();
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction * dashSpeed, ForceMode2D.Impulse);
        state = State.IDLE;
    }

    public void Reflect()
    {
        reflect.SetActive(true);
        state = State.IDLE;
    }

    private void AttackDash()
    {

    }

    private void Absorb()
    {

    }

    /*
    public void UndeterminedAttack()
    {

    }
    */

    private void TimerHandler()
    {
        longATKTimer += Time.deltaTime;
        evadeTimer += Time.deltaTime;
        reflectTimer += Time.deltaTime;



        if(evadeTimer >= EVADECOOLDOWN)
            gameObject.GetComponent<Collider2D>().enabled = true;
        if (reflectTimer >= 2f)
            reflect.SetActive(false);
    }

    public float GetTimer(string str)
    {
        if (str == "attack")
            return longATKTimer;
        else if (str == "evade")
            return evadeTimer;
        else
            return reflectTimer;
    }

    public float GetCooldown(string str)
    {
        if (str == "attack")
            return LONGATKCOOLDOWN;
        else if (str == "evade")
            return EVADECOOLDOWN;
        else
            return REFLECTCOOLDOWN;
    }
}
