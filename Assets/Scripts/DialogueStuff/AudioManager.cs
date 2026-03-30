using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource source;

    private void OnEnable()
    {
        EventDispatcher.instance.AddListener<PlaySound>(OnPlaySound);
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
}
