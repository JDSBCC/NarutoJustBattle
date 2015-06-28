using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

	public class CharacterControl : MonoBehaviour {
		private CharacterMove m_Character;
		private bool m_Jump;
		
		
		private void Awake()
		{
			m_Character = GetComponent<CharacterMove>();
		}
		
		
		private void Update()
		{
			if (!m_Jump)
			{
				// Read the jump input in Update so button presses aren't missed.
			m_Jump = Input.GetKeyDown(KeyCode.UpArrow);
		}
		}
		
		
		private void FixedUpdate()
		{
			// Read the inputs.
			float h = CrossPlatformInputManager.GetAxis("Horizontal");
			// Pass all parameters to the character control script.
			m_Character.Move(h, m_Jump);
			m_Jump = false;
		}
	}
