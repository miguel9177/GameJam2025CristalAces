using System;
using UnityEngine;

public class AI_PlayerManager : MonoBehaviour
{
    [SerializeField] private AI_PlayerMovement movement;
    [SerializeField] private TriggerLayerDetector triggerLayerDetector;

    private bool collidingWithPlayer = false;

    #region Initialize

    private void Start()
    {
        triggerLayerDetector.OnEnter += OnEnteredPlayerLayer;
        triggerLayerDetector.OnExit += OnExitPlayerLayer;
    }

    private void OnDestroy()
    {
        triggerLayerDetector.OnEnter -= OnEnteredPlayerLayer;
        triggerLayerDetector.OnExit -= OnExitPlayerLayer;
    }

    #endregion

    #region Event Dependent Functions

    private void OnEnteredPlayerLayer(Collider collider)
    {
        collidingWithPlayer = true;
    }

    private void OnExitPlayerLayer(Collider collider)
    {
        collidingWithPlayer = false;
    }

    #endregion

    #region Public Functions

    public void MoveTo(Vector3 targetPosition, float speed)
    {
        if (collidingWithPlayer)
            return;

        movement.MoveTo(targetPosition, speed);
    }

    public bool ReachedTarget(float reachDistance)
    {
        return movement.ReachedTarget(reachDistance);
    }

    #endregion
}
