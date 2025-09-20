using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeech", menuName = "ScriptableObjects/SpeechData")]
public class SpeechData : ScriptableObject
{
    [TextArea(3, 5)]
    [SerializeField] string[] lines;
    [SerializeField] private Sprite icon;

    public string[] Lines => lines;
    public Sprite Icon => icon;
}
