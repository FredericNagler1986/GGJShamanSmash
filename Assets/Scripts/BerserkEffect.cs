using UnityEngine;
using System.Collections;

public class BerserkEffect : MonoBehaviour
{
	public Player ownerPlayer;
	public PlayerAction MyAction;
	public bool AppendOnPlayer;


	public void OnExecuteAction ( object[] args )
	{
		ownerPlayer = args[0] as Player;
		MyAction = args[1] as PlayerAction;

		if ( AppendOnPlayer )
		{
			transform.parent = ownerPlayer.transform;
			transform.localPosition = new Vector3 ();
		}

		StartCoroutine ( DestroyTimer () );
	}

	IEnumerator DestroyTimer ()
	{
		ownerPlayer.DamageModifier = MyAction.DamageModifier;
		ownerPlayer.SpeedModifier = MyAction.SpeedModifier;
		ownerPlayer.Knockbackable = MyAction.Knockback;

		yield return new WaitForSeconds ( MyAction.Duration );

		SoundManager.Instance.PlayBerserkEndSound ();

		ownerPlayer.DamageModifier = 1;
		ownerPlayer.SpeedModifier = 1;
		ownerPlayer.Knockbackable = true;

		var ps = GetComponent<ParticleSystem> ();
		ps.Stop ();

		while ( ps.IsAlive () )
		{
			yield return null;
		}

		Destroy ( gameObject );
	}
}
