using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform groundCheck;
    public Animator _animator;
    public bool inverseMovement = false;
    public GameObject landedParticle;
    public GameObject jumpParticle;

    //Rigidbody
    private CharacterController2D _controller;
    private Rigidbody2D rb;
    private LevelLoader loader;

    bool previousGrounded = false;
    bool jump = false;
    float horizontal = 0;
    private bool m_Grounded = false;
    bool facingRight = true;
    void Awake()
    {
        loader = FindObjectOfType<LevelLoader>();
        _controller = FindObjectOfType<CharacterController2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //Inputs
        horizontal = DataHolder.horizontal * DataHolder.runSpeed;

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

        //inverseMovement
        if (inverseMovement)
        {
            horizontal *= -1;
        }

        //Instantiate particles when character jumps and landed
        if (previousGrounded != m_Grounded)
            if (m_Grounded)
                Instantiate(landedParticle, transform.position, Quaternion.identity);
            else if (rb.velocity.y > 0)
                Instantiate(jumpParticle, transform.position - new Vector3(0, 0.8f, 0), Quaternion.Euler(0, 0, 90));
        previousGrounded = m_Grounded;

        //Animator
        _animator.SetInteger("X_Speed", Convert.ToInt32(Mathf.Abs(horizontal)));
        _animator.SetFloat("Y_Velocity", rb.velocity.y);
    }
    private void FixedUpdate()
    {
        Move();
        jump = false;

        //GroundCheck
        m_Grounded = false;
        transform.parent = null;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, _controller.k_GroundedRadius, _controller.m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                foreach (var c in colliders)
                {
                    if (c.CompareTag("MovingPlatform"))
                    {
                        transform.parent = c.transform;
                    }
                }
            }
        }
    }
    private void Move()
    {
        //Find TargetVelocity
        Vector2 _targetVelocity = new Vector2()
        {
            x = (horizontal * Time.deltaTime) * 10f,
            y = rb.velocity.y
        };

        if (!_animator.GetBool("Exit"))
        {
            //Apply
            rb.velocity = Vector3.SmoothDamp(rb.velocity, _targetVelocity, ref _controller.m_Velocity, _controller.m_MovementSmoothing);
        }

        //Jump
        if (jump && m_Grounded)
        {
            m_Grounded = false;
            rb.AddForce(new Vector2(0f, _controller.m_JumpForce));
        }

        //Flip enemy
        if (horizontal < 0 && facingRight)
        {
            Flip();
        }
        else if (horizontal > 0 && !facingRight)
        {
            Flip();
        }
    }

    //Kill Player if he staying to close
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            loader.StartCoroutine("Death");
        }
    }
    public void Exit(float targetX, float exitSpeed)
    {
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
            loader.StartCoroutine("GameOver");
        }
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
