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
    private bool reflectCalled;
    private bool evadeCalled;

    //Ability timers
    private float LONGATKCOOLDOWN = 1f;
    private float EVADECOOLDOWN = 1f;
    private float REFLECTCOOLDOWN = 1f;
    private float longATKTimer;
    private float evadeTimer;
    private float reflectTimer;

	// Use this for initialization
	void Start () {
        alive = true;
        reflectCalled = false;
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
                    EvadeHandler();
                    break;
                case State.REFLECT:
                    ReflectHandler();
                    break;
            }
            yield return null;
        }
    }

    public void Idle()
    {
        cursorInWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Mouse0) && longATKTimer >= LONGATKCOOLDOWN)
        {
            state = State.LONGATK;
        }

        if(Input.GetKeyDown(KeyCode.Space) && evadeTimer >= EVADECOOLDOWN)
        {
            state = State.EVADE;
        }

        if(Input.GetKeyDown(KeyCode.R) && reflectTimer >= REFLECTCOOLDOWN)
        {
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

    public void EvadeHandler()
    {
        if(!evadeCalled)
        {
            evadeCalled = true;
            StartCoroutine("Evade");
        }
    }

    IEnumerator Evade()
    {
        Vector2 direction = cursorInWorldPos - new Vector2(transform.position.x, transform.position.y);
        direction.Normalize();
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction * dashSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(.5f);
        evadeCalled = false;
        state = State.IDLE;
    }

    public void ReflectHandler()
    {
        if(!reflectCalled)
        {
            reflectCalled = true;
            StartCoroutine("Reflect");
        }
    }

    IEnumerator Reflect()
    {
        reflect.SetActive(true);
        yield return new WaitForSeconds(2f);
        reflect.SetActive(false);
        reflectCalled = false;
        state = State.IDLE;
    }

    private void Timers(string str)
    {

    }
}
