using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomAudioClip : MonoBehaviour
{
    private AudioSource _audioSource;
    
    [SerializeField]
    private List<AudioClip> allSongs;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = allSongs[Random.Range(0, allSongs.Count)];
        _audioSource.Play();
    }
}
