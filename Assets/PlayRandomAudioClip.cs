using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayRandomAudioClip : MonoBehaviour
{
    private AudioSource _audioSource;
    private float timer;
    
    [SerializeField]
    private List<AudioClip> allSongs;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        ChangeClip();
    }

    private void Update()
    {
        Timer();
    }

    private void ChangeClip()
    {
        _audioSource.PlayOneShot(_audioSource.clip = allSongs[Random.Range(0, allSongs.Count)]);
    }


    private void Timer()
    {
        timer += Time.deltaTime * _audioSource.pitch;
        if (timer >= _audioSource.clip.length + 1)
        {
            timer = 0f;
            ChangeClip();
        }
    }
}
