using System;
using System.Collections.Generic;
using UnityEngine;

public class OnReachDogStartDogQuest : MonoBehaviour
{
    [SerializeField] TriggerLayerDetector layerDetector;
    [SerializeField] private List<GameObject> objectsToActivate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        layerDetector.OnEnter += OnEnter;
        layerDetector.OnExit += OnExit;
    }

    private void OnDestroy()
    {
        layerDetector.OnEnter -= OnEnter;
        layerDetector.OnExit -= OnExit;
    }

    private void OnEnter(Collider collider)
    {
        for(int i = 0; i < objectsToActivate.Count; i++)
        {
            objectsToActivate[i].gameObject.SetActive(true);
        }

        PlayerEvents.OnPlayerReachedDogPosition?.Invoke();

        Destroy(this.gameObject);
    }

    private void OnExit(Collider collider)
    {
        
    }
}
