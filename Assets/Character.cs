using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour {

    //Retrieve Components
    Rigidbody2D rigidbody2d;
    Animator anim;

    [SerializeField] float characterMovementSpeed = 1f;
    [SerializeField] float characterJumpSpeed = 100f;

    bool isReadyToJump = false;
    bool grounded = false;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    [SerializeField] bool walledFront = false;
    [SerializeField] bool walledBack = false;
    public Transform wallCheckFront;
    public Transform wallCheckBack;
    public LayerMask whatIsWall;

    Vector3 velocity;

    bool facingRight = true;
    // Use this for initialization
    void Start()
    {
        // Collision States
        grounded = false;
        walledFront = false;
        walledBack = false;

        // Game Components
        anim = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()

    {
        velocity = rigidbody2d.velocity;
        ProcessPlayerInput();
        Animate();
    }

    private void ProcessPlayerInput()
    {
        CheckMoveLeft();
        CheckMoveRight();
        CheckJump();
    }

    private void CheckMoveLeft()
    {
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
    }

    private void MoveLeft()
    {
        rigidbody2d.AddRelativeForce(Vector2.left * characterMovementSpeed * Time.deltaTime);
    }

    private void CheckMoveRight()
    {
        if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
    }

    private void MoveRight()
    {
        rigidbody2d.AddRelativeForce(Vector2.right * characterMovementSpeed * Time.deltaTime);
    }

    private void CheckJump()
    {
        if ((Input.GetKey(KeyCode.Space) || (Input.GetKey(KeyCode.W))) && grounded && !isReadyToJump)
        {
            isReadyToJump = true;
            Jump();
        }
    }

    private void Jump()
    {
        rigidbody2d.velocity = new Vector2 (velocity.x, characterJumpSpeed * Time.deltaTime);
        //Invoke("DisableReadyToJump",0.07f);
        DisableReadyToJump();
    }

    private void DisableReadyToJump()
    {
        isReadyToJump = false;
    }

    private void Animate()
    {
        // Set values relevant for animation processing relative to environment
        grounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.6f, 0.25f), 0, whatIsGround);
        walledFront = Physics2D.OverlapBox(wallCheckFront.position, new Vector2(0.1f, 0.1f), 0, whatIsWall);
        walledBack = Physics2D.OverlapBox(wallCheckBack.position, new Vector2(0.1f, 0.1f), 0, whatIsWall);

        anim.SetBool("Grounded", grounded);
        anim.SetBool("WalledFront", walledFront);
        anim.SetBool("WalledBack", walledBack);
        anim.SetBool("ReadyToJump", isReadyToJump);
        anim.SetFloat("vSpeed", velocity.y);
        anim.SetFloat("xSpeed", Math.Abs(velocity.x));

        TurnLeftRight();
    }

    private void TurnLeftRight()
    {
        if (velocity.x > 0.2 && !facingRight && !walledFront)
        {
            Flip();
        }
        else if (velocity.x < -0.2 && facingRight && !walledFront)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
