using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_PlayerManager : MonoBehaviour
{
    [SerializeField] private AI_PlayerMovement movement;
    [SerializeField] private Ai_PlayerAnimatorController animatorController;
    [SerializeField] private List<TriggerLayerDetector> triggersLayerDetector;
    [SerializeField] private Vector2 timeToWaitToMoveAgainAfterCollidingWithPlayer = new Vector2(0.8f, 2.5f);
    [SerializeField] private string startWithSpecificAnimation = "";
    private bool collidingWithPlayer = false;
    private bool blockMovement = false;
    private Coroutine stopCollidingWithPlayerCoroutine = null;

    public AI_PlayerMovement Movement => movement;

    #region Initialize

    private void Start()
    {
        for(int i = 0; i < triggersLayerDetector.Count; i++)
        {
            triggersLayerDetector[i].OnEnter += OnEnteredPlayerLayer;
            triggersLayerDetector[i].OnExit += OnExitPlayerLayer;
        }

        if (string.IsNullOrEmpty(startWithSpecificAnimation) == false)
            animatorController?.StartSpecificAnimation(startWithSpecificAnimation);
    }

    private void OnDestroy()
    {
        for(int i = 0; i < triggersLayerDetector.Count; i++)
        {
            triggersLayerDetector[i].OnEnter -= OnEnteredPlayerLayer;
            triggersLayerDetector[i].OnExit -= OnExitPlayerLayer;
        }
    }

    #endregion

    #region Event Dependent Functions

    private void OnEnteredPlayerLayer(Collider collider)
    {
        if(stopCollidingWithPlayerCoroutine != null)
        {
            StopCoroutine(stopCollidingWithPlayerCoroutine);
            stopCollidingWithPlayerCoroutine = null;
        }

        if(movement.HasTarget)
            animatorController?.StartIdleAnimation();

        collidingWithPlayer = true;
        movement.StopMovement();

    }

    private void OnExitPlayerLayer(Collider collider)
    {
        if(stopCollidingWithPlayerCoroutine == null)
            stopCollidingWithPlayerCoroutine = StartCoroutine(IE_WaitForSecondsBeforeStoppingCollidingWithPlayer(UnityEngine.Random.Range(timeToWaitToMoveAgainAfterCollidingWithPlayer.x, timeToWaitToMoveAgainAfterCollidingWithPlayer.y)));
    }

    #endregion

    #region Public Functions

    public void MoveTo(Vector3 targetPosition, float speed)
    {
        if (collidingWithPlayer || blockMovement)
            return;

        animatorController?.StartWalkingAnimation();
        movement.MoveTo(targetPosition, speed);
    }

    public void NoMoreTargets()
    {
        animatorController?.StartIdleAnimation();
        movement.NoMoreTargets();
    }

    public bool ReachedTarget(float reachDistance)
    {
        return movement.ReachedTarget(reachDistance);
    }

    public void TeleportToPosition(Transform pos)
    {
        this.transform.position = pos.position;
    }

    public void BlockOrUnlockMovement(bool lockIt)
    {
        blockMovement = lockIt;
    }

    #endregion

    #region Stop Movement Functions

    private IEnumerator IE_WaitForSecondsBeforeStoppingCollidingWithPlayer(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        collidingWithPlayer = false;
        stopCollidingWithPlayerCoroutine = null;
    }

    #endregion
}
