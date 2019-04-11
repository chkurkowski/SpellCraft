using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXObjectDestroyer : MonoBehaviour {

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
