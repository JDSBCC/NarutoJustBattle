using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour {
	[SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
	[SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	public Animator m_Anim;            // Reference to the player's animator component.
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	
	private void Awake()
	{
		// Setting up references.
		m_Anim = GetComponent<Animator>();
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	
	/*private void FixedUpdate()
	{
		
		m_Grounded = false;
		if(transform.position.y==-4){
			m_Grounded=true;
		}

		Debug.Log (m_Grounded);
		m_Anim.SetBool("ground", m_Grounded);
		
		// Set the vertical animation
		//m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
	}*/

	void OnCollisionEnter2D(Collision2D coll){
		if(coll.transform.name=="Ground"){
			m_Grounded=true;
			m_Anim.SetBool("ground", m_Grounded);
		}
	}
	
	void OnCollisionExit2D(Collision2D coll){
		if(coll.transform.name=="Ground"){
			m_Grounded=false;
			m_Anim.SetBool("ground", m_Grounded);
		}
	}
	
	
	public void Move(float move, bool jump)
	{
		//change colliders
		Destroy(GetComponent<PolygonCollider2D>());
		gameObject.AddComponent<PolygonCollider2D>();

		//only control the player if grounded or airControl is turned on
		if (m_Grounded)
		{
			
			// The Speed animator parameter is set to the absolute value of the horizontal input.
			m_Anim.SetFloat("hSpeed", Mathf.Abs(move));
			
			// Move the character
			m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);
			
			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump && m_Anim.GetBool("ground"))
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Anim.SetBool("ground", false);
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
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
