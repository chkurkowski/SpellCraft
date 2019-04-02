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

    private void Update()
    {
        switch (pylonNumber)
        {
            case 1:
               if(protoNovusAttacksInfo.pillarOneIsUp)
                {
                    TurnCoverLaserOn();
                }
                else
                {
                    TurnCoverLaserOff();
                }
                break;

            case 2:
                if (protoNovusAttacksInfo.pillarTwoIsUp)
                {
                    TurnCoverLaserOn();
                }
                else
                {
                    TurnCoverLaserOff();
                }
                break;

            case 3:
                if (protoNovusAttacksInfo.pillarThreeIsUp)
                {
                    TurnCoverLaserOn();
                }
                else
                {
                    TurnCoverLaserOff();
                }
                break;

            case 4:
                if (protoNovusAttacksInfo.pillarFourIsUp)
                {
                    TurnCoverLaserOn();
                }
                else
                {
                    TurnCoverLaserOff();
                }
                break;

        }
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        Debug.Log(gameObject.name + " detected a collision from: " + trig.gameObject.name);
        if(trig.gameObject.tag == "Projectile")// || trig.gameObject.tag == "LaserEndPoint" || trig.gameObject.tag == "EnemyProjectile")
        {
            TurnCoverLaserOff();
            switch (pylonNumber)
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

    public void TurnCoverLaserOn()
    {
        gameObject.GetComponent<LineRenderer>().enabled = true;
    }

    public void TurnCoverLaserOff()
    {
        gameObject.GetComponent<LineRenderer>().enabled = false;
    }
}
