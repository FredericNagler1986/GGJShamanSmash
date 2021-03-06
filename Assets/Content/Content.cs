﻿using UnityEngine;
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
	
    public AudioClip DefaultSound;
    public List<AudioClip> DeathSounds;
    public List<AudioClip> GetHitSounds;

    public List<AudioClip> SpawnOrbSounds;
    public List<AudioClip> CollectOrbSounds;

    public List<AudioClip> ExecutePunchSounds;
    public List<AudioClip> PunchSuccessSounds;

    public List<AudioClip> SlashSounds;
    public List<AudioClip> Slash2Sounds;
    public List<AudioClip> Slash3Sounds;

    public List<AudioClip> ShieldSounds;

    public List<AudioClip> FireBallThrowSounds;
    public List<AudioClip> FireBall2ThrowSounds;
    public List<AudioClip> FireBall3ThrowSounds;
    public List<AudioClip> FireBallExplodeSounds;
    public List<AudioClip> FireBall2ExplodeSounds;
    public List<AudioClip> FireBall3ExplodeSounds;

    public List<AudioClip> OrbDeniedSounds;
    public List<AudioClip> LoadPunchSounds;
    public List<AudioClip> BerserkSounds;
    public List<AudioClip> BerserkEndSounds;

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

    public AudioClip GetRandomSlash2Sound()
    {
        return GetRandomSound(Slash2Sounds);
    }

    public AudioClip GetRandomSlash3Sound()
    {
        return GetRandomSound(Slash3Sounds);
    }

    public AudioClip GetRandomShieldSound()
    {
        return GetRandomSound(ShieldSounds);
    }

    public AudioClip GetRandomFireBallThrowSound()
    {
        return GetRandomSound(FireBallThrowSounds);
    }

    public AudioClip GetRandomFireBall2ThrowSound()
    {
        return GetRandomSound(FireBall2ThrowSounds);
    }

    public AudioClip GetRandomFireBall3ThrowSound()
    {
        return GetRandomSound(FireBall3ThrowSounds);
    }

    public AudioClip GetRandomFireBallExplodeSound()
    {
        return GetRandomSound(FireBallExplodeSounds);
    }

    public AudioClip GetRandomFireBall2ExplodeSound()
    {
        return GetRandomSound(FireBall2ExplodeSounds);
    }

    public AudioClip GetRandomFireBall3ExplodeSound()
    {
        return GetRandomSound(FireBall3ExplodeSounds);
    }

    public AudioClip GetRandomOrbDeniedSound()
    {
        return GetRandomSound(OrbDeniedSounds);
    }

    public AudioClip GetRandomLoadPunchSound()
    {
        return GetRandomSound(LoadPunchSounds);
    }

    public AudioClip GetRandomBerserkSound()
    {
        return GetRandomSound(BerserkSounds);
    }

    public AudioClip GetRandomBerserkEndSound()
    {
        return GetRandomSound(BerserkEndSounds);
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

public enum AttackType
{
	None, Punch, Slash, Projectile, Summon, SummonShield,
	_Count
}

[Serializable]
public class PlayerValues
{
	public float MoveSpeed = 100;
	public float JumpForce = 100;
	public int StartHP = 100;

	public float PunchCooldown = 1.2f;
	public float PunchKnockback = 1.2f;

	public float PunchLength = 0.632f;
	public float SlashLength = 1.2f;
	public float ProjectileLength = 1.2f;
	public float SummonLength = 1.2f;
	public float SummonShieldLength = 1.2f;
}

[Serializable]
public class PlayerAction
{
	public string Name = "Action";
	public AttackType AttackType;
	// player mod values
	public bool Knockback = false;
	public float SpeedModifier = 1f;
	public float DamageModifier = 1f;

	// 
	public int BaseDamage = 0;
	public int KnockbackStrength = 0;

	//
	public float Cooldown = 1f;
	public float Duration = 0;

	public bool CheckOrbSequence = true;
	public OrbType[] OrbNeeded;

	public GameObject EffectPrefab;
}
