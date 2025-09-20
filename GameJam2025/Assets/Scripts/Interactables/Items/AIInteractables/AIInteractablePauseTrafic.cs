using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInteractablePauseTrafic : InteractableItemBase
{
    [SerializeField] private List<Ai_Checkpoint> checkPointToPause;
    [SerializeField] private float timeToWait;

    private bool interacting = false;

    protected override void Interact()
    {
        base.Interact();

        if (interacting)
            return;

        interacting = true;

        for(int i = 0; i < checkPointToPause.Count; i++)
            checkPointToPause[i].ChangeWaitTime(timeToWait);

        StartCoroutine(IE_WaitForSeconds());
    }

    private IEnumerator IE_WaitForSeconds()
    {
        yield return new WaitForSeconds(timeToWait);

        for (int i = 0; i < checkPointToPause.Count; i++)
            checkPointToPause[i].ResetWaitTime();

        interacting = false;
    }
}
