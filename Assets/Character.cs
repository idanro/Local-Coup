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
    [SerializeField] bool isOnGround;
    Vector3 velocity;
    // Use this for initialization
    void Start ()
    {
        isOnGround = false;
        anim = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()

    {
        print(isOnGround);
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
        if (Input.GetKey(KeyCode.Space) && isOnGround)
        {
            rigidbody2d.AddForce(Vector2.up * characterJumpHeight);
        }
    }

    void OnCollisionEnter (Collision collision) // Need to fix for collision from below only
    {
        if(collision.gameObject.name == "Ground")
        {
            print("COLLIDING WITH GROUND");
            isOnGround = true;
        }
    }

    private void ControlAnimations()
    {
        if (isOnGround && (velocity.x == 0) && (velocity.y == 0))
        {
            InitiateIdleAnimation();
        }
        else if (isOnGround && (velocity.x != 0) && (velocity.y == 0))
        {
            InitiateWalkingAnimation();
        }
        if (!isOnGround)
        {
            InitiateInAirAnimation();
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
