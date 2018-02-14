using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour {

    //Retrieve Components
    Rigidbody2D rigidbody2d;
    Animator anim;

    [SerializeField] float characterMovementSpeed = 1000f;
    [SerializeField] float characterJumpHeight = 200f;
    [SerializeField] bool grounded = false;

    public Transform groundCheck;
    float groundRadius = 0.05f;
    public LayerMask whatIsGround;
    Vector3 velocity;

    bool facingRight = true;

    // Use this for initialization
    void Start()
    {
        grounded = false;
        anim = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()

    {
        anim.SetFloat("vSpeed", velocity.y);
        anim.SetFloat("xSpeed", Math.Abs(velocity.x));
        print(velocity.x);
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        velocity = rigidbody2d.velocity;
        ProcessPlayerInput();
        ControlAnimations();
        anim.SetBool("Grounded", grounded);
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
            rigidbody2d.AddRelativeForce(Vector2.left * characterMovementSpeed * Time.deltaTime);
        }
    }

    private void CheckMoveRight()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rigidbody2d.AddRelativeForce(Vector2.right * characterMovementSpeed * Time.deltaTime);
        }
    }

    private void CheckJump()
    {
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Invoke("Jump", 1);
        }
    }

    private void Jump()
    {
            rigidbody2d.AddForce(Vector2.up * characterJumpHeight);
    }

    private void ControlAnimations()
    {
        TurnLeftRight();
        //TriggerAnimations();
    }

    private void TurnLeftRight()
    {
        if (velocity.x > 0.1 && !facingRight)
        {
            Flip();
        }
        else if (velocity.x < -0.1 && facingRight)
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

    private void InitiateIdleAnimation() { anim.SetInteger("State", 0); }

    private void InitiateWalkingAnimation() { anim.SetInteger("State", 1); }

    private void InitiateJumpingAnimation() { anim.SetInteger("State", 2); }

    private void InitiateInAirAnimation() { anim.SetInteger("State", 3); }
}
