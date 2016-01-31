using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    public Content Content;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayDeathSound()
    {
        source.PlayOneShot(Content.GetRandomDeathSound());
    }

    public void PlayGetHitSound()
    {
        source.PlayOneShot(Content.GetRandomGetHitSound());
    }

    public void PlaySpawnOrbSound()
    {
        source.PlayOneShot(Content.GetRandomSpawnOrbSound());
    }

    public void PlayCollectOrbSound()
    {
        source.PlayOneShot(Content.GetRandomCollectOrbSound());
    }

    public void PlayExecutePunchSound()
    {
        source.PlayOneShot(Content.GetRandomExecutePunchSound());
    }

    public void PlayPunchSuccessSound()
    {
        source.PlayOneShot(Content.GetRandomPunchSuccessSound());
    }

    public void PlaySlashSound()
    {
        source.PlayOneShot(Content.GetRandomSlashSound());
    }

    public void PlaySlash2Sound()
    {
        source.PlayOneShot(Content.GetRandomSlash2Sound());
    }

    public void PlaySlash3Sound()
    {
        source.PlayOneShot(Content.GetRandomSlash3Sound());
    }

    public void PlayShieldSound()
    {
        source.PlayOneShot(Content.GetRandomShieldSound());
    }

    public void PlayFireBallThrowSound()
    {
        source.PlayOneShot(Content.GetRandomFireBallThrowSound());
    }

    public void PlayFireBall2ThrowSound()
    {
        source.PlayOneShot(Content.GetRandomFireBall2ThrowSound());
    }

    public void PlayFireBall3ThrowSound()
    {
        source.PlayOneShot(Content.GetRandomFireBall3ThrowSound());
    }

    public void PlayFireBallExplodeSound()
    {
        source.PlayOneShot(Content.GetRandomFireBallExplodeSound());
    }

    public void PlayFireBall2ExplodeSound()
    {
        source.PlayOneShot(Content.GetRandomFireBall2ExplodeSound());
    }

    public void PlayFireBall3ExplodeSound()
    {
        source.PlayOneShot(Content.GetRandomFireBall3ExplodeSound());
    }

    public void PlayOrbDeniedSound()
    {
        source.PlayOneShot(Content.GetRandomOrbDeniedSound());
    }

    public void PlayLoadPunchSound()
    {
        source.PlayOneShot(Content.GetRandomLoadPunchSound());
    }

    public void PlayBerserkSound()
    {
        source.PlayOneShot(Content.GetRandomBerserkSound());
    }

    public void PlayBerserkEndSound()
    {
        source.PlayOneShot(Content.GetRandomBerserkEndSound());
    }
}
