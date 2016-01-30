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

    public void PlayShieldSound()
    {
        source.PlayOneShot(Content.GetRandomShieldSound());
    }

    public void PlayFireBallThrowSound()
    {
        source.PlayOneShot(Content.GetRandomFireBallThrowSound());
    }

    public void PlayFireBallExplodeSound()
    {
        source.PlayOneShot(Content.GetRandomFireBallExplodeSound());
    }
}
