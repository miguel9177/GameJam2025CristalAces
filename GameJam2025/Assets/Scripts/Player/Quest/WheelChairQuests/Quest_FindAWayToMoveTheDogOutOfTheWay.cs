using System;
using UnityEngine;

public class Quest_FindAWayToMoveTheDogOutOfTheWay : PlayerQuestBase
{
    protected override void _Start()
    {
        base._Start();
        PlayerEvents.OnPlayerEndedDogAction += OnPlayerEndedDogAction;
    }

    protected override void _Destroy()
    {
        base._Destroy();
        PlayerEvents.OnPlayerEndedDogAction -= OnPlayerEndedDogAction;
    }

    private void OnPlayerEndedDogAction()
    {
        manager.FinishedQuest(this);
    }
}
