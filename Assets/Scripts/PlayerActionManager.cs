using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerActionManager : MonoBehaviour
{
	public Content Content;

	public bool PlayAction(Player owner, List<OrbType> orbList)
	{
		if ( orbList.Count == 0 )
		{
			return false;
		}
		
		foreach ( PlayerAction action in Content.Actions )
		{
			bool isOk = true;//action.OrbNeeded.SequenceEqual ( orbList );
			if ( isOk )
			{
				return ExecuteAction ( owner, action );
			}
		}
		return false;
	}

	private bool ExecuteAction( Player owner, PlayerAction action )
	{
		if ( action.EffectPrefab == null )
		{
			return false;
		}

		var instance = Object.Instantiate ( action.EffectPrefab );

		instance.BroadcastMessage ( "OnExecuteAction", new object[] { owner, action } );

		return true;
	}
}
