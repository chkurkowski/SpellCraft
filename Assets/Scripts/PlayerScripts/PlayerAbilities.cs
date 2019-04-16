using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilities : MonoBehaviour {

    public static PlayerAbilities instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

	//Public Editor Variables
    [Header("Editor Variables - Don't Touch")]
	public PlayerMovement movement;
	public PlayerHealth health;
	public AbilityHandler handlers;
	private ParticleSystem pSystem;
    public GameObject dashAnim;

    [Space(10)]

    [Header("Combo Resource Amount")]
    [SerializeField] [Range(0, 3)]
    private float comboResource = 0f;
    private float COMBORESOURCEMAX = 3f;
    private float comboResourceRegenRate = .05f;
    public Image resourceBar;

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
    public float BURSTCOOLDOWN = .5f;
    public float EVADECOOLDOWN = .25f;
    public float EVADELENGTHTIME = .1f;

    public  float evadeEnd = .35f;

    private float burstTimer, evadeTimer, evadeLengthTimer;

    [Space(10)]

    [Range(1, 3)]
    public int dashType = 1;

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

    private Animator playerAnimator;

	private void Start()
	{
        burstTimer = BURSTCOOLDOWN;
        evadeTimer = EVADECOOLDOWN;
        evadeLengthTimer = EVADELENGTHTIME;

        playerAnimator = gameObject.GetComponent<Animator>();
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
            ResourceRegenHandler();

			switch (state)
			{
				case State.IDLE:
					Idle();
					break;
				case State.EVADE:
					if(dashType == 1)
                        Evade();
                    else if(dashType == 2)
                        DashOptionTwo();
                    else
                        DashOptionThree();
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
		if(Input.GetKey(KeyCode.Mouse1))
        {
            // reflectAudio.clip = ritualSound;
            // reflectAudio.Play();
            // Invoke("StopReflectAudioSound", reflectAudio.clip.length);
            handlers.AbilityChecker(rightMouseAbility, false, false);
        }
        else if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            handlers.CancelReflect();
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
		// if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        // {
        //     ritualAudio = GetComponent<AudioSource>();
        //     ritualAudio.clip = ritualSound;
        //     ritualAudio.Play();
        //     state = State.RITUALCAST;
        // }

        //Middle Mouse and Q Burst Cast
        if (Input.GetKeyDown(KeyCode.Mouse2) || Input.GetKeyDown(KeyCode.Q))
        {
            state = State.BURSTCAST;
        }

        // if(pSystem.isPlaying)
        // {
        //     pSystem.Stop();
        // }

        movement.slowed = false;
        TimerHandlers();
	}

    private void Evade()
	{
        EvadeAnimations();

        Vector3 direction = new Vector3(movement.horizontalMovement, movement.verticalMovement);
        direction.Normalize();
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction * dashSpeed, ForceMode2D.Impulse);


        gameObject.layer = 14;// changes physics layers to avoid collision
        Invoke("ResetPhysicsLayer", evadeEnd);//basically delays physics layer reset to give player invincibility frames.
	}

    private bool CanMove(Vector3 dir, float dist)
    {
        print(Physics2D.Raycast(transform.position, dir, dist).collider.name);
        return Physics2D.Raycast(transform.position, dir, dist).collider == null;
    }


    private void DashOptionTwo()
    { 
        Vector3 direction = new Vector3(Mathf.Ceil(movement.horizontalMovement), Mathf.Ceil(movement.verticalMovement));
        direction.Normalize();
        // if(CanMove(direction, dashDistance))
            transform.position += direction * dashDistance;
        state = State.IDLE;
    }

    private void DashOptionThree()
    {
        Vector3 direction = new Vector3(Mathf.Ceil(movement.horizontalMovement), Mathf.Ceil(movement.verticalMovement));
        direction.Normalize();
        if (direction != Vector3.zero)
        {
            //EvadeAnimations();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            GameObject dashAnimObj = Instantiate(dashAnim, transform.position, transform.rotation);
            dashAnimObj.transform.eulerAngles = new Vector3(0, 0, angle - 90);

            movement.canMove = false;
            gameObject.layer = 14;// changes physics layers to avoid collision

            if (evadeLengthTimer <= 0)
            {
                evadeLengthTimer = EVADELENGTHTIME;
                movement.canMove = true;
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                ResetPhysicsLayer();
            }
            else
            {
                evadeLengthTimer -= Time.deltaTime;
                gameObject.GetComponent<Rigidbody2D>().velocity = direction * dashSpeed;
            }
        }

        if(direction == Vector3.zero)
            state = State.IDLE;
    }

    private IEnumerator EvadeFunctionality(Vector3 point)
    {
        yield return new WaitForSeconds(.3f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        // health.isAlive = false;
        yield return new WaitForSeconds(.30f);
        transform.position = (Vector2)point;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        // health.isAlive = true;
        if(gameObject.layer != 13)
            gameObject.layer = 13;
        state = State.IDLE;
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
		if (lastAttacks.Contains("MagicMissile") && lastAttacks.Contains("ProjectileSpeed") 
            && burstTimer >= BURSTCOOLDOWN && comboResource >= COMBORESOURCEMAX / 3)
        {
            handlers.AbilityChecker(ATTACKSIM, true, true);
            burstTimer = 0;
            comboResource -= 1;
        }
        else if(lastAttacks.Contains("MagicMissile") && lastAttacks.Contains("Reflect") 
            && burstTimer >= BURSTCOOLDOWN && comboResource >= COMBORESOURCEMAX / 3)
        {
            handlers.AbilityChecker(ABSORBSIM, true, true);
            burstTimer = 0;
            comboResource -= 1;
        }
        else if(lastAttacks.Contains("Reflect") && lastAttacks.Contains("ProjectileSpeed") 
            && burstTimer >= BURSTCOOLDOWN && comboResource >= COMBORESOURCEMAX / 3)
        {
            handlers.AbilityChecker(ABSORB, true, true);
            burstTimer = 0;
            comboResource -= 1;
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
        if (Input.GetKey(KeyCode.Mouse0))
        {
            AttackArrayHandler("MagicMissile", ritualList);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            AttackArrayHandler("Reflect", ritualList);
        }
        else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.F))
        {
            AttackArrayHandler("ProjectileSplit", ritualList);
        }
    }

    private void EvadeAnimations()
    {
        switch(movement.playerDirection)
        {
            case 0:
                playerAnimator.SetTrigger("triggeredPlayerTeleportOutUp");
                break;
            case 1:
                playerAnimator.SetTrigger("triggeredPlayerTeleportOutRight");
                break;
            case 2:
                playerAnimator.SetTrigger("triggeredPlayerTeleportOutDown");
                break;
            case 3:
                playerAnimator.SetTrigger("triggeredPlayerTeleportOutLeft");
                break;
        }
    }

    private void ResourceRegenHandler()
    {
        if(comboResource <= COMBORESOURCEMAX)
        {
            comboResource += Time.deltaTime * comboResourceRegenRate;
            resourceBar.fillAmount = comboResource / 3;
        }
    }

    public void AddToResource(float add)
    {
        comboResource += add;
    }

    public void ChangeLeftMouse()
    {
        if(leftMouseAbility == 1)
            leftMouseAbility = 4;
        else
            leftMouseAbility = 1;
    }

    public void ChangeRightMouse()
    {
        if(rightMouseAbility == 2)
            rightMouseAbility = 5;
        else
            rightMouseAbility = 2;
    }

    public void ChangeKeyboardButton()
    {
        if(keyboardAbility == 3)
            keyboardAbility = 6;
        else
            keyboardAbility = 3;
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
            evadeLengthTimer += EVADELENGTHTIME;
        }
    }

	#endregion
}