using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHandler : MonoBehaviour {

    //Public Editor Variables
    [Header("Editor Variables - Don't Touch")]
    public GameObject magicMissile;
    public GameObject reflect;
    public GameObject absorb;
    public GameObject simulacrum;
    public GameObject golem;
    public GameObject healStun;
    public GameObject healStunCombo;
    public GameObject projectileSplit;
    public GameObject projectileSplitSim;
    public GameObject projectileSpeed;
    public GameObject simulacrumAbsorb;
    public GameObject rotator;
    public Vector3 cursorInWorldPos;
    public ParticleSystem waveSystem;
    private Color origColor;

    [Space(10)]
   
   [Header("Audio Variables")]
    public AudioSource abilityHandlerSource;
    public AudioClip magicMissileSound;
    public AudioClip attackSimSound;


    [Space(10)]

    //Attack Variables
    [Header("Projectile Speed - For Magic Missile")]
    public float atkSpeed = 135f;

    //Max Placement Distance
    [Header("Max Placement Distance - For Abilities that are Placed")]
    public float placementDistance = 10f;

    [Space(10)]

    [Header("Cooldowns and Timers")]
    //Ability timers -- For the Attacks scripts
    public float LONGATKCOOLDOWN = .2f;
    public float HEALSTUNCOOLDOWN = 6f;
    public float REFLECTCOOLDOWN = 3f;
    public float ABSORBEXPLODECOOLDOWN = 8f;
    public float ENERGYGOLEMCOOLDOWN = 2f;
    public float HEALSTUNCOMBOCOOLDOWN = 6f;
    public float PROJECTILESPEEDCOOLDOWN = 6f;
    public float ATKSIMCOOLDOWN = 2f;
    public float ABSORBCOOLDOWN = 6f;
    public float ABSORBSIMCOOLDOWN = 12f;
    public float BURSTCOOLDOWN = 2f;

    public float ABSORBEND = 3f;
    public float ABSORBEXPLODEEND = 5f;

    public float projectileSpeedTimer;

    private float longATKTimer, energyGolemTimer, healStunTimer, healStunComboTimer,
    reflectTimer, absorbExplodeTimer, atkSimTimer, absorbTimer, absorbSimTimer, burstTimer;

    public float reflectHealth = 100f;
    public float REFLECTHEALTHMAX = 200f;
    public float REFLECTRECHARGEDELAY = 1f;
    private float reflectRechargeTimer;

    private PlayerAbilities abilities;
    private PlayerHealth health;
    private Animator playerAnimator;

    // Use this for initialization
    void Start () {
        longATKTimer = LONGATKCOOLDOWN;
        healStunTimer = HEALSTUNCOOLDOWN;
        healStunComboTimer = HEALSTUNCOMBOCOOLDOWN;
        reflectTimer = REFLECTCOOLDOWN;
        atkSimTimer = ATKSIMCOOLDOWN;
        absorbTimer = ABSORBCOOLDOWN;
        absorbSimTimer = ABSORBSIMCOOLDOWN;
        burstTimer = BURSTCOOLDOWN;
        energyGolemTimer = ENERGYGOLEMCOOLDOWN;
        projectileSpeedTimer = PROJECTILESPEEDCOOLDOWN;
        absorbExplodeTimer = ABSORBEXPLODECOOLDOWN;
        reflectRechargeTimer = 0;

        rotator = GameObject.Find("PlayerRotator");
        playerAnimator = GetComponent<Animator>();
        abilities = GetComponent<PlayerAbilities>();
        health = GetComponent<PlayerHealth>();

        StartCoroutine(LightUpdate());
    }

    private IEnumerator LightUpdate()
    {
        while(true)
        {
        	// print(waveSystem.isPlaying);
            BasicHandlers();
            ReflectRecharge();
            yield return null;
        }
    }

    public void AbilityChecker(int spell, bool isCombo, bool isBurst)
    {
        switch (isCombo)
        {
            case false:
                switch (spell)
                {
                    case 1:
                        MagicMissile();
                        break;
                    case 2:
                        Reflect();
                        break;
                    case 3:
                    	ProjectileSpeed();
                        break;
                    case 4:
                        EnergyGolem();
                        break;
                    case 5:
                    	AbsorbExplode();
                        break;
                    case 6:
                        HealStun();
                        break;
                }
                break;
            case true:
                switch (spell)
                {
                    case 1:
                        AttackSim(isBurst);
                        break;
                    case 2:
                        AbsorbSim(isBurst);
                        break;
                    case 3:
                        ProjectileSplitSim(isBurst);
                        break;
                    case 4:
                        HealStunCombo(isBurst);
                        break;
                    case 5:
                        Absorb(isBurst);
                        break;
                }
                break;
        }
    }

    #region BaseSpells

    private void MagicMissile()
    {
        if(longATKTimer >= LONGATKCOOLDOWN)
        {
            //TODO Add MagicMissile Sound

            float tempX = 0; //Random.Range(-.15f, .15f);

            AttackAnimations();

            Vector3 direction = cursorInWorldPos - new Vector3(transform.position.x, transform.position.y, 0);
            direction.Normalize();
            GameObject fb = Instantiate(magicMissile, transform.position, rotator.transform.rotation);
            fb.GetComponent<Rigidbody2D>().velocity = (direction + new Vector3(tempX, 0, 0)) * atkSpeed;
            abilities.AttackArrayHandler("MagicMissile", abilities.lastAttacks);
            longATKTimer = 0;

            abilityHandlerSource.clip = magicMissileSound;
            abilityHandlerSource.PlayOneShot(magicMissileSound);
        }
    }

    //Energy Golem/Simulacrum
    private void EnergyGolem()
    {
        if(energyGolemTimer >= ENERGYGOLEMCOOLDOWN)
        {
            GameObject sim = Instantiate(golem, transform.position + (transform.up * 8), transform.rotation);
            abilities.AttackArrayHandler("Golem", abilities.lastAttacks);
            energyGolemTimer = 0f;
        }
    }

    private void Reflect()
    {
        if (reflectTimer >= REFLECTCOOLDOWN)
        {
            if(!reflect.activeSelf)
            {
                reflect.SetActive(true);

            }
                
            abilities.AttackArrayHandler("Reflect", abilities.lastAttacks);
            reflectRechargeTimer = 0;
            // reflectTimer = 0;
        }
    }

    private void AbsorbExplode()
    {
    	if(absorbExplodeTimer >= ABSORBEXPLODECOOLDOWN)
    	{
    		origColor = gameObject.GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(origColor.r, origColor.g, origColor.b, .5f);
    		health.absorbDamage = true;
    		absorbExplodeTimer = 0;
    	}
    }

    private void HealStun()
    {
        //TODO Add HealStun Sound

        if(healStunTimer >= HEALSTUNCOOLDOWN)
        {
            Instantiate(healStun, cursorInWorldPos, transform.rotation);
            abilities.AttackArrayHandler("HealStun", abilities.lastAttacks);
            healStunTimer = 0;
        }
    }

    private void ProjectileSpeed()
    {
    	if(projectileSpeedTimer >= PROJECTILESPEEDCOOLDOWN)
    	{
    		Instantiate(projectileSpeed, PlacementCheck(), rotator.transform.rotation);
	        abilities.AttackArrayHandler("ProjectileSpeed", abilities.lastAttacks);
	        projectileSpeedTimer = 0;
    	}
    }

    #endregion

    #region ComboSpells

    //NewName - AtkSim
    private void AttackSim(bool isBurst)
    {
        if (isBurst)
        {
            //TODO Add AttackSim Burst Sound
            abilityHandlerSource.clip = attackSimSound;
            abilityHandlerSource.PlayOneShot(attackSimSound);

            GameObject[] sims = new GameObject[4];
            sims[0] = Instantiate(simulacrum, transform.position + (rotator.transform.up * -10) + (rotator.transform.right * -5), Quaternion.identity);
            sims[1] = Instantiate(simulacrum, transform.position + (rotator.transform.up * -10) + (rotator.transform.right * 5), Quaternion.identity);
            sims[2] = Instantiate(simulacrum, transform.position + (rotator.transform.up * -5) + (rotator.transform.right * -10), Quaternion.identity);
            sims[3] = Instantiate(simulacrum, transform.position + (rotator.transform.up * -5) + (rotator.transform.right * 10), Quaternion.identity);

            foreach(GameObject sim in sims)
            {
                sim.GetComponent<SimulacrumAbilities>().type = "Attack";
                sim.GetComponent<SimulacrumAbilities>().SetLifetime(4f);
            }
        }
        else
        {
            //TODO Add AttackSim Ritual Sound

            if(atkSimTimer >= ATKSIMCOOLDOWN)
            {
                abilityHandlerSource.clip = attackSimSound;
                abilityHandlerSource.PlayOneShot(attackSimSound);
                GameObject sim = Instantiate(simulacrum, transform.position + (transform.up * -8), Quaternion.identity);
                sim.GetComponent<SimulacrumAbilities>().type = "Attack";
                atkSimTimer = 0f;
            }
        }
    }

    private void ProjectileSplitSim(bool isBurst)
    {
        if(isBurst)
        {
            // print("Hit");
        	Instantiate(projectileSplitSim, PlacementCheck(), rotator.transform.rotation);
        }
    }

    //NewName - AbsorbSim
    private void AbsorbSim(bool isBurst)
    {
        if(isBurst)
        {
            //TODO Add AbsorbSim Burst Sound

            simulacrumAbsorb.SetActive(true);
            simulacrumAbsorb.GetComponent<SimulacrumAbsorb>().damageCap = 80f;
        }
        else
        {
            //TODO Add AbsorbSim Ritual Sound
            if(absorbSimTimer >= ABSORBSIMCOOLDOWN)
            {
                GameObject sim = Instantiate(simulacrum, transform.position + (transform.up * 8), Quaternion.identity);
                sim.GetComponent<SimulacrumAbilities>().type = "Absorb";
                sim.GetComponent<SimulacrumAbilities>().damageCap = 50;
                absorbSimTimer = 0f;
            }
        }
    }
 
    //HealStun Combo 
    private void HealStunCombo(bool isBurst)
    {
        if(isBurst)
        {
            GameObject gm = Instantiate(healStunCombo, cursorInWorldPos, transform.rotation);
            gm.GetComponent<HealStunComboHandler>().isBurst = true;
            burstTimer = 0;
        }
        else
        {
            if(healStunTimer >= HEALSTUNCOOLDOWN)
            {
                Instantiate(healStunCombo, cursorInWorldPos, transform.rotation);
                healStunComboTimer = 0;
            }
        }
    }

    //NewName - Absorb
    private void Absorb(bool isBurst)
    {
        if(isBurst)
        {
            //TODO Add Absorb Burst Sound

            absorb.SetActive(true);
            burstTimer = 0f;  
        }
        else
        {
            //TODO Add Absorb Ritual Sound
            if(absorbTimer >= ABSORBCOOLDOWN)
            {
                absorb.SetActive(true);
                absorbTimer = 0f;  
            }
        }
    }

    #endregion

    #region Handlers

    private void BasicHandlers()
    {
        TimerHandler();

        if (absorb.activeSelf && reflect.activeSelf)
        {
            reflect.GetComponent<ReflectLaser>().isLasered = false;
            reflect.SetActive(false);

        }

        cursorInWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    //X = transform.position.x + (placementDistance * Mathf.Cos(angle));
    //Y = transform.position.y + (placementDistance * Mathf.Cos(angle));
    //Handles the placement of the zone abilities
    private Vector3 PlacementCheck()
    {
        if(Vector3.Distance(transform.position, cursorInWorldPos) >= placementDistance)
        {
            float angle = Mathf.Atan2(cursorInWorldPos.y - transform.position.y, cursorInWorldPos.x - transform.position.x);

            float X = transform.position.x + (placementDistance * Mathf.Cos(angle));
            float Y = transform.position.y + (placementDistance * Mathf.Sin(angle));

            return new Vector2(X, Y);
        }
        else
        {
            return (Vector2)cursorInWorldPos;
        }

    }

    private void AttackAnimations()
    {
        switch(CursorDirection())
        {
            case 0:
                playerAnimator.SetTrigger("triggeredPlayerAttackUp");
                break;
            case 1:
                playerAnimator.SetTrigger("triggeredPlayerAttackRight");
                break;
            case 2:
                playerAnimator.SetTrigger("triggeredPlayerAttackDown");
                break;
            case 3:
                playerAnimator.SetTrigger("triggeredPlayerAttackLeft");
                break;
        }
    }

    public int CursorDirection()
    {
        int posX = -1;
        int posY = -1;
        float distX = cursorInWorldPos.x - transform.position.x;
        float distY = cursorInWorldPos.y - transform.position.y;

        if(distX > 0)
        {
            posX = 1;
        }
        if(distY > 0)
        {
            posY = 1;
        }

        if(Mathf.Abs(distX) > Mathf.Abs(distY))
        {
            if(posX > 0)
            {
                return 1;
            }
            else
            {
                return 3;
            }
        }
        else
        {
            if(posY > 0)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }
    }

    private void TimerHandler()
    {
        longATKTimer += Time.deltaTime;
        healStunTimer += Time.deltaTime;
        healStunComboTimer += HEALSTUNCOMBOCOOLDOWN;
        reflectTimer += Time.deltaTime;
        atkSimTimer += Time.deltaTime;
        absorbTimer += Time.deltaTime;
        absorbSimTimer += Time.deltaTime;
        burstTimer += Time.deltaTime;
        energyGolemTimer += Time.deltaTime;
        projectileSpeedTimer += Time.deltaTime;
        absorbExplodeTimer += Time.deltaTime;
        if(!reflect.activeSelf)
            reflectRechargeTimer += Time.deltaTime;

        if (reflectTimer >= 2f)
        {
            reflect.GetComponent<ReflectLaser>().isLasered = false;
            reflect.SetActive(false);

        }

        if(absorbTimer >= ABSORBEND)
        {

            absorb.SetActive(false);
        }

        //Maybe instead of when the time ends make it when the ability is next triggered that the player explodes?
        //Allows for better playmaking
        if(absorbExplodeTimer >= ABSORBEXPLODEEND && health.absorbDamage)
        {
        	GetComponent<SpriteRenderer>().color = origColor;
        	health.absorbDamage = false;
        	Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, 48f);

            foreach(Collider2D collider in col)
            {
            	if(collider.gameObject.tag == "Boss")
            	{
            		collider.GetComponent<BossHealth>().DealDamage(health.damageAbsorbed);
            	}
            }

            waveSystem.Play();
        }
    }

    public float GetTimer(string str)
    {
        switch(str)
        {
            case "attack":
                return longATKTimer;
            case "healstun":
                return healStunTimer;
            case "healstuncombo":
                return healStunComboTimer;
            case "reflect":
                return reflectTimer;
            case "atksim":
                return atkSimTimer;
            case "absorb":
                return absorbTimer;
            case "projectilesplit":
            	return projectileSpeedTimer;
        	case "absorbexplode":
        		return absorbExplodeTimer;
            default:
                return absorbSimTimer;
        }
    }

    public float GetCooldown(string str)
    {
        switch(str)
        {
            case "attack":
                return LONGATKCOOLDOWN;
            case "healstun":
                return HEALSTUNCOOLDOWN;
            case "healstuncombo":
                return HEALSTUNCOMBOCOOLDOWN;
            case "reflect":
                return REFLECTCOOLDOWN;
            case "atksim":
                return ATKSIMCOOLDOWN;
            case "absorb":
                return ABSORBCOOLDOWN;
        	case "projectilesplit":
            	return PROJECTILESPEEDCOOLDOWN;
        	case "absorbexplode":
        		return ABSORBEXPLODECOOLDOWN;
            default:
                return ABSORBSIMCOOLDOWN;
        }
    }

    private void ReflectRecharge()
    {
        if(reflectRechargeTimer >= REFLECTRECHARGEDELAY && reflectHealth < REFLECTHEALTHMAX && !reflect.activeSelf)
        {
            // print("Recharge " + reflectHealth);
            reflectHealth += 75 * Time.deltaTime;
        }
    }

    public void CancelReflect()
    {
        reflect.SetActive(false);

        reflectRechargeTimer = 0;
    }

    public void ReflectBroken()
    {
        reflect.SetActive(false);

        reflectTimer = 0;
        reflectRechargeTimer = 0;
    }

    #endregion

}
