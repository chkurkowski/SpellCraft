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

    private void OnCollisionEnter2D(Collision2D collider)
    {
        Debug.Log(gameObject.name + " detected a collision from: " + collider.gameObject.name);
        if(collider.collider.tag == "Projectile" || collider.collider.tag == "LaserEndpoint")
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
