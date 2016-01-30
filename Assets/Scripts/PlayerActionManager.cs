using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
	public Content Content;

	public LayerMask punchLayer;

	public bool PlayAction ( Player owner, List<OrbType> orbList )
	{
		if ( orbList.Count == 0 )
		{
			return false;
		}

		foreach ( PlayerAction action in Content.Actions )
		{
			bool isOk = action.OrbNeeded.SequenceEqual ( orbList );
			if ( isOk )
			{
				return ExecuteAction ( owner, action, null );
			}
		}
		return false;
	}

	public bool PlayPunch ( Player owner, Vector2 point0, Vector2 point1 )
	{
		bool flag = false;
		var targets = Physics2D.OverlapAreaAll ( point0, point1, punchLayer.value );
		foreach ( var target in targets )
		{
			var player = target.GetComponentInParent<Player> ();
			if ( player != null && player != owner )
			{
				flag |= ExecuteAction ( owner, Content.ActionPunch, player );
			}
		}
		return flag;
	}

	public bool PlaySlash ( Player owner, Vector2 point0, Vector2 point1 )
	{
		bool flag = false;
		var targets = Physics2D.OverlapAreaAll ( point0, point1, punchLayer.value );
		foreach ( var target in targets )
		{
			var player = target.GetComponentInParent<Player> ();
			if ( player != null && player != owner )
			{
				flag |= ExecuteAction ( owner, Content.ActionSlash, player );
			}
		}
		return flag;
	}

	private bool ExecuteAction ( Player owner, PlayerAction action, Player target )
	{
		if ( action.EffectPrefab == null )
		{
			return false;
		}

		var instance = UnityEngine.Object.Instantiate ( action.EffectPrefab );

		instance.BroadcastMessage ( "OnExecuteAction", new object[] { owner, action, target } );

		return true;
	}
}
