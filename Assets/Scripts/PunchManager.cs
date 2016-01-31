using UnityEngine;
using System.Collections;

public class PunchManager : MonoBehaviour
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
		ownerPlayer = args[0] as Player;
		MyAction = args[1] as PlayerAction;
		targetPlayer = args[2] as Player;

		this.transform.position = ownerPlayer.transform.position;
		this.transform.localEulerAngles = new Vector3 ( 0, ownerPlayer.Lookleft ? -90 : 90, 0 );

		OnDoDamaged ( targetPlayer );
	}

	void OnDoDamaged ( Player otherPlayer )
	{
        if (otherPlayer == null)
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
	}
}
