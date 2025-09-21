using System.Collections;
using UnityEngine;

public class PlayAudioPlayerInfinitly : MonoBehaviour
{
    [SerializeField] private AudioPlayer audioPlayer;
    [SerializeField] private AudioData audioData;

    [SerializeField] private bool playOnAwake;
    private bool playing = true;
    public void Start()
    {
        if (playOnAwake)
        {
            StartCoroutine(IE_AudioPlayer());
        }
    }

    private void OnDestroy()
    {
        
    }

    public void StartPlay()
    {
        StartCoroutine(IE_AudioPlayer());
    }

    public void StopPlay()
    {
        StopAllCoroutines();
    }

    private IEnumerator IE_AudioPlayer()
    {
        audioPlayer.PlayAudio(audioData);
        while (playing)
        {
            yield return new WaitForSeconds(audioData.Cooldown);
            audioPlayer.PlayAudio(audioData);
        }
    }
}
