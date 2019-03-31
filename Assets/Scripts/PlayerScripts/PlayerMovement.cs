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

    private Animator playerAnimator;
    // 0 - Up, 1 - Right, 2 - Down, 3 - Left
    public int playerDirection = 0;
    private float diagonalSpeed = .4f;

    private bool canMove = true;

    // Use this for initialization
    void Start ()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        playerAnimator = gameObject.GetComponent<Animator>();
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
            // Rotate();
    }

    public void Movement()
    {
        //TODO Add movement sounds here, only play if velocity != 0 (More complex logic can be added later)
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
        if(slowed)
        {
            playerRigidbody.velocity = new Vector3(horizontalMovement * slowedSpeed * Time.deltaTime * 100, verticalMovement * slowedSpeed * Time.deltaTime * 100);
            ClampDiagonal();
        }
        else
        {
            playerRigidbody.velocity = new Vector3(horizontalMovement * movementSpeed * Time.deltaTime * 100, verticalMovement * movementSpeed * Time.deltaTime * 100);
            ClampDiagonal();
        }

        // print("Horizontal " + horizontalMovement + ", Vertical " + verticalMovement);
        MovementAnims();
    }

    private void ClampDiagonal()
    {
        if(Mathf.Abs(playerRigidbody.velocity.x) > .5f && Mathf.Abs(playerRigidbody.velocity.y) > .5f)
        {
            float tempX = playerRigidbody.velocity.x * .68f;
            float tempY = playerRigidbody.velocity.y * .68f;
            playerRigidbody.velocity = new Vector3(tempX, tempY);
        }
    }

    public void MovementAnims()
    {
        if(horizontalMovement >= .1)
        {
            playerAnimator.SetBool("isWalkingRight", true);
            playerAnimator.SetBool("isWalkingLeft", false);
            playerAnimator.SetBool("isWalkingUp", false);
            playerAnimator.SetBool("isWalkingDown", false);
            SetPlayerDirection(1);
        }
        else if(horizontalMovement <= -.1)
        {
            playerAnimator.SetBool("isWalkingLeft", true);
            playerAnimator.SetBool("isWalkingRight", false);
            playerAnimator.SetBool("isWalkingUp", false);
            playerAnimator.SetBool("isWalkingDown", false);
            SetPlayerDirection(3);
        }
        else if(verticalMovement >= .1)
        {
            playerAnimator.SetBool("isWalkingUp", true);
            playerAnimator.SetBool("isWalkingRight", false);
            playerAnimator.SetBool("isWalkingLeft", false);
            playerAnimator.SetBool("isWalkingDown", false);
            SetPlayerDirection(0);
        }
        else if(verticalMovement <= -.1)
        {
            playerAnimator.SetBool("isWalkingDown", true);
            playerAnimator.SetBool("isWalkingRight", false);
            playerAnimator.SetBool("isWalkingLeft", false);
            playerAnimator.SetBool("isWalkingUp", false);
            SetPlayerDirection(2);
        }
    }

    public void SetPlayerDirection(int dir)
    {
        playerDirection = dir;
    }

    public void Rotate(Transform pos)
    {
        Vector3 vectorToTarget = handler.cursorInWorldPos - new Vector3(pos.position.x, pos.position.y);
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        angle -= 90;
        Quaternion rotAngle = Quaternion.AngleAxis(angle, Vector3.forward);
        pos.rotation = Quaternion.Slerp(pos.rotation, rotAngle, rotSpeed);
    }
    
}