using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "AudioData", menuName = "ScriptableObjects/AudioData", order = 1)]
public class AudioData : ScriptableObject
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private float volume;
    [SerializeField] private float cooldown;

    public AudioClip Clip => clip;
    public float Volume => volume;
    public float Cooldown => cooldown;
}
