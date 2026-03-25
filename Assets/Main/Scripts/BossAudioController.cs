using UnityEngine;

public class BossAudioController : MonoBehaviour
{
    public AudioSource audioSource;

    // attack
    public AudioClip idleClip;
    public AudioClip sweepClip;
    public AudioClip patClip;
    public AudioClip throw1Clip;
    public AudioClip throw2Clip;
    // hurt
    public AudioClip hurtClip;
    public AudioClip dieClip;

    public void PlayIdleSound()
    {
        audioSource.PlayOneShot(idleClip);
    }
    public void PlaySweepSound()
    {
        audioSource.PlayOneShot(sweepClip);
    }

    public void PlayPatSound()
    {
        audioSource.PlayOneShot(patClip);
    }

    public void PlayThrow1Sound()
    {
        audioSource.PlayOneShot(throw1Clip);
    }
    public void PlayThrow2Sound()
    {
        audioSource.PlayOneShot(throw2Clip);
    }

    public void PlayHurtSound()
    {
        audioSource.PlayOneShot(hurtClip);
    }

    public void PlayDieSound()
    {
        audioSource.PlayOneShot(dieClip);
    }

}
