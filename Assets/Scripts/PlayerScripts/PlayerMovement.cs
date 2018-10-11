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

	// Use this for initialization
	void Start ()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        abilities = GetComponent<PlayerAbilities>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(abilities.state.ToString() != "EVADE")
            Movement();
	}

    public void Movement()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
        playerRigidbody.velocity = new Vector3(horizontalMovement * movementSpeed * Time.deltaTime * 100, verticalMovement * movementSpeed * Time.deltaTime * 100);
    }
}
