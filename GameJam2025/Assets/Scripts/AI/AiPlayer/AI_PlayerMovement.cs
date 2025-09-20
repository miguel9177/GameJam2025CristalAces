using UnityEngine;

public class AI_PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private Vector3 target;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform feet;

    private bool hasTarget = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    public void MoveTo(Vector3 targetTransform, float moveSpeed)
    {
        target = targetTransform;
        speed = moveSpeed;
        hasTarget = true;
    }

    public bool ReachedTarget(float reachDistance)
    {
        if (!hasTarget || target == null) return true;
        return Vector3.Distance(feet.position, target) <= reachDistance;
    }

    private void FixedUpdate()
    {
        if (!hasTarget || target == null) return;

        Vector3 direction = (target - rb.position).normalized;

        // aplicar for�a para mover
        rb.AddForce(direction * speed, ForceMode.Acceleration);

        // rodar suavemente para onde est� a andar
        if (rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z));
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }
}
