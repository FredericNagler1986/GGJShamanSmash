using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public Content Content;

	public string inputPrefix;

	public Animator myAnimator;
	public Rigidbody2D myRigid;
	public SpriteRenderer myBody;

	public LayerMask GroundLayer;

	bool lookleft;
	bool grounded;

	void Awake ()
	{
	}

	/*
	void Update ()
	{
		//var x = Input.GetAxis ( inputPrefix + "Horizontal" );
		//var y = Input.GetAxis ( inputPrefix + "Vertical" );

		//if ( x != 0 )
		//{
		//	lookleft = x < 0;
		//}
		//myBody.flipX = lookleft;

		//transform.position += new Vector3 ( x, y, 0 );

		//var btnX = Input.GetButton ( inputPrefix + "X" );
		//var btnY = Input.GetButton ( inputPrefix + "Y" );
		//var btnA = Input.GetButton ( inputPrefix + "A" );
		//var btnB = Input.GetButton ( inputPrefix + "B" );
		//Debug.Log ( "X: " + btnX + "; Y: " + btnY + "; A: " + btnA + "; B: " + btnB );
	}
	*/

	void FixedUpdate ()
	{
		var x = Input.GetAxis ( inputPrefix + "Horizontal" );
		var y = Input.GetAxis ( inputPrefix + "Vertical" );

		var velo = myRigid.velocity;
		velo.x = x * Content.Player.MoveSpeed;
		myRigid.velocity = velo;


		var isGround = Mathf.Abs ( velo.y ) < 0.1f;
		if ( grounded != isGround )
		{
			grounded = isGround;
		}
		if ( !grounded && velo.y < -1f )
		{
			myAnimator.SetTrigger ( "Falling" );
		}

		if ( velo.x != 0 )
		{
			lookleft = velo.x < 0;
		}
		myBody.flipX = lookleft;

		var speedMod = Mathf.Abs ( velo.x ) / Content.Player.MoveSpeed;
		myAnimator.SetFloat ( "Speed", speedMod );
		myAnimator.SetBool ( "IsGround", isGround );
		//myAnimator.SetBool ( "IsRun", x != 0 );

		bool isGrounded = myRigid.IsTouchingLayers ( GroundLayer.value );
		var jump = Input.GetButtonDown ( inputPrefix + "A" );
		if ( (jump || y > 0.6f) && isGrounded )
		{
			myRigid.AddForce ( new Vector2 ( 0, Content.Player.JumpForce ) );
			myAnimator.SetTrigger ( "Jump" );
			myAnimator.ResetTrigger ( "Falling" );
			Debug.Log ( inputPrefix + " jump" );
		}
	}



	void OnGUI ()
	{
		GUILayout.Label ( myRigid.velocity.ToString ( "" ) );
	}
}
