using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHealth : MonoBehaviour
{
    //boss health bar
    public Image bossHealthBar;
    public GameObject boss;



	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () 
    {
	}


    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "Fireball(Clone)") 
        {
            bossHealthBar.fillAmount -= 10;
        }
    }
}
