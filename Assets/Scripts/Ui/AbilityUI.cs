using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour {
	public Image imageReflect;
	public Image imageSpeedField;
	
	bool isCooldown;
	bool isPCooldown;

	public float PROJECTILESPEEDCOOLDOWN = 6f;
	public float REFLECTCOOLDOWN = 3f;



	void Start() {
		
	}



	
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(1))
	{
		isCooldown = true;

	}
	if(Input.GetKeyDown("e"))
	{
		isPCooldown = true;

	}
	if(isCooldown)
	{
		imageReflect.fillAmount += 1 / REFLECTCOOLDOWN * Time.deltaTime;
		if(imageReflect.fillAmount >= 1)
		{
			imageReflect.fillAmount = 0;
			isCooldown = false;
		}
	}
	if(isPCooldown)
	{
		imageSpeedField.fillAmount += 1 / PROJECTILESPEEDCOOLDOWN * Time.deltaTime;

		if(imageSpeedField.fillAmount >= 1)
		{
			imageSpeedField.fillAmount = 0;
			isPCooldown = false;
		}
	}
		
	}
}
