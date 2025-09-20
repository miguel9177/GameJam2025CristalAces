using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_CheckpointsController : MonoBehaviour
{
    [SerializeField] private AI_PlayerManager aiPlayer;
    [SerializeField] private List<Ai_Checkpoint> ourCheckpoints = new List<Ai_Checkpoint>();
    [SerializeField] private float reachDistance = 0.2f;
    [SerializeField] private bool loop = true;
    [SerializeField] private bool teleportToStartPos = false;

    private int currentIndex = 0;
    private bool isMoving = true;
    private bool isWaiting = false;

    private void FixedUpdate()
    {
        if (!isMoving || isWaiting || ourCheckpoints.Count == 0) return;

        MovePlayer();
    }

    private void MovePlayer()
    {
        Ai_Checkpoint checkpoint = ourCheckpoints[currentIndex];
        aiPlayer.MoveTo(checkpoint.transform.position, checkpoint.Speed);

        if (aiPlayer.ReachedTarget(reachDistance))
        {
            StartCoroutine(WaitAtCheckpoint(checkpoint.WaitTime));
            NextCheckpoint();
        }
    }

    private void NextCheckpoint()
    {
        currentIndex++;
        if (currentIndex >= ourCheckpoints.Count)
        {
            if (loop)
                currentIndex = 0;
            else if (teleportToStartPos)
            {
                currentIndex = 0;
                aiPlayer.TeleportToPosition(ourCheckpoints[0].transform);
            }
            else
            {
                isMoving = false;
                aiPlayer.NoMoreTargets();
            }
        }
    }

    private IEnumerator WaitAtCheckpoint(float seconds)
    {
        if (seconds > 0)
        {
            isWaiting = true;
            aiPlayer.Movement.StopMovement();
            yield return new WaitForSeconds(seconds);
            isWaiting = false;
        }
    }
}
