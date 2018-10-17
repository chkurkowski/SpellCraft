using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHealth : MonoBehaviour
{

    public Image healthBar;



    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Projectile")
        {
            healthBar.fillAmount -= 0.05f;
        }
       

    }



}