using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {

    public GameObject fireball;
    public GameObject reflect;
    public PlayerMovement movement;

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
    public float atkSpeed = 25;
    public Vector2 cursorInWorldPos;

    private bool alive;
    private bool coroutineCalled;

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
        coroutineCalled = false;
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

    public void Evade()
    {

        state = State.IDLE;
    }

    public void ReflectHandler()
    {
        if(!coroutineCalled)
        {
            coroutineCalled = true;
            StartCoroutine("Reflect");
        }
    }

    IEnumerator Reflect()
    {
        reflect.SetActive(true);
        yield return new WaitForSeconds(2f);
        reflect.SetActive(false);
        coroutineCalled = false;
        state = State.IDLE;
    }

    private void Timers(string str)
    {

    }
}
