using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonArt : MonoBehaviour
{
    private ProtoNovusAttacks protoNovusBoss;
    private Animator bossAnimator;
    

    private void Start()
    {
        bossAnimator = gameObject.GetComponent<Animator>();
        protoNovusBoss = GameObject.Find("ProtoNovus").GetComponent<ProtoNovusAttacks>();
    }

    public void AttackOneStart()
    {
        protoNovusBoss.AttackOne();
    }


    public void AttackTwoStart()
    {
        protoNovusBoss.AttackTwo();
    }



    public void AttackThreeStart()
    {
        protoNovusBoss.AttackThree();
    }

  

}
