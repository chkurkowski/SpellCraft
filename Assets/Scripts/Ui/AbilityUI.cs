using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    public Image imageReflect;
    public Image imageSpeedField;

    bool isCooldown;
    bool isPCooldown;

    private AbilityHandler playerAbilities;
    public float PROJECTILESPEEDCOOLDOWN;
    public float REFLECTCOOLDOWN;

    private void Start()
    {
        playerAbilities = GameObject.Find("Player").GetComponent<AbilityHandler>();
        PROJECTILESPEEDCOOLDOWN = playerAbilities.GetCooldown("projectilespeed");
        REFLECTCOOLDOWN = playerAbilities.GetCooldown("reflect");
    }



    // Update is called once per frame
    void Update()
    {
            imageReflect.fillAmount = playerAbilities.reflectHealth / playerAbilities.REFLECTHEALTHMAX;


        if (Input.GetKeyDown("e"))
        {
            if (!isPCooldown)
            {
                isPCooldown = true;
                imageSpeedField.fillAmount = 0f;
            }
        }
        if (isPCooldown)
        {
            imageSpeedField.fillAmount += 1f / PROJECTILESPEEDCOOLDOWN * Time.deltaTime;

            if (imageSpeedField.fillAmount >= 1f)
            {
                imageSpeedField.fillAmount = 1f;
                isPCooldown = false;
            }
        }

    }
}
