using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	public Content Content;

	public Animator myAnimator;
	public Rigidbody2D myRigid;
	public SpriteRenderer myBody;

	public LayerMask GroundLayer;

	public BoxCollider2D PunchHotzone;
	public BoxCollider2D SlashHotzone;

	public SpriteRenderer BodySpriteRenderer;
	public SpriteRenderer MaskSpriteRenderer;

	public List<Image> Slots;

	public bool Knockbackable = true;
	public int HP;

	private bool lookleft;
	private bool grounded;

	private PlayerAction targetAction;
	private string inputPrefix;

	private float moveInputBlockTime;
	private float punchCooldown;
	private OrbCollector collector;
	private PlayerActionManager actionManager;

	private Image healthImage;

	public bool Lookleft
	{
		get { return lookleft; }
	}

	public void Init ( int playerId, int maskId, Image healthImage, Image slot1, Image slot2, Image slot3 )
	{
		//id = playerId;

		MaskSpriteRenderer.sprite = Content.GetMaskSprite ( maskId );
		BodySpriteRenderer.color = Content.GetPlayerColor ( playerId );
		inputPrefix = Content.GetInputPrefix ( playerId );

		HP = Content.Player.StartHP;

		this.healthImage = healthImage;

		Slots = new List<Image> ();
		Slots.Add ( slot1 );
		Slots.Add ( slot2 );
		Slots.Add ( slot3 );
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
		velo = Vector2.ClampMagnitude ( velo, 10 );

		bool blockMoveInput = Time.time > moveInputBlockTime;

		if ( x != 0 && blockMoveInput )
		{
			velo.x = x * Content.Player.MoveSpeed;
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

		if ( !Mathf.Approximately ( velo.x, 0 ) )
		{
			if ( velo.x < -0.01f || velo.x > 0.01f )
			{
				lookleft = velo.x < 0;
			}
		}

		transform.localScale = new Vector3 ( lookleft ? -1 : 1, 1, 1 );

		var speedMod = Mathf.Abs ( velo.x ) / Content.Player.MoveSpeed;
		myAnimator.SetFloat ( "Speed", speedMod );
		myAnimator.SetBool ( "IsGround", isGround );

		bool isGrounded = myRigid.IsTouchingLayers ( GroundLayer.value );
		var jump = Input.GetButtonDown ( inputPrefix + "A" );
		if ( (jump || y > 0.6f) && isGrounded && blockMoveInput )
		{
			myRigid.AddForce ( new Vector2 ( 0, Content.Player.JumpForce ) );
			myAnimator.SetTrigger ( "Jump" );
			myAnimator.ResetTrigger ( "Falling" );
		}


		myRigid.velocity = velo;

		this.healthImage.fillAmount = 1.0f - (float)HP / (float)Content.Player.StartHP;

		UpdateOrbs ();
	}

	void UpdateOrbs ()
	{
		var collectedOrbs = collector.GetCollectedOrbs ();

		if ( collectedOrbs == null )
			return;

		for ( var i = 0; i < 3; i++ )
		{
			if ( i >= collectedOrbs.Count )
			{
				Slots[i].gameObject.SetActive ( false );
				continue;
			}

			var orbType = collectedOrbs[i];
			if ( orbType == OrbType.Fire )
			{
				Slots[i].sprite = Content.FireOrbSprite;
			}
			else if ( orbType == OrbType.Shield )
			{
				Slots[i].sprite = Content.ShieldOrbSprite;
			}
			else if ( orbType == OrbType.Melee )
			{
				Slots[i].sprite = Content.MeleeOrbSprite;
			}

			Slots[i].gameObject.SetActive ( true );
		}
	}

	void Update ()
	{
		bool block = Time.time > moveInputBlockTime;
		bool punchblock = Time.time > punchCooldown;
		myAnimator.SetBool ( "IsBlock", !block );

		// use orb effect
		if ( block && Input.GetButtonDown ( inputPrefix + "B" ) )
		{
			var action = actionManager.FindAction ( collector.GetCollectedOrbs () );
			if ( action != null )
			{
				Debug.Log ( "cast action " + action.Name );

				switch ( action.AttackType )
				{
					case AttackType.Punch:
						if ( punchblock )
						{
							targetAction = action;
							collector.ClearCollectedOrbs ();
							moveInputBlockTime = Time.time + Content.Player.PunchLength;
							punchCooldown = Time.time + Content.Player.PunchCooldown;
							myRigid.velocity *= 0.5f;
							myAnimator.SetTrigger ( "Punch" );
						}
						break;
					case AttackType.Slash:
						if ( punchblock )
						{
							targetAction = action;
							collector.ClearCollectedOrbs ();
							moveInputBlockTime = Time.time + Content.Player.SlashLength;
							punchCooldown = Time.time + Content.Player.SlashCooldown;
							myRigid.velocity *= 0.5f;
							myAnimator.SetTrigger ( "Slash" );
						}
						break;
					case AttackType.Projectile:
						if ( punchblock )
						{
							targetAction = action;
							collector.ClearCollectedOrbs ();
							moveInputBlockTime = Time.time + Content.Player.ProjectileLength;
							punchCooldown = Time.time + Content.Player.ProjectileCooldown;
							myRigid.velocity *= 0.5f;
							myAnimator.SetTrigger ( "Shoot" );
						}
						break;
					case AttackType.Summon:
						if ( punchblock )
						{
							targetAction = action;
							collector.ClearCollectedOrbs ();
							moveInputBlockTime = Time.time + Content.Player.SummonLength;
							punchCooldown = Time.time + Content.Player.SummonCooldown;
							myRigid.velocity *= 0.5f;
							myAnimator.SetTrigger ( "Summon" );
						}
						break;
					case AttackType.SummonShield:
						if ( punchblock )
						{
							targetAction = action;
							collector.ClearCollectedOrbs ();
							moveInputBlockTime = Time.time + Content.Player.SummonShieldLength;
							punchCooldown = Time.time + Content.Player.SummonShieldCooldown;
							myRigid.velocity *= 0.5f;
							myAnimator.SetTrigger ( "SummonShield" );
						}
						break;
				}

			}
		}

		// simple punch attack
		if ( punchblock && Input.GetButtonDown ( inputPrefix + "X" ) )
		{
			targetAction = Content.ActionPunch;
			moveInputBlockTime = Time.time + Content.Player.PunchLength;
			punchCooldown = Time.time + Content.Player.PunchCooldown;
			myRigid.velocity *= 0.5f;
			myAnimator.SetTrigger ( "Punch" );
		}
	}

	void OnPunch ()
	{
		var point0 = (Vector2)PunchHotzone.transform.position + PunchHotzone.offset - PunchHotzone.size * 0.5f;
		var point1 = (Vector2)PunchHotzone.transform.position + PunchHotzone.offset + PunchHotzone.size * 0.5f;

		bool isCast = actionManager.PlayAction ( this, targetAction, point0, point1 );
		Debug.Log ( "cast punch " + isCast );
		if ( isCast )
		{
		}
	}

	void OnSlash ()
	{
		var point0 = (Vector2)SlashHotzone.transform.position + SlashHotzone.offset - SlashHotzone.size * 0.5f;
		var point1 = (Vector2)SlashHotzone.transform.position + SlashHotzone.offset + SlashHotzone.size * 0.5f;

		bool isCast = actionManager.PlayAction ( this, targetAction, point0, point1 );
		Debug.Log ( "cast slash " + isCast );
		if ( isCast )
		{
		}
	}

	void OnProjectile ()
	{
		bool isCast = actionManager.ExecuteAction (this, targetAction, null);
		Debug.Log ( "cast Projectile " + isCast );
		if ( isCast )
		{
		}
	}

	void OnSummon ()
	{
		bool isCast = actionManager.ExecuteAction ( this, targetAction, null );
		Debug.Log ( "cast Summon " + isCast );
		if ( isCast )
		{
		}
	}

	void OnSummonShield ()
	{
		bool isCast = actionManager.ExecuteAction ( this, targetAction, null );
		Debug.Log ( "cast SummonShield " + isCast );
		if ( isCast )
		{
		}
	}
}
