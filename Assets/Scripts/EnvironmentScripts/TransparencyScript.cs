using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyScript : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            CancelInvoke("IncreaseOpacity");
            InvokeRepeating("DecreaseOpacity", 0, .02f);
            //gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, .4f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CancelInvoke("DecreaseOpacity");
            InvokeRepeating("IncreaseOpacity", 0, .02f);
           // gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, 1f);
        }
    }


    private void IncreaseOpacity()
    {
        if (gameObject.GetComponent<SpriteRenderer>().color.a < 1)
        {
            gameObject.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, .1f);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, 1f);
            CancelInvoke("IncreaseOpacity");
        }
       
    }

    private void DecreaseOpacity()
    {
        if (gameObject.GetComponent<SpriteRenderer>().color.a > .4)
        {
            gameObject.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, .1f);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, .4f);
            CancelInvoke("DecreaseOpacity");

        }

           
    }
}
