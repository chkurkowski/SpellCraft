using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaCameraActivator : MonoBehaviour
{
    private GameObject mainCamera;
    /// <summary>
    /// type the name of the boss that the arena belongs to
    /// </summary>
    public string associatedBossName = "";
    //private Transform playerLocation;
   // public float detectionDistance = 150f;
    public GameObject prototypeBoss;
    public GameObject pylonBoss;
    public GameObject alchemistBoss;
    public GameObject lichBoss;
    public GameObject reflectorBoss;
    public GameObject charmerBoss;


    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
      

       

    }

    

    private void OnTriggerEnter2D(Collider2D trig)
    {
       if(trig.gameObject.tag == "Player")
        { 
            switch (associatedBossName)
            {
                case "Lich":
                    mainCamera.GetComponent<DynamicCamera>().Boss = lichBoss;
                    break;

                case "Pylon":
                    mainCamera.GetComponent<DynamicCamera>().Boss = pylonBoss;
                    break;

                case "Charmer":
                    mainCamera.GetComponent<DynamicCamera>().Boss = charmerBoss;
                    break;

                case "Reflector":
                    mainCamera.GetComponent<DynamicCamera>().Boss = reflectorBoss;
                    break;

                case "Alchemist":
                    mainCamera.GetComponent<DynamicCamera>().Boss = alchemistBoss;
                    break;

                case "PrototypeBoss":
                    Debug.Log("prototypeBoss was found!");
                    mainCamera.GetComponent<DynamicCamera>().Boss = prototypeBoss;
                    break;

            }
        
            GameObject.Find("Main Camera").GetComponent<CameraScriptActivator>().ActivateDynamicCamera();
          
        }
    }
   
    private void OnTriggerExit2D(Collider2D trig)
   {
        if (trig.gameObject.tag == "Player")
       {
           GameObject.Find("Main Camera").GetComponent<CameraScriptActivator>().DisableDynamicCamera();
           // mainCamera.GetComponent<DynamicCamera>().Boss = null;
       }
   }
}
