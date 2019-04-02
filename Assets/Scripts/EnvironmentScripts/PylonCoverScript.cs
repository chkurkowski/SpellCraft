using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonCoverScript : MonoBehaviour
{
    private ProtoNovusAttacks protoNovusAttacksInfo;

    public int pylonNumber;

	// Use this for initialization
	void Start ()
    {
        protoNovusAttacksInfo = GameObject.Find("ProtoNovus").GetComponent<ProtoNovusAttacks>();
	}

    private void OnTriggerEnter2D(Collider2D trig)
    {
        Debug.Log(gameObject.name + " detected a collision from: " + trig.gameObject.name);
        if(trig.gameObject.tag == "Projectile" || trig.gameObject.tag == "LaserEndPoint")
        {
            switch(pylonNumber)
            {
                case 1:
                    protoNovusAttacksInfo.ResetPillarOneActual();
                    break;

                case 2:
                    protoNovusAttacksInfo.ResetPillarTwoActual();
                    break;

                case 3:
                    protoNovusAttacksInfo.ResetPillarThreeActual();
                    break;

                case 4:
                    protoNovusAttacksInfo.ResetPillarFourActual();
                    break;
                
            }
        }
    }

}
