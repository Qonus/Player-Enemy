using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    private Animator _animator;
    private Rigidbody2D rb;

    public float runSpeed = 30f;

    private float horizontalMove = 0f;
    private bool jump;
    private void Awake()
    {
        DataHolder.runSpeed = runSpeed;
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        //Inputs
        horizontalMove = DataHolder.horizontal * runSpeed;

        //Check KeyboardMove
        if (DataHolder.KeyboardMove)
        {
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }
        }
        else
        {
            jump = DataHolder.jump;
        }

        //Set velocity
        _animator.SetInteger("X_Speed", Convert.ToInt32(Mathf.Abs(DataHolder.horizontal)));
        _animator.SetFloat("Y_Velocity", rb.velocity.y);
    }
    private void FixedUpdate()
    {
        if (!_animator.GetBool("Exit"))
        {
            controller.Move(horizontalMove * Time.deltaTime, jump);
        }
        DataHolder.jump = false;
        jump = false;
    }
    public void Exit(float targetX, float exitSpeed)
    {
        //Set character position
        transform.position = new Vector3()
        {
            x = Mathf.Lerp(transform.position.x, targetX, exitSpeed),
            y = transform.position.y,
            z = 0
        };


        rb.velocity = Vector3.zero;


        if (!_animator.GetBool("Exit"))
        {
            _animator.SetBool("Exit", true);
            GetComponent<LineRenderer>().enabled = false;
        }
    }
}
