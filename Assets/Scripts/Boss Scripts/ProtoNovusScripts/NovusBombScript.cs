using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovusBombScript : MonoBehaviour
{
    public float moveSpeed = 50;

    public bool bombExploded = false;
    public Bomb bomb1;
    public Bomb bomb2;
    public GameObject energyLink;
    private bool genericBoolSwitch = true;

    private void Start()
    {
        transform.Rotate(new Vector3(0, 0, 90));
    }
    void FixedUpdate ()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        if(bombExploded && genericBoolSwitch)
        {
            Destroy(energyLink);
            genericBoolSwitch = false;
            if(bomb1 != null)
            {
                bomb1.Explode();
            }
            if(bomb2 != null)
            {
                bomb2.Explode();
            }
        }
    }

}
