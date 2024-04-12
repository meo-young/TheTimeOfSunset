using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioManager : MonoBehaviour
{
    static public AudioManager instance;
    public AudioClip[] clips;
    private AudioSource source;
    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Play(int _playMusicTrack)
    {
        source.volume = 1f;
        source.clip = clips[_playMusicTrack];
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }

    public void SetVolume(float _volume)
    {
        source.volume = _volume;
    }

    public void Pause()
    {
        source.Pause();
    }

    public void UnPause()
    {
        source.UnPause();
    }
}
