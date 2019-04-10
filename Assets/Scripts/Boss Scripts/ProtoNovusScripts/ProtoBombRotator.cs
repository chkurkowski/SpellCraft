using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoBombRotator : MonoBehaviour {
    public float rotationAmount = .1f;
    public float rotationFrequency = .1f;
    // Use this for initialization
    void Start ()
    {
        InvokeRepeating("Rotate", 0, rotationFrequency);
    }

    public void Rotate()
    {
        gameObject.transform.Rotate(0, 0, rotationAmount);
    }
}
