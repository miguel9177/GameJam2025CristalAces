using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInteractablePauseTrafic : InteractableItemBase
{
    [SerializeField] List<Collider> collidersToDeactivate;
    [SerializeField] List<GameObject> gameObjectsToActivate;
    [SerializeField] TriggerLayerDetector triggerLayerDetector;
    [SerializeField] private List<Ai_Checkpoint> checkPointToPause;
    [SerializeField] private float timeToWait;

    private bool interacting = false;
    private bool playerInsideCrossroad = false;

    protected override void _Start()
    {
        base._Start();
        triggerLayerDetector.OnEnter += OnEnter;
        triggerLayerDetector.OnExit += OnExit;
    }

    protected override void _Destroy()
    {
        base._Destroy();
        triggerLayerDetector.OnEnter -= OnEnter;
        triggerLayerDetector.OnExit -= OnExit;
    }

    protected override void Interact()
    {
        base.Interact();

        if (interacting)
            return;

        interacting = true;

        for(int i = 0; i < checkPointToPause.Count; i++)
            checkPointToPause[i].ChangeWaitTime(timeToWait);

        for (int i = 0; i < collidersToDeactivate.Count; i++)
        {
            collidersToDeactivate[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < gameObjectsToActivate.Count; i++)
            gameObjectsToActivate[i].gameObject.SetActive(true);

        StartCoroutine(IE_WaitForSeconds());
    }

    private IEnumerator IE_WaitForSeconds()
    {
        yield return new WaitForSeconds(timeToWait);

        for (int i = 0; i < checkPointToPause.Count; i++)
            checkPointToPause[i].ResetWaitTime();

        interacting = false;
        if(playerInsideCrossroad == false)
        {
            for (int i = 0; i < collidersToDeactivate.Count; i++)
            {
                collidersToDeactivate[i].gameObject.SetActive(true);
            }
            for (int i = 0; i < gameObjectsToActivate.Count; i++)
                gameObjectsToActivate[i].gameObject.SetActive(false);
        }
    }

    #region Event Dependent Functions

    private void OnEnter(Collider collider)
    {
        playerInsideCrossroad = true;
        for(int i = 0; i < collidersToDeactivate.Count; i++)
        {
            collidersToDeactivate[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < gameObjectsToActivate.Count; i++)
            gameObjectsToActivate[i].gameObject.SetActive(true);
    }

    private void OnExit(Collider collider)
    {
        playerInsideCrossroad = false;
        for (int i = 0; i < collidersToDeactivate.Count; i++)
        {
            collidersToDeactivate[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < gameObjectsToActivate.Count; i++)
            gameObjectsToActivate[i].gameObject.SetActive(false);
    }

    #endregion
}
