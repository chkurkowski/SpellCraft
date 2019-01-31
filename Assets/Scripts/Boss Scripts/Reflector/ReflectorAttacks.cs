using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorAttacks : MonoBehaviour
{

    private Animator reflectorAnimatorInfo;
    // Use this for initialization
    void Start()
    {
        reflectorAnimatorInfo = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void Attack(int attackNumber)
    {

        switch (attackNumber)
        {
            case 0:

                Debug.Log("An incorrect attackNumber was passed as 0");
                break;

            case 1:


                break;

            case 2:


                break;

            case 3:


                break;

        }
}
