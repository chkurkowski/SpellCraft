using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionPositionLimiter : MonoBehaviour {
    public Transform bottomLimit;
    public Transform topLimit;
    public Transform rightLimit;
    public Transform leftLimit;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
                if (gameObject.transform.position.x > rightLimit.transform.position.x)
                {
                    
                    gameObject.transform.position = new Vector2(rightLimit.transform.position.x, gameObject.transform.position.y);
                }

                if (gameObject.transform.position.x < leftLimit.transform.position.x)
                {
                   
                    gameObject.transform.position = new Vector2(leftLimit.transform.position.x, gameObject.transform.position.y);
                }

                if (gameObject.transform.position.y < bottomLimit.transform.position.y)
                {
                 
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x, bottomLimit.transform.position.y);
                }

                if (gameObject.transform.position.y > topLimit.transform.position.y)
                {
                    
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x, topLimit.transform.position.y);
                }
    }
}
