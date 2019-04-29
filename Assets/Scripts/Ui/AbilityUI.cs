using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    public Image imageReflect;
    public Image imageSpeedField;

    //0 = Magic Missile
    //1 = Reflect
    //2 = Speed Field
    public Sprite[] abilityIcons;

    public Image imageOne;
    public Image imageTwo;

    public Text nextBurstText;

    private AbilityHandler abilityHandler;

    private PlayerAbilities playerAbilities;

    private void Start()
    {
        abilityHandler = GameObject.Find("Player").GetComponent<AbilityHandler>();
        playerAbilities = GameObject.Find("Player").GetComponent<PlayerAbilities>();
    }

    // Update is called once per frame
    void Update()
    {
        imageReflect.fillAmount = 1 - abilityHandler.reflectHealth / abilityHandler.REFLECTHEALTHMAX;

        imageSpeedField.fillAmount = 1 - abilityHandler.projectileSpeedTimer / abilityHandler.PROJECTILESPEEDCOOLDOWN;

        UpdateAbilityIcons();
    }

    private void UpdateAbilityIcons()
    {
    	string[] attacks = playerAbilities.CurrentBurst();

    	if(attacks.Length > 0 && attacks[0] != "")
    	{
    		print(attacks[0]);
    		switch(attacks[0])
	    	{
	    		case "MagicMissile":
	    			imageOne.sprite = abilityIcons[0];
	    			break;
				case "Reflect":
					imageOne.sprite = abilityIcons[1];
					break;
				case "ProjectileSpeed":
					imageOne.sprite = abilityIcons[2];
					break;
	    	}
	    }
	    else if(attacks[0] == "")
	    {
	    	//Show blank
	    }

	    if(attacks.Length > 1)
	    {
	    	print(attacks[1]);
	    	switch(attacks[1])
	    	{
	    		case "MagicMissile":
	    			imageTwo.sprite = abilityIcons[0];
	    			break;
				case "Reflect":
					imageTwo.sprite = abilityIcons[1];
					break;
				case "ProjectileSpeed":
					imageTwo.sprite = abilityIcons[2];
					break;
	    	}

	    	nextBurstText.text = attacks[2];
    	}
    }
}
