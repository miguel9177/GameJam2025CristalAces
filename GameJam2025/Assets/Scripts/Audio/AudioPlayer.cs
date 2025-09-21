using System.Collections;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSource source;

    private bool playingAudio = false;

    public void PlayAudio(AudioData audio)
    {
        if (playingAudio)
            return;
        source.clip = audio.Clip;
        source.volume = audio.Volume;
        source.Play();
        StartCoroutine(IE_Cooldown(audio));
    }

    private IEnumerator IE_Cooldown(AudioData audio)
    {
        playingAudio = true;
        yield return new WaitForSeconds(audio.Cooldown);
        playingAudio = false;
    }
}
