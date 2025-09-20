using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AI_PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private Transform feet;
    [SerializeField] private Rigidbody rb;

    private float acceleration = 3f;
    private Vector3 targetPosition;
    private bool hasTarget = false;

    private void Awake()
    {
        rb.freezeRotation = true;
    }

    public void MoveTo(Vector3 newTarget, float moveSpeed)
    {
        targetPosition = newTarget;
        acceleration = moveSpeed;
        hasTarget = true;
        MoveAI();
    }

    public void StopMovement()
    {
        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
        StartCoroutine(HelperFunctionsUtility.IE_WaitForFrames(() => rb.isKinematic = false, 1));
    }

    public bool ReachedTarget(float reachDistance)
    {
        if (!hasTarget) return true;
        return Vector3.Distance(feet.position, targetPosition) <= reachDistance;
    }

    private void MoveAI()
    {
        if (!hasTarget) return;

        Vector3 targetPos = new Vector3(targetPosition.x, rb.position.y, targetPosition.z);
        Vector3 ourPos = rb.position;
        Vector3 direction = (targetPos - ourPos).normalized;

        rb.AddForce(direction * acceleration * Time.fixedDeltaTime, ForceMode.Acceleration);

        Vector3 horizontalVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (horizontalVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = horizontalVel.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }

        if (horizontalVel.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(horizontalVel);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }
}
