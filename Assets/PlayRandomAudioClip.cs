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
        ChangeClip();
    }

    private void ChangeClip()
    {
        _audioSource.clip = allSongs[Random.Range(0, allSongs.Count)];
        _audioSource.Play();
        StartCoroutine(ChangeClipTimer());
    }

    private IEnumerator ChangeClipTimer()
    {
        var timer = 0f;
        while (timer <= _audioSource.clip.length)
        {
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        ChangeClip();
    }
}
