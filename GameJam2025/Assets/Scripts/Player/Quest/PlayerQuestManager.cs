using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerQuestManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private GameObject parentOfQuestUI;
    [SerializeField] private TextMeshProUGUI txtText;
    [SerializeField] private List<PlayerQuestBase> allQuests;

    private Queue<PlayerQuestBase> questQueue = new Queue<PlayerQuestBase>();
    private PlayerQuestBase currentQuest;

    private void Start()
    {
        for(int i = 0; i < allQuests.Count; i++)
            questQueue.Enqueue(allQuests[i]);
        StartNextQuest();    
    }

    private void StartNextQuest()
    {
        if (questQueue.Count == 0)
        {
            currentQuest = null;
            parentOfQuestUI.SetActive(false);
            return;
        }

        currentQuest = questQueue.Dequeue();
        currentQuest.Initialize(this);

        parentOfQuestUI.SetActive(true);
        txtText.text = currentQuest.QuestText;
    }

    public void FinishedQuest(PlayerQuestBase quest)
    {
        if (quest == currentQuest)
        {
            StartNextQuest();
        }
    }
}
