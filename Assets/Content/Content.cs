using UnityEngine;
using System;

[Serializable]
public class Content : ScriptableObject
{
    public int MaxOrbs;
    public float SpawnTimeOrbs;
    public float ProjectileSpeed;  
	public PlayerValues Player;

	[Serializable]
	public class PlayerValues
	{
		public float MoveSpeed = 100;
		public float JumpForce = 100;
	}
}
