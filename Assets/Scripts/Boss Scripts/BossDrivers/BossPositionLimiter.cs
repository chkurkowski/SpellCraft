using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPositionLimiter : MonoBehaviour {
    public Transform bottomLimit;
    public Transform topLimit;
    public Transform rightLimit;
    public Transform leftLimit;
    private BossHealth bossHealthInfo;
	// Use this for initialization
	void Start ()
    {
        bossHealthInfo = gameObject.GetComponent<BossHealth>();
       
	}

    void Update()
    {
        if(bossHealthInfo.isAlive)
        {
            if (gameObject.transform.position.x > rightLimit.transform.position.x)
            {
                Debug.Log("Right Limit Triggered!");
                gameObject.transform.position = new Vector2(rightLimit.transform.position.x, gameObject.transform.position.y);
            }

            if (gameObject.transform.position.x < leftLimit.transform.position.x)
            {
                Debug.Log("Left Limit Triggered!");
                gameObject.transform.position = new Vector2(leftLimit.transform.position.x, gameObject.transform.position.y);
            }

            if (gameObject.transform.position.y < bottomLimit.transform.position.y)
            {
                Debug.Log("Bottom Limit Triggered!");
                gameObject.transform.position = new Vector2(gameObject.transform.position.y, bottomLimit.transform.position.y);
            }

            if (gameObject.transform.position.y > topLimit.transform.position.y)
            {
                Debug.Log("Top Limit Triggered!");
                gameObject.transform.position = new Vector2(gameObject.transform.position.y, topLimit.transform.position.y);
            }
        }
    }

}
