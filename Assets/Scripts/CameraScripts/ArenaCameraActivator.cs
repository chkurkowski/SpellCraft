using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaCameraActivator : MonoBehaviour
{
    private GameObject mainCamera;
    /// <summary>
    /// type the name of the boss that the arena belongs to
    /// </summary>
    public bool useDynamicCamera = true;
    [Space(40)]
    public string associatedBossName = "";
    //private Transform playerLocation;
    // public float detectionDistance = 150f;
 
    public GameObject prototypeBoss;
    public GameObject pylonBoss;
    public GameObject alchemistBoss;
    public GameObject lichBoss;
    public GameObject reflectorBoss;
    public GameObject charmerBoss;
    public GameObject protoNovusBoss;
    private BossInfo bossInfo;
    private BossHealth bossHealthInfo;


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
                    bossInfo = mainCamera.GetComponent<DynamicCamera>().Boss.GetComponent<BossInfo>();
                    bossHealthInfo = mainCamera.GetComponent<DynamicCamera>().Boss.GetComponent<BossHealth>();
                  
                    bossInfo.isActivated = true;
                    bossHealthInfo.HealthBarParent.SetActive(true);

                    break;

                case "Pylon":
                    mainCamera.GetComponent<DynamicCamera>().Boss = pylonBoss;
                    bossInfo = mainCamera.GetComponent<DynamicCamera>().Boss.GetComponent<BossInfo>();
                    bossHealthInfo = mainCamera.GetComponent<DynamicCamera>().Boss.GetComponent<BossHealth>();
                 
                    bossInfo.isActivated = true;
                    bossHealthInfo.HealthBarParent.SetActive(true);
                    break;

                case "Charmer":
                    mainCamera.GetComponent<DynamicCamera>().Boss = charmerBoss;
                    bossInfo = mainCamera.GetComponent<DynamicCamera>().Boss.GetComponent<BossInfo>();
                    bossInfo.isActivated = true;
                    break;

                case "Reflector":
                    mainCamera.GetComponent<DynamicCamera>().Boss = reflectorBoss;
                    bossInfo = mainCamera.GetComponent<DynamicCamera>().Boss.GetComponent<BossInfo>();
                    bossHealthInfo = mainCamera.GetComponent<DynamicCamera>().Boss.GetComponent<BossHealth>();
                    bossHealthInfo.HealthBarParent.SetActive(true);
                    bossInfo.isActivated = true;
                    break;

                case "Alchemist":
                    mainCamera.GetComponent<DynamicCamera>().Boss = alchemistBoss;
                    bossInfo = mainCamera.GetComponent<DynamicCamera>().Boss.GetComponent<BossInfo>();
                    bossHealthInfo = mainCamera.GetComponent<DynamicCamera>().Boss.GetComponent<BossHealth>();
                    bossHealthInfo.HealthBarParent.SetActive(true);
                    bossInfo.isActivated = true;
                    break;

                case "PrototypeBoss":
                    Debug.Log("prototypeBoss was found!");
                    mainCamera.GetComponent<DynamicCamera>().Boss = prototypeBoss;
                    bossInfo = mainCamera.GetComponent<DynamicCamera>().Boss.GetComponent<BossInfo>();
                    bossHealthInfo = mainCamera.GetComponent<DynamicCamera>().Boss.GetComponent<BossHealth>();
                    bossHealthInfo.HealthBarParent.SetActive(true);
                    bossInfo.isActivated = true;
                    break;


                case "ProtoNovus":
                    Debug.Log("ProtoNovus was found!");
                    mainCamera.GetComponent<DynamicCamera>().Boss = protoNovusBoss;
                    bossInfo = mainCamera.GetComponent<DynamicCamera>().Boss.GetComponent<BossInfo>();
                    bossHealthInfo = mainCamera.GetComponent<DynamicCamera>().Boss.GetComponent<BossHealth>();
                    bossHealthInfo.HealthBarParent.SetActive(true);
                    bossInfo.isActivated = true;
                    break;

            }
            if(useDynamicCamera)
            {
                GameObject.Find("Main Camera").GetComponent<CameraScriptActivator>().ActivateDynamicCamera();
            }
        }
    }
   
    private void OnTriggerExit2D(Collider2D trig)
   {
        if (trig.gameObject.tag == "Player")
       {
            if (useDynamicCamera)
            {
                GameObject.Find("Main Camera").GetComponent<CameraScriptActivator>().DisableDynamicCamera();
            }
            bossInfo.isActivated = false;
            bossInfo.ResetBoss();
        }
   }
}
