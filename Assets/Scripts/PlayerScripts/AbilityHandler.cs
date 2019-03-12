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
    public Vector2 cursorInWorldPos;
    public ParticleSystem waveSystem;
    private Color origColor;

    [Space(10)]
   
   [Header("Audio Variables")]
    public AudioSource abilityHandlerSource;
    public AudioSource reflectAudio;

    public AudioClip magicMissileSound;
    public AudioClip attackSimSound;
    public AudioClip reflectLoopSound;

    [Space(10)]

    //Attack Variables
    [Header("Projectile Speed - For First Projectile")]
    public float atkSpeed = 135f;

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

    private float longATKTimer, energyGolemTimer, healStunTimer, projectileSpeedTimer, healStunComboTimer,
    reflectTimer, absorbExplodeTimer, atkSimTimer, absorbTimer, absorbSimTimer, burstTimer;

    private PlayerAbilities abilities;
    private PlayerHealth health;

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

        abilities = GetComponent<PlayerAbilities>();
        health = GetComponent<PlayerHealth>();

        StartCoroutine(LightUpdate());
    }

    private IEnumerator LightUpdate()
    {
        while(true)
        {
        	print(waveSystem.isPlaying);
            BasicHandlers();
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

            Vector2 direction = cursorInWorldPos - new Vector2(transform.position.x, transform.position.y);
            direction.Normalize();
            GameObject fb = Instantiate(magicMissile, transform.position, Quaternion.identity);
            fb.GetComponent<Rigidbody2D>().velocity = (direction + new Vector2(tempX, 0)) * atkSpeed;
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
            reflectAudio = GetComponent<AudioSource>();
            reflectAudio.clip = reflectLoopSound;
            reflectAudio.Play();
            
            reflect.SetActive(true);
            abilities.AttackArrayHandler("Reflect", abilities.lastAttacks);
            reflectTimer = 0;
           // reflectAudio.Stop();
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
    		Instantiate(projectileSpeed, cursorInWorldPos,transform.rotation);
	        abilities.AttackArrayHandler("HealStun", abilities.lastAttacks);
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
            sims[0] = Instantiate(simulacrum, transform.position + (transform.up * -10) + (transform.right * -5), Quaternion.identity);
            sims[1] = Instantiate(simulacrum, transform.position + (transform.up * -10) + (transform.right * 5), Quaternion.identity);
            sims[2] = Instantiate(simulacrum, transform.position + (transform.up * -5) + (transform.right * -10), Quaternion.identity);
            sims[3] = Instantiate(simulacrum, transform.position + (transform.up * -5) + (transform.right * 10), Quaternion.identity);

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
            print("Hit");
        	Instantiate(projectileSplitSim, cursorInWorldPos, transform.rotation);
        }
    }

    //NewName - AbsorbSim
    private void AbsorbSim(bool isBurst)
    {
        if(isBurst)
        {
            //TODO Add AbsorbSim Burst Sound

            simulacrumAbsorb.SetActive(true);
            simulacrumAbsorb.GetComponent<SimulacrumAbsorb>().damageCap = 40f;
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

    #endregion

}
