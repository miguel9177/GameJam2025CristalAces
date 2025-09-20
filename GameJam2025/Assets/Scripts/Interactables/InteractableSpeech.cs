using UnityEngine;

public class InteractableSpeech : InteractableItemBase
{
    [SerializeField] private SpeechData speechData;

    protected override void Interact()
    {
        base.Interact();

        if(GameplayManager.Instance.Player.SpeechManager.IsActive == false)
            GameplayManager.Instance.Player.SpeechManager.StartSpeech(speechData);
    }

    public override bool CanInteract()
    {
        if (PlayerInsideTrigger)
            return true;
        else
            return false;
        //return base.CanInteract();
    }
}
