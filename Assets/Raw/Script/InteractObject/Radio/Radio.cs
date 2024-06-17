using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] playlist;
    private List<AudioClip> shuffledPlaylist;
    private int currentTrackIndex = 0;
    private const float volumeStep = 0.1f; // Константа для изменения громкости

    void Start()
    {
        ShufflePlaylist();
        if (shuffledPlaylist.Count > 0)
        {
            audioSource.clip = shuffledPlaylist[currentTrackIndex];
        }
    }

    void Update()
    {
        // Проверка, если трек закончился
        if (!audioSource.isPlaying && audioSource.time == 0)
        {
            NextTrack();
        }
    }

    private void ShufflePlaylist()
    {
        shuffledPlaylist = new List<AudioClip>(playlist);
        for (int i = 0; i < shuffledPlaylist.Count; i++)
        {
            int randomIndex = Random.Range(0, shuffledPlaylist.Count);
            AudioClip temp = shuffledPlaylist[i];
            shuffledPlaylist[i] = shuffledPlaylist[randomIndex];
            shuffledPlaylist[randomIndex] = temp;
        }
    }

    public void PlayPause()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.Play();
        }
    }

    public void NextTrack()
    {
        currentTrackIndex = (currentTrackIndex + 1) % shuffledPlaylist.Count;
        audioSource.clip = shuffledPlaylist[currentTrackIndex];
        audioSource.Play();
    }

    public void PreviousTrack()
    {
        currentTrackIndex--;
        if (currentTrackIndex < 0)
        {
            currentTrackIndex = shuffledPlaylist.Count - 1;
        }
        audioSource.clip = shuffledPlaylist[currentTrackIndex];
        audioSource.Play();
    }

    public void IncreaseVolume()
    {
        audioSource.volume = Mathf.Clamp(audioSource.volume + volumeStep, 0f, 1f);
    }

    public void DecreaseVolume()
    {
        audioSource.volume = Mathf.Clamp(audioSource.volume - volumeStep, 0f, 1f);
    }
}
