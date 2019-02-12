using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHandler : MonoBehaviour {

	//Public Editor Variables
	public GameObject magicMissile;
	public GameObject reflect;
	public GameObject absorb;
	public GameObject simulacrum;
	public Vector2 cursorInWorldPos;

	//Attack Variables
	public float atkSpeed = 75f;

	//Ability timers -- For the Attacks scripts
    private const float LONGATKCOOLDOWN = .35f;
    private const float EVADECOOLDOWN = .5f;
    private const float REFLECTCOOLDOWN = 3f;
    private const float ATKSIMCOOLDOWN = 2f;
    private const float ABSORBCOOLDOWN = 6f;
    private const float ABSORBSIMCOOLDOWN = 12f;
    private const float BURSTCOOLDOWN = 2f;

    private const float ABSORBEND = 3f;

    private float longATKTimer, evadeTimer, reflectTimer, 
    atkSimTimer, absorbTimer, absorbSimTimer, burstTimer;


	// Use this for initialization
	void Start () {
        longATKTimer = LONGATKCOOLDOWN;
        evadeTimer = EVADECOOLDOWN;
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
					case 7:
						ComboOne(isBurst);
						break;
					case 8:
						ComboTwo(isBurst);
						break;
					case 9:
						ComboThree(isBurst);
						break;
				}
				break;
		}
	}

	private void MagicMissile()
	{
		Vector2 direction = cursorInWorldPos - new Vector2(transform.position.x, transform.position.y);
        direction.Normalize();
        GameObject fb = Instantiate(magicMissile, transform.position, Quaternion.identity);
        fb.GetComponent<Rigidbody2D>().velocity = direction * atkSpeed;
        longATKTimer = 0;
	}

	private void Reflect()
	{
		reflect.SetActive(true);
		reflectTimer = 0;
	}

	private void HealStun()
	{

	}

	//NewName
	private void ComboOne(bool isBurst)
	{

	}

	//NewName
	private void ComboTwo(bool isBurst)
	{

	}

	//NewName
	private void ComboThree(bool isBurst)
	{

	}

	#endregion

	#region Handlers

    private void BasicHandlers()
    {
        TimerHandler();

        if (absorb.activeSelf && reflect.activeSelf)
            reflect.SetActive(false);

        cursorInWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void TimerHandler()
    {
        longATKTimer += Time.deltaTime;
        evadeTimer += Time.deltaTime;
        reflectTimer += Time.deltaTime;
        atkSimTimer += Time.deltaTime;
        absorbTimer += Time.deltaTime;
        absorbSimTimer += Time.deltaTime;
        burstTimer += Time.deltaTime;

        if (evadeTimer >= EVADECOOLDOWN)
            gameObject.GetComponent<Collider2D>().isTrigger = false;
        if (reflectTimer >= 2f)
            reflect.SetActive(false);
        if(absorbTimer >= ABSORBEND)
        {
            absorb.SetActive(false);
        }
    }

    public float GetTimer(string str)
    {
        if (str == "attack")
            return longATKTimer;
        else if (str == "evade")
            return evadeTimer;
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
        else if (str == "evade")
            return EVADECOOLDOWN;
        else if (str == "reflect")
            return REFLECTCOOLDOWN;
        else if (str == "atkdash")
            return ATKSIMCOOLDOWN;
        else if (str == "absorb")
            return ABSORBCOOLDOWN;
        else
            return ABSORBSIMCOOLDOWN;
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
