using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
	public float Direction;
	public Content Content;
	public Player ownerPlayer;
	public PlayerAction MyAction;
	public float speedModifier = 1f;

	void Awake ()
	{
		Destroy ( gameObject, Content.ProjectileLifetime );
	}
	
	void Start ()
	{
		GetComponent<SpriteRenderer> ().flipX = Direction < 0;

		float normalizedDirection = Direction >= 0 ? 1 : -1;
		GetComponent<Rigidbody2D> ().velocity = new Vector2 ( normalizedDirection * Content.ProjectileSpeed * speedModifier, 0 );
	}

	void OnTriggerEnter2D ( Collider2D other )
	{
		var otherPlayer = other.GetComponentInParent<Player> ();
		if ( ownerPlayer != otherPlayer )
		{
			StartCoroutine ( Explode ( otherPlayer ) );
		}
	}

	IEnumerator Explode ( Player otherPlayer )
	{
		GetComponent<Animator> ().SetTrigger ( "explode" );
		yield return new WaitForSeconds ( 0.2f );
		Destroy ( gameObject );
		if ( otherPlayer != null )
			OnDoDamaged ( otherPlayer );
	}

	void OnDoDamaged ( Player otherPlayer )
	{
		otherPlayer.HP -= MyAction.BaseDamage;
		if ( otherPlayer.Knockbackable && MyAction.KnockbackStrength > 0 )
		{
			var dir = transform.position - otherPlayer.transform.position;
			dir.Normalize ();
			Debug.DrawRay ( otherPlayer.transform.position, dir, Color.red, 1 );
			dir *= MyAction.KnockbackStrength;
			otherPlayer.myRigid.AddRelativeForce ( dir, ForceMode2D.Impulse );
		}
	}

	public void OnExecuteAction ( object[] args )
	{
		ownerPlayer = args[0] as Player;
		MyAction = args[1] as PlayerAction;

		this.transform.position = ownerPlayer.transform.position;

		Direction = ownerPlayer.Lookleft ? -1 : 1;

		Start ();
	}
}
