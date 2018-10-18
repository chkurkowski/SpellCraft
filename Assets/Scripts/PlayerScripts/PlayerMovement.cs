using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerAbilities abilities;
    public Rigidbody2D playerRigidbody;
    public float horizontalMovement;
    public float verticalMovement;
    public float movementSpeed = 5;
    public float rotSpeed = 25f;

	// Use this for initialization
	void Start ()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        abilities = GetComponent<PlayerAbilities>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(abilities.GetTimer("evade") >= abilities.GetCooldown("evade"))
        {
            if(abilities.GetTimer("atkdash") >= abilities.GetCooldown("atkdash"))
                Movement();
        }
            Rotate();
	}

    public void Movement()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
        playerRigidbody.velocity = new Vector3(horizontalMovement * movementSpeed * Time.deltaTime * 100, verticalMovement * movementSpeed * Time.deltaTime * 100);
        //gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
    }

    public void Rotate()
    {
        Vector3 vectorToTarget = abilities.cursorInWorldPos - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        angle -= 90;
        Quaternion rotAngle = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotAngle, rotSpeed);
    }
    
}