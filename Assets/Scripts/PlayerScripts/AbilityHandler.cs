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
    public Vector2 cursorInWorldPos;

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
    public float ENERGYGOLEMCOOLDOWN = 2f;
    public float HEALSTUNCOMBOCOOLDOWN = 6f;
    public float ATKSIMCOOLDOWN = 2f;
    public float ABSORBCOOLDOWN = 6f;
    public float ABSORBSIMCOOLDOWN = 12f;
    public float BURSTCOOLDOWN = 2f;

    public float ABSORBEND = 3f;

    private float longATKTimer, energyGolemTimer, healStunTimer, healStunComboTimer, reflectTimer, 
    atkSimTimer, absorbTimer, absorbSimTimer, burstTimer;

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

        StartCoroutine(LightUpdate());
    }

    private IEnumerator LightUpdate()
    {
        while(true)
        {
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
                        HealStun();
                        break;
                    case 4:
                        EnergyGolem();
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
                        Absorb(isBurst);
                        break;
                    case 3:
                        AbsorbSim(isBurst);
                        break;
                    case 4:
                        HealStunCombo(isBurst);
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
            reflectTimer = 0;
           // reflectAudio.Stop();
        }
    }

    private void HealStun()
    {
        //TODO Add HealStun Sound

        if(healStunTimer >= HEALSTUNCOOLDOWN)
        {
            Instantiate(healStun, cursorInWorldPos, transform.rotation);
            healStunTimer = 0;
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

            GameObject sim = Instantiate(simulacrum, transform.position + (transform.up * -8), Quaternion.identity);
            sim.GetComponent<SimulacrumAbilities>().type = "Attack";
            sim.GetComponent<SimulacrumAbilities>().SetLifetime(4f);
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

    //NewName - AbsorbSim
    private void AbsorbSim(bool isBurst)
    {
        if(isBurst)
        {
            //TODO Add AbsorbSim Burst Sound

            GameObject sim = Instantiate(simulacrum, transform.position + (transform.up * 8), Quaternion.identity);
            sim.GetComponent<SimulacrumAbilities>().type = "Absorb";
            sim.GetComponent<SimulacrumAbilities>().damageCap = 20;
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

        if (reflectTimer >= 2f)
        {
            reflect.GetComponent<ReflectLaser>().isLasered = false;
            reflect.SetActive(false);
        }
        if(absorbTimer >= ABSORBEND)
        {
            absorb.SetActive(false);
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
            default:
                return ABSORBSIMCOOLDOWN;
        }
    }

    #endregion

}
