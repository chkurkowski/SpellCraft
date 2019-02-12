using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {

	//Public Editor Variables
	public PlayerMovement movement;
	public PlayerHealth health;
	private AbilityHandler handlers;
	private ParticleSystem pSystem;

	//Dash Variable
	public float dashSpeed;
	public float dashDistance;

	public enum State
	{
		IDLE,
		ABILITY,
		EVADE,
		RITUALCAST,
		BURSTCAST,
		STUN
	}
	public State state;

	//Booleans
	private bool isBurst;
	private bool evadeCalled;

	private List<string> lastAttacks = new List<string>();
	private List<string> ritualList = new List<string>();

	//Ability Variables
	private int leftMouseAbility = 1;
	private int rightMouseAbility = 2;
	private int keyboardAbility = 3;

	private const float BURSTCOOLDOWN = 2f;
	private const float EVADECOOLDOWN = .5f;
	private float burstTimer, evadeTimer;

	private void Start()
	{
		isBurst = false;
        evadeCalled = false;
        burstTimer = BURSTCOOLDOWN;
        evadeTimer = EVADECOOLDOWN;

        handlers = GetComponent<AbilityHandler>();
        movement = GetComponent<PlayerMovement>();
        health = GetComponent<PlayerHealth>();
        pSystem = GetComponent<ParticleSystem>();

        state = State.IDLE;

        StartCoroutine(FSM());
	}

	#region FiniteStateMachine

	private IEnumerator FSM()
	{
		while(health.isAlive)
		{
			print(state);
			switch (state)
			{
				case State.IDLE:
					Idle();
					break;
				case State.ABILITY:
					Ability();
					break;
				case State.EVADE:
					Evade();
					break;
				case State.STUN:
					Stun();
					break;
			}
			yield return null;
		}
	}

	#endregion

	#region StateFunctions

	private void Idle()
	{
		//Left Click Ability
		if(Input.GetKeyDown(KeyCode.Mouse0))
		{
			handlers.AbilityChecker(leftMouseAbility, false, false);
		}

		//Right Click Ability
		if(Input.GetKeyDown(KeyCode.Mouse1))
		{
			handlers.AbilityChecker(rightMouseAbility, false, false);
		}

		//E or F Ability
		if(Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.E))
		{
			handlers.AbilityChecker(keyboardAbility, false, false);
		}

		//Shift Ritual Cast
		if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            state = State.RITUALCAST;
        }

        //Middle Mouse and Q Burst Cast
        if (Input.GetKeyDown(KeyCode.Mouse2) || Input.GetKeyDown(KeyCode.Q))
        {
            state = State.BURSTCAST;
        }

        pSystem.Stop();
        TimerHandlers();
	}

	private void Ability()
	{

	}

	private void Evade()
	{

	}

	private void RitualCast()
	{
		pSystem.Play();
		InputHandler();
		movement.slowed = true;

		if (ritualList.Count == 2)
        {
            if (ritualList.Contains("Projectile") && ritualList.Contains("Zone"))
            {
                ritualList.Clear();
            }
            else if (ritualList.Contains("Projectile") && ritualList.Contains("Self"))
            {
                ritualList.Clear();
            }
            else if (ritualList.Contains("Self") && ritualList.Contains("Zone"))
            {
                ritualList.Clear();
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

	private void BurstCast()
	{
		if (lastAttacks.Contains("Projectile") && lastAttacks.Contains("Zone"))
        {
            print("AtkSim Cast");
            burstTimer = 0;
        }
        else if(lastAttacks.Contains("Projectile") && lastAttacks.Contains("Self"))
        {
            print("Reflect Cast");
            burstTimer = 0;
        }
        else if(lastAttacks.Contains("Self") && lastAttacks.Contains("Zone"))
        {
            print("Absorb Cast");
            burstTimer = 0;
        }
        else
            state = State.IDLE;
	}

	//TODO: Decide if we really want this for the player
	private void Stun()
	{

	}

	#endregion

	#region Handlers
	
	//Handles the Attack Array for the Burst Casting
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

    //Handles the Inputs for the RitualCasting System
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

    private void TimerHandlers()
    {
    	burstTimer += Time.deltaTime;
    	evadeTimer += Time.deltaTime;
    }

	#endregion
}