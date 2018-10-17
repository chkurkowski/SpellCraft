using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image playerHealthBar;
    public GameObject Player;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (playerHealthBar.fillAmount <= 0)
        {
            Destroy(Player);
            print("You Died");
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "EnemyProjectile")
        {
            playerHealthBar.fillAmount = playerHealthBar.fillAmount - 0.075f;
        }
    }
}
