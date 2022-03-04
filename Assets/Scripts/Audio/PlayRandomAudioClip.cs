using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayRandomAudioClip : MonoBehaviour
{
    private AudioSource _audioSource;
    private GameManager _gameManager;
    private float timer;
    private bool startTimer;

    [SerializeField]
    private List<AudioClip> allSongs;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _gameManager = FindObjectOfType<GameManager>();
        if (_gameManager == null)
        {
            ChangeClip();
        }
        else
        {
            _gameManager.StartOfRace += ChangeClip;
            _gameManager.StartOfRace += () => { startTimer = true; };
        }
    }

    private void Update()
    {
        if (!startTimer) return;
        Timer();
    }

    private void ChangeClip()
    {
        _audioSource.PlayOneShot(_audioSource.clip = allSongs[Random.Range(0, allSongs.Count)]);
        timer = 0f;
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
