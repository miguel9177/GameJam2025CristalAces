using System.Collections;
using UnityEngine;

public class Quest_WaitUntillBuyHotDog : PlayerQuestBase
{
    [SerializeField] InventoryItemData hotDog;

    private void Start()
    {
        StartCoroutine(IE_WaitUntillBuyHotDog());
    }

    private IEnumerator IE_WaitUntillBuyHotDog()
    {
        yield return new WaitUntil(() => GameplayManager.Instance.Player.InventoryManager.HasItem(hotDog));
        manager.FinishedQuest(this);
    }
}
