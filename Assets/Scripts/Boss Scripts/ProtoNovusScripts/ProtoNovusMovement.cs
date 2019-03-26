using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoNovusMovement : MonoBehaviour {
    private BossInfo bossInfo;
    private BossAttacks bossAttacksInfo;
    public float rotationDirection = 1f;
    private float rotationSpeed = 0.01f;
    private float rotationAmount = 0.5f;
    private bool isSpinning = false;
    // Use this for initialization
    void Start()
    {
        bossInfo = gameObject.GetComponent<BossInfo>();
        bossAttacksInfo = gameObject.GetComponent<BossAttacks>();
    }

    public void LaserAttackMovement()
    {

        if (!isSpinning)
        {
            isSpinning = true;
            Vector3 dir = bossInfo.GetPlayerLocation().transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 180, transform.forward);

            Debug.Log("LaserMove was called");
            InvokeRepeating("PylonRotate", 0, rotationSpeed);
        }
        else
        {
            Debug.Log("LaserMove was attempted to be called twice");
        }
    }

    public void StopLaserAttackMovement()
    {
        isSpinning = false;
        CancelInvoke();
    }

    public void PylonRotate()
    {
        gameObject.transform.Rotate(0, 0, rotationAmount);
    }
}
