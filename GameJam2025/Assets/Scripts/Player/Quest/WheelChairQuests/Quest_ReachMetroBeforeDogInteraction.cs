using UnityEngine;

public class Quest_ReachMetroBeforeDogInteraction : PlayerQuestBase
{
    protected override void _Start()
    {
        base._Start();
        PlayerEvents.OnPlayerReachedDogPosition += OnPlayerReachedDogPosition;
    }

    protected override void _Destroy()
    {
        base._Destroy();
        PlayerEvents.OnPlayerReachedMetro -= OnPlayerReachedDogPosition;
    }

    private void OnPlayerReachedDogPosition()
    {
        manager.FinishedQuest(this);
    }
}
