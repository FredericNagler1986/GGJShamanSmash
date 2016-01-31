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

	public PlayerAction FindAction ( List<OrbType> orbList )
	{
		foreach ( PlayerAction action in Content.Actions.OrderByDescending ( ( a ) => a.OrbNeeded.Length ) )
		{
			bool found = true;
			if ( action.CheckOrbSequence )
			{
				for ( int i = 0; i < action.OrbNeeded.Length; i++ )
				{
					if ( i >= orbList.Count )
					{
						found = false;
						break;
					}
					OrbType item = action.OrbNeeded[i];
					if ( orbList[i] != item )
					{
						found = false;
						break;
					}
				}
			}
			else
			{
				for ( int i = 0; i < action.OrbNeeded.Length; i++ )
				{
					OrbType item = action.OrbNeeded[i];
					found &= orbList.Contains ( item );
				}
			}
			if ( found )
			{
				orbList.RemoveRange ( 0, action.OrbNeeded.Length );
				return action;
			}
		}
		return null;
	}

	public bool PlayAction ( Player owner, PlayerAction action, Vector2 point0, Vector2 point1 )
	{
        if (action.Name == "Punch")
        {
            SoundManager.Instance.PlayExecutePunchSound();
        }
        else if (action.Name == "Slash")
        {
            SoundManager.Instance.PlaySlashSound();
        }
        else if (action.Name == "Super Slash")
        {
            SoundManager.Instance.PlaySlash2Sound();
        }
        else if (action.Name == "Hyper Slash")
        {
            SoundManager.Instance.PlaySlash3Sound();
        }

		var targets = Physics2D.OverlapAreaAll ( point0, point1, punchLayer.value );
		bool flag = false;
		foreach ( var target in targets )
		{
			var player = target.GetComponentInParent<Player> ();
			if ( player != null && player != owner )
			{
				flag |= ExecuteAction ( owner, action, player );
			}
		}
		if ( !flag )
		{
			ExecuteAction ( owner, action, null );
		}
		return flag;
	}
	/*
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
	*/
	public bool ExecuteAction ( Player owner, PlayerAction action, Player target )
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
