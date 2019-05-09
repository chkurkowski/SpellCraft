using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAnim : MonoBehaviour
{
    private GameObject parentTeleport;
    private TeleporterScript teleScript;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SetTeleReference(GameObject teleReference)
    {
        parentTeleport = teleReference;
        teleScript = parentTeleport.GetComponent<TeleporterScript>();
    }

    public void TeleportPlayer()
    {
        teleScript.TeleportPlayer();

    }

    public void UnteleportPlayer()
    {
        teleScript.UnteleportPlayer();

    }

    public void DestroySelf()
    {

        Destroy(gameObject);
    }



}
