using NUnit.Framework;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource source;

    private void OnEnable()
    {
        EventDispatcher.instance.AddListener<PlaySound>(OnPlaySound);
        EventDispatcher.instance.AddListener<PlayRandomSound>(OnPlayRandomSound);
    }

    private void OnDisable()
    {
        EventDispatcher.instance.RemoveListener<PlaySound>(OnPlaySound);
    }
    
    private void OnPlaySound(PlaySound eventData)
    {
        source.Stop();
        source.PlayOneShot(eventData.sound);
    }

    private void OnPlayRandomSound(PlayRandomSound eventData)
    {
        source.Stop();
        source.pitch = Random.Range(eventData.minPitch, eventData.maxPitch);
        source.PlayOneShot(eventData.sounds[Random.Range(0,eventData.sounds.Count)]);
    }


}
