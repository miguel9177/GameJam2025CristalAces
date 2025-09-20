using UnityEngine;

public class Ai_Checkpoint : MonoBehaviour
{
    [SerializeField] private float waitTime = 0f;
    [SerializeField] private float speed = 3f;

    public float WaitTime => waitTime;
    public float Speed => speed;
}
