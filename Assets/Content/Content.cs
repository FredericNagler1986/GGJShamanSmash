using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Content : ScriptableObject
{
    public int MaxOrbs;
    public float SpawnTimeOrbs;
    public float ProjectileSpeed;
	public float ProjectileLifetime;

    public PlayerValues Player;

	public PlayerAction ActionPunch;
	public PlayerAction[] Actions;


    [Serializable]
    public class PlayerValues
    {
        public float MoveSpeed = 100;
        public float JumpForce = 100;
		public int StartHP = 100;
		public float PunchLength = 0.632f;
		public float PunchCooldown = 1.2f;
		public float PunchKnockback = 1.2f;
	}

    public AudioClip DefaultSound;
    public List<AudioClip> DeathSounds;
    public List<AudioClip> GetHitSounds;

    public List<AudioClip> SpawnOrbSounds;
    public List<AudioClip> CollectOrbSounds;

    public List<AudioClip> ExecutePunchSounds;
    public List<AudioClip> PunchSuccessSounds;

    public List<AudioClip> SlashSounds;

    public List<AudioClip> ShieldSounds;

    public List<AudioClip> FireBallThrowSounds;
    public List<AudioClip> FireBallExplodeSounds;
    
    public List<Color> PlayerColors;
    public List<Sprite> Masks;
    public List<string> InputPrefixes;

    public Sprite FireOrbSprite;
    public Sprite ShieldOrbSprite;
    public Sprite MeleeOrbSprite;

    private AudioClip GetRandomSound(List<AudioClip> sounds)
    {
        if (sounds.Count == 0)
            return DefaultSound;

        return sounds[UnityEngine.Random.Range(0, sounds.Count)];
    }

    public AudioClip GetRandomDeathSound()
    {
        return GetRandomSound(DeathSounds);
    }

    public AudioClip GetRandomGetHitSound()
    {
        return GetRandomSound(GetHitSounds);
    }
    
    public AudioClip GetRandomSpawnOrbSound()
    {
        return GetRandomSound(SpawnOrbSounds);
    }
    
    public AudioClip GetRandomCollectOrbSound()
    {
        return GetRandomSound(CollectOrbSounds);
    }

    public AudioClip GetRandomExecutePunchSound()
    {
        return GetRandomSound(ExecutePunchSounds);
    }

    public AudioClip GetRandomPunchSuccessSound()
    {
        return GetRandomSound(PunchSuccessSounds);
    }

    public AudioClip GetRandomSlashSound()
    {
        return GetRandomSound(SlashSounds);
    }

    public AudioClip GetRandomShieldSound()
    {
        return GetRandomSound(ShieldSounds);
    }

    public AudioClip GetRandomFireBallThrowSound()
    {
        return GetRandomSound(FireBallThrowSounds);
    }

    public AudioClip GetRandomFireBallExplodeSound()
    {
        return GetRandomSound(FireBallExplodeSounds);
    }

    public Color GetPlayerColor(int id)
    {
        return PlayerColors[id];
    }

    public Sprite GetMaskSprite(int id)
    {
        return Masks[id];
    }

    public string GetInputPrefix(int id)
    {
        return InputPrefixes[id];
    }
}


[Serializable]
public class PlayerAction
{
	public string Name = "Action";
	// player mod values
	public bool Knockback = false;
	public float SpeedModifier = 1f;
	public float DamageModifier = 1f;

	// 
	public int BaseDamage = 0;
	public int KnockbackStrength = 0;

	public OrbType[] OrbNeeded;

	public GameObject EffectPrefab;
}
