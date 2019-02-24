using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerHealth health;
    private AbilityHandler handler;
    private PlayerAbilities abilities;
    private Rigidbody2D playerRigidbody;
    public float horizontalMovement;
    public float verticalMovement;

    [Header("Movement Variables")]
    public float movementSpeed = 5;
    public float slowedSpeed;
    public float rotSpeed = 25f;
    public bool slowed = false;

    private bool canMove = true;

    // Use this for initialization
    void Start ()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        abilities = GetComponent<PlayerAbilities>();
        handler = GetComponent<AbilityHandler>();
        health = GetComponent<PlayerHealth>();
        slowedSpeed = movementSpeed * .65f;
    }
    
    // Update is called once per frame
    void Update ()
    {
        if(abilities.health.isAlive && canMove)
        {
           Movement();
        }
            Rotate();
    }

    public void Movement()
    {
        //TODO Add movement sounds here, only play if velocity != 0 (More complex logic can be added later)
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
        if(slowed)
            playerRigidbody.velocity = new Vector3(horizontalMovement * slowedSpeed * Time.deltaTime * 100, verticalMovement * slowedSpeed * Time.deltaTime * 100);
        else
            playerRigidbody.velocity = new Vector3(horizontalMovement * movementSpeed * Time.deltaTime * 100, verticalMovement * movementSpeed * Time.deltaTime * 100);
    }

    public void Rotate()
    {
        Vector3 vectorToTarget = handler.cursorInWorldPos - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        angle -= 90;
        Quaternion rotAngle = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotAngle, rotSpeed);
    }
    
}