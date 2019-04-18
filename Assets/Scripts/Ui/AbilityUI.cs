using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    public Image imageReflect;
    public Image imageSpeedField;

    private AbilityHandler playerAbilities;

    private void Start()
    {
        playerAbilities = GameObject.Find("Player").GetComponent<AbilityHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        imageReflect.fillAmount = 1 - playerAbilities.reflectHealth / playerAbilities.REFLECTHEALTHMAX;

        imageSpeedField.fillAmount = 1 - playerAbilities.projectileSpeedTimer / playerAbilities.PROJECTILESPEEDCOOLDOWN;
    }
}
