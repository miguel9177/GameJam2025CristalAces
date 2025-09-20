using UnityEngine;

public class AI_PlayerManager : MonoBehaviour
{
    [SerializeField] private AI_PlayerMovement movement;

    public void MoveTo(Vector3 targetPosition, float speed)
    {
        movement.MoveTo(targetPosition, speed);
    }

    public bool ReachedTarget(float reachDistance)
    {
        return movement.ReachedTarget(reachDistance);
    }
}
