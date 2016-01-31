using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
	public float Direction;
	public Content Content;
	public Player ownerPlayer;
	public PlayerAction MyAction;
	public float speedModifier = 1f;

	public ParticleSystem[] particleStartOnExplode;
	public ParticleSystem[] particleStopDetachOnExplode;

	void Awake ()
	{
		StartCoroutine ( Explode ( Content.ProjectileLifetime, null ) );
	}

	void Start ()
	{
		GetComponent<SpriteRenderer> ().flipX = Direction < 0;

		float normalizedDirection = Direction >= 0 ? 1 : -1;
		GetComponent<Rigidbody2D> ().velocity = new Vector2 ( normalizedDirection * Content.ProjectileSpeed * speedModifier, 0 );

        if (MyAction.Name == "Fireball")
        {
            SoundManager.Instance.PlayFireBallThrowSound();
        }
        else if (MyAction.Name == "Super Fireball")
        {
            SoundManager.Instance.PlayFireBall2ThrowSound();
        }
        else if (MyAction.Name == "Hyper Fireball")
        {
            SoundManager.Instance.PlayFireBall3ThrowSound();
        }
    }

	void OnTriggerEnter2D ( Collider2D other )
	{
		var otherPlayer = other.GetComponentInParent<Player> ();
		if ( ownerPlayer != otherPlayer )
		{
			StartCoroutine ( Explode ( 0.2f, otherPlayer ) );
		}
	}

	IEnumerator Explode ( float time, Player otherPlayer )
	{
		yield return new WaitForSeconds ( time );
		GetComponent<Rigidbody2D> ().velocity = new Vector2 ();
		GetComponent<Animator> ().SetTrigger ( "explode" );
		yield return new WaitForSeconds ( 1f );
		if ( particleStartOnExplode != null )
		{
			foreach ( var item in particleStartOnExplode )
			{
				if ( item != null )
					item.Play ();
			}
		}
		if ( particleStopDetachOnExplode != null )
		{
			foreach ( var item in particleStopDetachOnExplode )
			{
				if ( item != null )
				{
					item.Stop ( false );
					item.transform.parent = null;
				}
			}
		}
		Destroy ( gameObject );
		if ( otherPlayer != null )
			OnDoDamaged ( otherPlayer );
	}

	void OnDoDamaged ( Player otherPlayer )
	{
        if (MyAction.Name == "Fireball")
        {
            SoundManager.Instance.PlayFireBallExplodeSound();
        }
        else if (MyAction.Name == "Super Fireball")
        {
            SoundManager.Instance.PlayFireBall2ExplodeSound();
        }
        else if (MyAction.Name == "Hyper Fireball")
        {
            SoundManager.Instance.PlayFireBall3ExplodeSound();
        }

        otherPlayer.ChangeHP( MyAction.BaseDamage);
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
