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

    //audio
    public AudioSource reflectAudio;
    public AudioSource ritualAudio;
    public AudioSource evadeAudio;

    public AudioClip reflectSound;
    public AudioClip evadeSound;
    public AudioClip ritualSound;


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
	private int comboOne = 7;
	private int comboTwo = 8;
	private int comboThree = 9;

	private const float BURSTCOOLDOWN = 2f;
	private const float EVADECOOLDOWN = .25f;

	private  float evadeEnd = .35f;

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
			print(lastAttacks.Count);
			switch (state)
			{
				case State.IDLE:
					Idle();
					break;
				case State.EVADE:
					Evade();
					break;
				case State.STUN:
					Stun();
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

	#region StateFunctions

	private void Idle()
	{
        ritualAudio.Stop();
        reflectAudio.Stop();

		//Left Click Ability
		if(Input.GetKey(KeyCode.Mouse0))
		{
			handlers.AbilityChecker(leftMouseAbility, false, false);
			AttackArrayHandler("Projectile", lastAttacks);

        }

		//Right Click Ability
		if(Input.GetKeyDown(KeyCode.Mouse1))
		{
            ritualAudio.clip = ritualSound;
            ritualAudio.Play();
            handlers.AbilityChecker(rightMouseAbility, false, false);
			AttackArrayHandler("Self", lastAttacks);
		}

		//E or F Ability
		if(Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.E))
		{
			handlers.AbilityChecker(keyboardAbility, false, false);
			AttackArrayHandler("Zone", lastAttacks);
		}

		//Evade
		if(Input.GetKeyDown(KeyCode.Space) && evadeTimer > EVADECOOLDOWN)
		{
            evadeAudio = GetComponent<AudioSource>();
            evadeAudio.PlayOneShot(evadeSound);
            state = State.EVADE;
			evadeTimer = 0;
		}

		//Shift Ritual Cast
		if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            ritualAudio = GetComponent<AudioSource>();
            ritualAudio.clip = ritualSound;
            ritualAudio.Play();
            state = State.RITUALCAST;
        }
        else //if (!Input.anyKey)
        {
           // abilityAudio.loop = false;
        }

        //Middle Mouse and Q Burst Cast
        if (Input.GetKeyDown(KeyCode.Mouse2) || Input.GetKeyDown(KeyCode.Q))
        {
            state = State.BURSTCAST;
        }

        pSystem.Stop();
        TimerHandlers();
	}

	private void Evade()
	{
        //(done)TODO add evade sound here 

        Vector2 direction = new Vector2(movement.horizontalMovement, movement.verticalMovement);
        direction.Normalize();
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction * dashSpeed, ForceMode2D.Impulse);
        gameObject.layer = 14;// changes physics layers to avoid collision
        Invoke("ResetPhysicsLayer", evadeEnd);//basically delays physics layer reset to give player invincibility frames.
	}

	private void RitualCast()
	{
        //(done) TODO Play any ritual sounds here, this would be a sound while you hold the ritual cast button before a combo cast.
        pSystem.Play();
		InputHandler();
		movement.slowed = true;

		if (ritualList.Count == 2)
        {
            if (ritualList.Contains("Projectile") && ritualList.Contains("Zone"))
            {
            	print("hitone");
            	handlers.AbilityChecker(comboOne, true, false);
                ritualList.Clear();
            }
            else if (ritualList.Contains("Projectile") && ritualList.Contains("Self"))
            {
            	print("hittwo");
            	handlers.AbilityChecker(comboThree, true, false);
                ritualList.Clear();
            }
            else if (ritualList.Contains("Self") && ritualList.Contains("Zone"))
            {
	        	print("hitthree");
	        	handlers.AbilityChecker(comboTwo, true, false);
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
		if (lastAttacks.Contains("Projectile") && lastAttacks.Contains("Zone") && burstTimer >= BURSTCOOLDOWN)
        {
            handlers.AbilityChecker(comboOne, true, true);
            burstTimer = 0;
        }
        else if(lastAttacks.Contains("Projectile") && lastAttacks.Contains("Self") && burstTimer >= BURSTCOOLDOWN)
        {
            handlers.AbilityChecker(comboThree, true, true);
            burstTimer = 0;
        }
        else if(lastAttacks.Contains("Self") && lastAttacks.Contains("Zone") && burstTimer >= BURSTCOOLDOWN)
        {
            handlers.AbilityChecker(comboTwo, true, true);
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

    //TODO: Maybe change to 13 - player layer
    public void ResetPhysicsLayer()
    {
        if (gameObject.layer != 13)
        {
            gameObject.layer = 13;//reset's the player's physics layer.
        }
        state = State.IDLE;
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
        else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.F))
        {
            AttackArrayHandler("Zone", ritualList);
        }
    }

    private void TimerHandlers()
    {
    	burstTimer += Time.deltaTime;
    	evadeTimer += Time.deltaTime;
    }

    public float GetTimer(string str)
    {
        if (str == "evade")
            return evadeTimer;
        else
            return burstTimer;
    }

    public float GetCooldown(string str)
    {
        if (str == "evade")
            return EVADECOOLDOWN;
        else
            return BURSTCOOLDOWN;
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