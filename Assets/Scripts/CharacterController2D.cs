using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	public float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[Range(0, .5f)] [SerializeField] private float moveSmoothing = 0f;
	[Range(0, .5f)] [SerializeField] private float jumpSmoothing = 0f;
	public LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	public float k_GroundedRadius = .1f; // Radius of the overlap circle to determine if grounded
	public GameObject landedParticle;
	public GameObject jumpParticle;

	bool previousGrounded = false;
	[HideInInspector]public float m_MovementSmoothing = 0f;
	private bool m_Grounded;            // Whether or not the player is grounded.
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	[HideInInspector] public Vector3 m_Velocity = Vector3.zero;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		m_Grounded = false;
		transform.parent = null;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
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


	public void Move(float move, bool jump)
	{
		// Move the character by finding the target velocity
		Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
		// And then smoothing it out and applying it to the character
		m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);


		//Instantiate particles when character jumps and landed
		if (previousGrounded != m_Grounded)
			if (m_Grounded)
				Instantiate(landedParticle, transform.position, Quaternion.identity);
			else if (m_Rigidbody2D.velocity.y > 0)
				Instantiate(jumpParticle, transform.position - new Vector3(0, 0.8f, 0), Quaternion.Euler(0, 0, 90));
		previousGrounded = m_Grounded;


		// If the input is moving the player right and the player is facing left...
		if (move > 0 && !m_FacingRight)
		{
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (move < 0 && m_FacingRight)
		{
			Flip();
		}


		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			m_Grounded = false;
			transform.parent = null;
		}


		//Smoothing value change
		if (m_Grounded)
        {
			m_MovementSmoothing = moveSmoothing;
		}
		else
        {
			m_MovementSmoothing = jumpSmoothing;
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}