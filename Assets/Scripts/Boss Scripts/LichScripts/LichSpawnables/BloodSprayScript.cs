using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSprayScript : MonoBehaviour
{
    private ObjectPoolerScript objectPooler;
    //public GameObject bloodDrop;
    public float sprayRate = .1f;
    public float spraySpread = 3f;
    public float sprayRotationRate = .01f;
    public float sprayRotationAmount = 1f;
    public float bloodSprayDuration = 2f;
    public bool canSprayBlood = false;


    // Use this for initialization
    private void Start()
    {
        objectPooler = ObjectPoolerScript.Instance;
       
    }
    private void OnEnable()
    {
        canSprayBlood = true;
        InvokeRepeating("SprayBlood", 0, sprayRate);
        InvokeRepeating("RotateSprayBlood", 0, sprayRotationRate);
        Invoke("TurnOffSpray", bloodSprayDuration);
    }

    public void SprayBlood()
    {
        if (canSprayBlood)
        {
            GameObject bloodOne = objectPooler.SpawnFromPool("BloodDrop", transform.position, transform.rotation);
            bloodOne.transform.Rotate(0, 0, Random.Range(-spraySpread, spraySpread));

            GameObject bloodTwo = objectPooler.SpawnFromPool("BloodDrop", transform.position, transform.rotation);
            bloodTwo.transform.Rotate(0, 0, 180);
            bloodTwo.transform.Rotate(0, 0, Random.Range(-spraySpread, spraySpread));

            GameObject bloodThree = objectPooler.SpawnFromPool("BloodDrop", transform.position, transform.rotation);
            bloodThree.transform.Rotate(0, 0, 90);
            bloodThree.transform.Rotate(0, 0, Random.Range(-spraySpread, spraySpread));

            GameObject bloodFour = objectPooler.SpawnFromPool("BloodDrop", transform.position, transform.rotation);
            bloodFour.transform.Rotate(0, 0, -90);
            bloodFour.transform.Rotate(0, 0, Random.Range(-spraySpread, spraySpread));
        }
    }

    public void RotateSprayBlood()
    {
        gameObject.transform.Rotate(0, 0, sprayRotationAmount);
    }

    public void TurnOffSpray()
    {
        canSprayBlood = false;
        gameObject.SetActive(false);
    }
}
