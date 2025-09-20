using System;
using UnityEngine;

public class Quest_PlayerReachMetro : PlayerQuestBase
{
    protected override void _Start()
    {
        base._Start();
        PlayerEvents.OnPlayerReachedMetro += OnPlayerReachedMetro;
    }

    protected override void _Destroy()
    {
        base._Destroy();
        PlayerEvents.OnPlayerReachedMetro -= OnPlayerReachedMetro;
    }

    private void OnPlayerReachedMetro()
    {
        manager.FinishedQuest(this);
    }
}

