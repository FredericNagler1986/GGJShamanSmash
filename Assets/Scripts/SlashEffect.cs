using UnityEngine;
using System.Collections;

public class SlashEffect : MonoBehaviour
{
	public Player ownerPlayer;
	public Player targetPlayer;
	public PlayerAction MyAction;

	public void Awake ()
	{
		Destroy ( gameObject, 1.1f );
	}

	public void OnExecuteAction ( object[] args )
	{
		if ( MyAction.Name == "Punch" )
		{
			SoundManager.Instance.PlayExecutePunchSound ();
		}
		else if ( MyAction.Name == "Slash" )
		{
			SoundManager.Instance.PlaySlashSound ();
		}
		else if ( MyAction.Name == "Super Slash" )
		{
			SoundManager.Instance.PlaySlash2Sound ();
		}
		else if ( MyAction.Name == "Hyper Slash" )
		{
			SoundManager.Instance.PlaySlash3Sound ();
		}

		ownerPlayer = args[0] as Player;
		MyAction = args[1] as PlayerAction;
		targetPlayer = args[2] as Player;

		this.transform.parent = ownerPlayer.SlashHotzone.transform;
		this.transform.localPosition = new Vector3 () ;
		this.transform.localRotation = Quaternion.identity;

		OnDoDamaged ( targetPlayer );
	}

	void OnDoDamaged ( Player otherPlayer )
	{
		if ( otherPlayer == null )
			return;

		otherPlayer.HP -= MyAction.BaseDamage;
		if ( otherPlayer.Knockbackable && MyAction.KnockbackStrength > 0 )
		{
			var dir = otherPlayer.transform.position - transform.position;
			dir.Normalize ();
			Debug.DrawRay ( otherPlayer.transform.position, dir, Color.red, 1 );
			dir *= MyAction.KnockbackStrength;
			otherPlayer.myRigid.AddRelativeForce ( dir, ForceMode2D.Impulse );
		}

		if ( MyAction.Name == "Punch" )
		{
			SoundManager.Instance.PlayPunchSuccessSound ();
		}
	}
}
