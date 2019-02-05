using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaCameraActivator : MonoBehaviour
{
    public GameObject mainCamera;
    /// <summary>
    /// type the name of the boss that the arena belongs to
    /// </summary>

    public string associatedBossName = "";

    public GameObject prototypeBoss;
    public GameObject pylonBoss;
    public GameObject alchemistBoss;
    public GameObject lichBoss;
    public GameObject reflectorBoss;
    public GameObject charmerBoss;


    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera");

        switch (BossName)
        {
            case "Lich":
                
                break;

            case "Pylon":
               
                break;

            case "Charmer":
               
                break;

            case "Reflector":
               
                break;

            case "Alchemist":
             
                break;

            case "PrototypeBoss":
              
                break;

        }
    }



    private void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.tag == "Player")
        {
            GameObject.Find("Main Camera").GetComponent<CameraScriptActivator>().ActivateDynamicCamera();
            //mainCamera.GetComponent<DynamicCamera>().Boss = 
        }
    }

    private void OnTriggerExit2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            GameObject.Find("Main Camera").GetComponent<CameraScriptActivator>().DisableDynamicCamera();
        }
    }
}
