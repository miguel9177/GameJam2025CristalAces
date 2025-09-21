using Unity.VisualScripting;
using UnityEngine;

public class OnCollisionDoSpeech : MonoBehaviour
{
    [SerializeField] private SpeechData speech;

    private void OnCollisionEnter(Collision collision)
    {
        GameplayManager.Instance.Player.SpeechManager.SpeechQueue.TryPeek(out SpeechData currentSpeech);
        if (collision.gameObject == GameplayManager.Instance.Player.gameObject && currentSpeech != speech)
        {
            GameplayManager.Instance.Player.SpeechManager.StartSpeech(speech);
        }
    }
}
