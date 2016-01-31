using UnityEngine;
using System.Collections;

public class ShieldEffect : MonoBehaviour
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
		yield return new WaitForSeconds ( MyAction.Duration );

		var ps = GetComponent<ParticleSystem> ();
		ps.Stop ();

		while ( ps.IsAlive () )
		{
			yield return null;
		}

		Destroy ( gameObject );
	}
}
