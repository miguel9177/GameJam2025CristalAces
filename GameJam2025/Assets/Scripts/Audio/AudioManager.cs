using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    public void PlayOneShot(AudioData clip)
    {
        audioSource.PlayOneShot(clip.Clip);
    }
}
