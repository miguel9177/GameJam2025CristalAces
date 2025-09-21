using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTriggerEnterChangeScene : MonoBehaviour
{
    [SerializeField] TriggerLayerDetector triggerLayer;

    [SerializeField] string sceneToChangeTo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        triggerLayer.OnEnter += OnEnter;
        triggerLayer.OnExit += OnExit;
    }

    private void OnDestroy()
    {
        triggerLayer.OnEnter -= OnEnter;
        triggerLayer.OnExit -= OnExit;
    }

    private void OnEnter(Collider collider)
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }

    private void OnExit(Collider collider)
    {
        
    }
}
