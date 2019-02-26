using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {

	//Public Editor Variables
    [Header("Editor Variables - Don't Touch")]
	public PlayerMovement movement;
	public PlayerHealth health;
	private AbilityHandler handlers;
	private ParticleSystem pSystem;

    [Space(10)]

	//Dash Variable
    [Header("Dash Variables")]
	public float dashSpeed;
	public float dashDistance;

    //States
    public enum State
    {
        IDLE,
        ABILITY,
        EVADE,
        RITUALCAST,
        BURSTCAST,
        STUN
    }
    [Space(10)]
    [Header("Player State")]
    public State state;

    [Space(10)]

    //audio
    [Header("Audio Sources")]
    public AudioSource reflectAudio;
    public AudioSource ritualAudio;
    public AudioSource evadeAudio;

    public AudioClip reflectSound;
    public AudioClip evadeSound;
    public AudioClip ritualSound;

	//Booleans
	// private bool isBurst;
	// private bool evadeCalled;

	public List<string> lastAttacks = new List<string>();
	private List<string> ritualList = new List<string>();

    [Space(10)]

    [Header("Cooldowns and Timers")]
    public float BURSTCOOLDOWN = 2f;
    public float EVADECOOLDOWN = .25f;

    public  float evadeEnd = .35f;

    private float burstTimer, evadeTimer;

    [Space(10)]

	//Ability Variables
    [Header("Ability Variables")]
	public int leftMouseAbility = 1;
	public int rightMouseAbility = 2;
	public int keyboardAbility = 3;

    //Constant combo variables
	private const int ATTACKSIM = 1;
	private const int ABSORB = 2;
	private const int ABSORBSIM = 3;
    private const int HEALSTUNCOMBO = 4;

	private void Start()
	{
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
			//print(state);
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
        }

		//Right Click Ability
		if(Input.GetKeyDown(KeyCode.Mouse1))
		{
            // reflectAudio.clip = ritualSound;
            // reflectAudio.Play();
            // Invoke("StopReflectAudioSound", reflectAudio.clip.length);
            handlers.AbilityChecker(rightMouseAbility, false, false);
		}

		//E or F Ability
		if(Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.E))
		{
			handlers.AbilityChecker(keyboardAbility, false, false);
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

        //Middle Mouse and Q Burst Cast
        if (Input.GetKeyDown(KeyCode.Mouse2) || Input.GetKeyDown(KeyCode.Q))
        {
            state = State.BURSTCAST;
        }

        if(pSystem.isPlaying)
        {
            pSystem.Stop();
        }

        movement.slowed = false;
        TimerHandlers();
	}

    // private void StopReflectAudioSound()
    // {
    //      reflectAudio.Stop();
    // }

    private void Evade()
	{
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
            if (ritualList.Contains("MagicMissile") && ritualList.Contains("HealStun"))
            {
            	handlers.AbilityChecker(ATTACKSIM, true, false);
                ritualList.Clear();
            }
            else if (ritualList.Contains("MagicMissile") && ritualList.Contains("Reflect"))
            {
            	handlers.AbilityChecker(ABSORBSIM, true, false);
                ritualList.Clear();
            }
            else if (ritualList.Contains("HealStun") && ritualList.Contains("Reflect"))
            {
	        	handlers.AbilityChecker(ABSORB, true, false);
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
		if (lastAttacks.Contains("MagicMissile") && lastAttacks.Contains("HealStun") && burstTimer >= BURSTCOOLDOWN)
        {
            handlers.AbilityChecker(ATTACKSIM, true, true);
            burstTimer = 0;
        }
        else if(lastAttacks.Contains("MagicMissile") && lastAttacks.Contains("Reflect") && burstTimer >= BURSTCOOLDOWN)
        {
            handlers.AbilityChecker(ABSORBSIM, true, true);
            burstTimer = 0;
        }
        else if(lastAttacks.Contains("Reflect") && lastAttacks.Contains("HealStun") && burstTimer >= BURSTCOOLDOWN)
        {
            handlers.AbilityChecker(ABSORB, true, true);
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
	public void AttackArrayHandler(string newAttack, List<string> list)
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
            if(leftMouseAbility == 1)
            {
                AttackArrayHandler("MagicMissile", ritualList);
            }
            else
            {
                AttackArrayHandler("Golem", ritualList);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if(rightMouseAbility == 2)
            {
                AttackArrayHandler("Reflect", ritualList);
            }
            else
            {
                AttackArrayHandler("AbsorbExplode", ritualList);
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.F))
        {
            if(keyboardAbility == 3)
            {
                AttackArrayHandler("HealStun", ritualList);
            }
            else
            {
                AttackArrayHandler("ProjectileSplit", ritualList);
            }
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