using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour {

    public int magicBallDamage = 5;
    public float magicBallSpeed = 50;
   

    private void Start()
    {
        transform.Rotate(new Vector3(0, 0, 90));
    }
    private void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * magicBallSpeed);
        // transform.localScale += new Vector3(1,0);
    }
    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Boss")
        {
            //Do damage
            print("Hit: 5 damage");
            Destroy(gameObject);
        }
        else if (col.gameObject.tag != "Player" && gameObject.tag != "Reflect")
        {
            if (col.gameObject.tag != "Boss" || gameObject.tag != "CameraTrigger")
            {
                Destroy(gameObject);
            }

        }
    }
}
