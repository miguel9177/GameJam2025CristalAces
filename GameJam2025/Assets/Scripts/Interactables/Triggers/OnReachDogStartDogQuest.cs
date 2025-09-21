using System;
using System.Collections.Generic;
using UnityEngine;

public class OnReachDogStartDogQuest : MonoBehaviour
{
    //[SerializeField] TriggerLayerDetector layerDetector;
    [SerializeField] private List<GameObject> objectsToActivate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerEvents.OnPlayerInteractedWithDog += OnPlayerInteractedWithDog;
    }

    private void OnDestroy()
    {
        PlayerEvents.OnPlayerInteractedWithDog -= OnPlayerInteractedWithDog;
    }

    private void OnPlayerInteractedWithDog()
    {
        for (int i = 0; i < objectsToActivate.Count; i++)
        {
            objectsToActivate[i].gameObject.SetActive(true);
        }

        PlayerEvents.OnPlayerReachedDogPosition?.Invoke();

        Destroy(this.gameObject);
    }
}
