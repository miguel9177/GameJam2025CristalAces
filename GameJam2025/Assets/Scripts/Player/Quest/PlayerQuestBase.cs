using System;
using UnityEngine;

public enum QuestState
{
    Inactive,
    Active,
    Completed
}

public class PlayerQuestBase : MonoBehaviour
{
    [SerializeField] protected string questText;

    protected PlayerQuestManager manager;
    protected QuestState state = QuestState.Inactive;

    public string QuestText => questText;
    public QuestState State => state;

    private void Start()
    {
        _Start();
    }

    private void OnDestroy()
    {
        _Destroy();
    }
    protected virtual void _Start()
    {
        
    }

    protected virtual void _Destroy()
    {
        
    }

    public virtual void Initialize(PlayerQuestManager manager)
    {
        this.manager = manager;
        state = QuestState.Active;
    }

    protected virtual void FinishQuest()
    {
        state = QuestState.Completed;
        manager.FinishedQuest(this);
    }
}
