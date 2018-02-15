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
    [SerializeField] float characterJumpSpeed = 100f;
    [SerializeField] bool grounded = false;

    bool isReadyToJump = false;

    public Transform groundCheck;
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
        grounded = Physics2D.OverlapBox(groundCheck.position, new Vector2 (0.7f,0.25f), 0, whatIsGround);
        velocity = rigidbody2d.velocity;
        ProcessPlayerInput();

        // Get values relevant for animation processing
        anim.SetBool("Grounded", grounded);
        anim.SetBool("ReadyToJump", isReadyToJump);
        anim.SetFloat("vSpeed", velocity.y);
        anim.SetFloat("xSpeed", Math.Abs(velocity.x));

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
        if (Input.GetKey(KeyCode.Space) && grounded && !isReadyToJump)
        {
            isReadyToJump = true;
            Invoke("Jump", 0.1f);
        }
    }

    private void Jump()
    {
        rigidbody2d.velocity = new Vector2 (velocity.x, characterJumpSpeed * Time.deltaTime);
        Invoke("DisableReadyToJump",0.11f);
    }

    private void DisableReadyToJump()
    {
        isReadyToJump = false;
    }

    private void Animate()
    {
        TurnLeftRight();
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
}
