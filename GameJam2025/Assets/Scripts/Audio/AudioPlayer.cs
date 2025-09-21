using System.Collections;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSource source;

    private bool playingAudio = false;

    public void PlayAudio(AudioData audio)
    {
        source.clip = audio.Clip;
        source.volume = audio.Volume;
        source.Play();
    }

    private IEnumerator IE_Cooldown(AudioData audio)
    {
        yield return new WaitForSeconds(audio.Cooldown);
    }
}
