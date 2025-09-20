using UnityEngine;

public class Ai_Checkpoint : MonoBehaviour
{
    [SerializeField] private float waitTime = 0f;
    [SerializeField] private float speed = 3f;

    private float currentWaitTime = 0;

    public float WaitTime => currentWaitTime;
    public float Speed => speed;

    private void Start()
    {
        currentWaitTime = waitTime;
    }

    public void ChangeWaitTime(float waitTime)
    {
        currentWaitTime = waitTime;
    }

    public void ResetWaitTime()
    {
        currentWaitTime = waitTime;
    }
}
