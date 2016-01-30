using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public Content Content;

	public Animator myAnimator;
	public Rigidbody2D myRigid;
	public SpriteRenderer myBody;

	public LayerMask GroundLayer;

    public SpriteRenderer BodySpriteRenderer;
    public SpriteRenderer MaskSpriteRenderer;

	public bool Knockbackable = true;
	public int HP;

	bool lookleft;
	bool grounded;

    private int id;
    private string inputPrefix;

    private OrbCollector collector;
	private PlayerActionManager actionManager;

	public bool Lookleft
	{
		get { return lookleft; }
	}

    public void Init(int playerId, int maskId)
    {
        id = playerId;

        MaskSpriteRenderer.sprite = Content.GetMaskSprite(maskId);
        BodySpriteRenderer.color = Content.GetPlayerColor(playerId);
        inputPrefix = Content.GetInputPrefix(playerId);

		HP = Content.Player.StartHP;
	}

	void Start ()
	{
		collector = GetComponent<OrbCollector> ();
		actionManager = GetComponent<PlayerActionManager> ();
	}

	void FixedUpdate ()
	{
		var x = Input.GetAxis ( inputPrefix + "Horizontal" );
		var y = Input.GetAxis ( inputPrefix + "Vertical" );

		var velo = myRigid.velocity;
		if ( x != 0 )
		{
		velo.x = x * Content.Player.MoveSpeed;
		myRigid.velocity = velo;
		}

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

		bool isGrounded = myRigid.IsTouchingLayers ( GroundLayer.value );
		var jump = Input.GetButtonDown ( inputPrefix + "A" );
		if ( (jump || y > 0.6f) && isGrounded )
		{
			myRigid.AddForce ( new Vector2 ( 0, Content.Player.JumpForce ) );
			myAnimator.SetTrigger ( "Jump" );
			myAnimator.ResetTrigger ( "Falling" );
		}
	}

	void Update ()
	{
		if ( Input.GetButtonDown ( inputPrefix + "B" ) )
		{
			bool isCast = actionManager.PlayAction ( this, collector.GetCollectedOrbs () );
			Debug.Log ("cast action " + isCast);
			if ( !isCast )
			{

			}
		}
	}

	/*
	void OnGUI ()
	{
		GUILayout.Label ( myRigid.velocity.ToString ( "" ) );
	}
	*/
}
