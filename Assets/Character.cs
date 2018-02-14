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
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    Vector3 velocity;

    // Use this for initialization
    void Start ()
    {
        grounded = false;
        anim = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()

    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        print(grounded);
        velocity = rigidbody2d.velocity;
        ProcessPlayerInput();
        ControlAnimations();
    }

    private void ProcessPlayerInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody2d.AddRelativeForce(Vector2.left * characterMovementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigidbody2d.AddRelativeForce(Vector2.right * characterMovementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            rigidbody2d.AddForce(Vector2.up * characterJumpHeight);
            InitiateJumpingAnimation();
        }
    }

    private void ControlAnimations()
    {
        if (grounded)
            if (velocity.x == 0)
            {
                InitiateIdleAnimation();
            }
            else
            {
                InitiateWalkingAnimation();
            }
        else
        {
            InitiateInAirAnimation();
            print("InAirAnimationActivated");
        }
    }

    private void InitiateIdleAnimation()
    {
        anim.SetInteger("State", 0);
    }

    private void InitiateWalkingAnimation()
    {
        anim.SetInteger("State", 1);
    }

    private void InitiateJumpingAnimation()
    {
        anim.SetInteger("State", 2);
    }

    private void InitiateInAirAnimation()
    {
        anim.SetInteger("State", 3);
    }

}
