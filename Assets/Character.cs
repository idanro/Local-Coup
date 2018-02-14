﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour {

    Rigidbody2D rigidbody2d;

    [SerializeField] float characterMovementSpeed = 1000f;
    [SerializeField] float characterJumpHeight = 200f;

    // Use this for initialization
    void Start ()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        MovingSideways();
        StartJump();
    }

    private void StartJump()
    {
        bool isJumping = true;
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody2d.AddForce(Vector2.up * characterJumpHeight);
        }
    }

    private void MovingSideways()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody2d.AddRelativeForce(Vector2.left * characterMovementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigidbody2d.AddRelativeForce(Vector2.right * characterMovementSpeed * Time.deltaTime);
        }
    }
}
