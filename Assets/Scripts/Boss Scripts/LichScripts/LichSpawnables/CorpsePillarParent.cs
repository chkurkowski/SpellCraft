using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpsePillarParent : MonoBehaviour
{
    private BossHealth lichHealth;
    private bool canSpin = true;
    public bool isSpinning = false;
    public bool isEnraged = false;
    [Space(25)]
    public float pillarSpinRate = .01f;
    public float pillarRotateAmount = 1f;

    [Space(25)]

    public float healPerPillar = 5f;
    public float healTime = 15f;

   

    [Space(45)]
    public GameObject pillarOne;
    public GameObject pillarTwo;
    public GameObject pillarThree;

    private void OnEnable()
    {
        ActivatePillars();
        InvokeRepeating("PillarHeal", 0, healTime);
    }
    // Use this for initialization
    void Start ()
    {
        lichHealth = GameObject.Find("Lich").GetComponent<BossHealth>();
        DeactivatePillars();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
        
            if(isSpinning && canSpin)
            {
                canSpin = false;
                InvokeRepeating("SpinPillars", 0, pillarSpinRate);
               // 
            }

        if(!pillarOne.activeSelf && !pillarTwo.activeSelf && !pillarThree.activeSelf)
        {
            gameObject.SetActive(false);
        }
        
	}

    public void SpinPillars()
    {
        transform.Rotate(0, 0, pillarRotateAmount);
    }

    public void PillarHeal()
    {
        if(pillarOne.activeSelf)
        {
            lichHealth.HealBoss(healPerPillar);
        }
        if(pillarTwo.activeSelf)
        {
            lichHealth.HealBoss(healPerPillar);
        }
        if(pillarThree.activeSelf)
        {
            lichHealth.HealBoss(healPerPillar);
        }
    }

    public void ActivatePillars()
    {
        pillarOne.SetActive(true);
        pillarTwo.SetActive(true);
        pillarThree.SetActive(true);
       if(isEnraged)
        {
         //   Debug.Log("Parent enraged function happened");
            pillarOne.GetComponent<CorpsePillar>().SetEnraged(true);
            pillarTwo.GetComponent<CorpsePillar>().SetEnraged(true);
            pillarThree.GetComponent<CorpsePillar>().SetEnraged(true);

        }
    }

    public void DeactivatePillars()
    {
        pillarOne.SetActive(false);
        pillarTwo.SetActive(false);
        pillarThree.SetActive(false);

      //  pillarOne.GetComponent<CorpsePillar>().SetEnraged(false);
     //   pillarTwo.GetComponent<CorpsePillar>().SetEnraged(false);
       // pillarThree.GetComponent<CorpsePillar>().SetEnraged(false);

    }
}
