using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHandler : MonoBehaviour {

    //Public Editor Variables
    public GameObject magicMissile;
    public GameObject reflect;
    public GameObject absorb;
    public GameObject simulacrum;
    public GameObject healStun;
    public GameObject healStunCombo;
    public Vector2 cursorInWorldPos;
   
    public AudioSource abilityHandlerSource;
    public AudioSource reflectAudio;

    public AudioClip magicMissileSound;
    public AudioClip attackSimSound;
    public AudioClip reflectLoopSound;

    //Attack Variables
    private float atkSpeed = 135f;

    //Ability timers -- For the Attacks scripts
    private const float LONGATKCOOLDOWN = .2f;
    private const float HEALSTUNCOOLDOWN = 6f;
    private const float HEALSTUNCOMBOCOOLDOWN = 6f;
    private const float REFLECTCOOLDOWN = 3f;
    private const float ATKSIMCOOLDOWN = 2f;
    private const float ABSORBCOOLDOWN = 6f;
    private const float ABSORBSIMCOOLDOWN = 12f;
    private const float BURSTCOOLDOWN = 2f;

    private const float ABSORBEND = 3f;

    private float longATKTimer, healStunTimer, healStunComboTimer, reflectTimer, 
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

    #region SpellFunctions

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

    private void Reflect()
    {
         reflectAudio = GetComponent<AudioSource>();
         reflectAudio.clip = reflectLoopSound;
         reflectAudio.Play();
        if (reflectTimer >= REFLECTCOOLDOWN)
        {
            reflect.SetActive(true);
            reflectTimer = 0;
            reflectAudio.Stop();
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

    //NewName - AtkSim
    private void AttackSim(bool isBurst)
    {
        abilityHandlerSource.clip = attackSimSound;
        abilityHandlerSource.PlayOneShot(attackSimSound);

        if (isBurst)
        {
            //TODO Add AttackSim Burst Sound

            GameObject sim = Instantiate(simulacrum, transform.position, Quaternion.identity);
            sim.GetComponent<SimulacrumAbilities>().type = "Attack";
            sim.GetComponent<SimulacrumAbilities>().SetLifetime(4f);
        }
        else
        {
            //TODO Add AttackSim Ritual Sound

            if(atkSimTimer >= ATKSIMCOOLDOWN)
            {
                GameObject sim = Instantiate(simulacrum, transform.position, Quaternion.identity);
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

            GameObject sim = Instantiate(simulacrum, transform.position, Quaternion.identity);
            sim.GetComponent<SimulacrumAbilities>().type = "Absorb";
        }
        else
        {
            //TODO Add AbsorbSim Ritual Sound
            if(absorbSimTimer >= ABSORBSIMCOOLDOWN)
            {
                GameObject sim = Instantiate(simulacrum, transform.position, Quaternion.identity);
                sim.GetComponent<SimulacrumAbilities>().type = "Absorb";
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
        if (str == "attack")
            return longATKTimer;
        else if (str == "healstun")
            return healStunTimer;
        else if (str == "healstuncombo")
            return healStunComboTimer;
        else if (str == "reflect")
            return reflectTimer;
        else if (str == "atkdash")
            return atkSimTimer;
        else if (str == "absorb")
            return absorbTimer;
        else
            return absorbSimTimer;
    }

    public float GetCooldown(string str)
    {
        if (str == "attack")
            return LONGATKCOOLDOWN;
        else if (str == "healstun")
            return HEALSTUNCOOLDOWN;
        else if (str == "healstuncombo")
            return HEALSTUNCOMBOCOOLDOWN;
        else if (str == "reflect")
            return REFLECTCOOLDOWN;
        else if (str == "atkdash")
            return ATKSIMCOOLDOWN;
        else if (str == "absorb")
            return ABSORBCOOLDOWN;
        else
            return ABSORBSIMCOOLDOWN;
    }

    #endregion

}
