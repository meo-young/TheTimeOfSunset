using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    AudioManager theAudio;
    public int playMusicTrack;
    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        theAudio.Play(playMusicTrack);
        this.gameObject.SetActive(false);
    }
}
