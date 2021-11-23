using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    [SerializeField] private AudioSource _sound;
    [SerializeField] private AudioClip[] _Fx;

    private void Awake()
    {
        instance = this;
    }

    public void OnEnter()
    {
        _sound.PlayOneShot(_Fx[0]);
    }

    public void OnDown()
    {
        _sound.PlayOneShot(_Fx[1]);
    }

    public void Shoot()
    {
        _sound.PlayOneShot(_Fx[2]);
    }

    public void DestroyPlayer()
    {
        _sound.PlayOneShot(_Fx[3]);
    }
        
    public void BrokenHighAsteroid()
    {
        _sound.PlayOneShot(_Fx[4]);
    }

    public void BrokenMiddleAsteroid()
    {
        _sound.PlayOneShot(_Fx[5]);
    }

    public void BrokenLightAsteroid()
    {
        _sound.PlayOneShot(_Fx[6]);
    }

    public void Knock()
    {
        _sound.PlayOneShot(_Fx[7]);
    }
}
