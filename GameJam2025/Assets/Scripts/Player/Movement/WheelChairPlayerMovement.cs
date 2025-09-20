using System.Collections;
using UnityEngine;

public class WheelChairPlayerMovement : PlayerMovementBase
{
    [Header("Wheelchair Settings")]
    [SerializeField] private float pushForce = 10f;        
    [SerializeField] private float pushCooldown = 2f;      
    [SerializeField] private float friction = 2f;
    [SerializeField] private float turnTime = 0.5f;
    [SerializeField] private float turnAmount = 0.5f;
    [SerializeField] private AnimationCurve turnCurve;

    [Header("Camera Settings")]
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float minX = -90f;
    [SerializeField] private float maxX = 90f;
    [SerializeField] private float minY = -90f;
    [SerializeField] private float maxY = 90f;

    private float yawRotation = 0f;
    private float xRotation = 0f;
    private float lastPushTime = -999f;
    private float rotationInput = 0f;
    private bool isPushing = false;
    private Coroutine decelerateRoutine;
    private Coroutine turningRoutine;

    private void Start()
    {
        Rb.freezeRotation = true;
    }

    private void Update()
    {
        HandlePushInput();
        HandleCameraRotation();
    }

    private void FixedUpdate()
    {
        ApplyFriction();
    }

    #region Push System

    private void HandlePushInput()
    {
        if (!CanPush())
            return;

        float scrollY = InputsManagers.Instance.MouseScrollWheelY;

        if (Mathf.Abs(scrollY) > 0.01f)
        {
            lastPushTime = Time.time;

        
            Vector3 direction = transform.forward * Mathf.Sign(scrollY);
            Rb.AddForce(direction * pushForce, ForceMode.VelocityChange);

            if (InputsManagers.Instance.OnPressingLeftClickMouse)
            {
                float sign = (scrollY > 0) ? -1f : 1f;
                StartTurning(sign * turnAmount);
            }
            else if (InputsManagers.Instance.OnPressingRightClickMouse)
            {
                float sign = (scrollY > 0) ? 1f : -1f;
                StartTurning(sign * turnAmount);
            }
            else
                rotationInput = 0f;

            if (decelerateRoutine != null) StopCoroutine(decelerateRoutine);
            decelerateRoutine = StartCoroutine(DecelerateOverTime());
        }
    }

    private void StartTurning(float deltaYaw)
    {
        if (turningRoutine != null) StopCoroutine(turningRoutine);
        turningRoutine = StartCoroutine(TurnOverTime(deltaYaw));
    }

    private IEnumerator TurnOverTime(float deltaYaw)
    {
        float elapsed = 0f;
        float startYaw = transform.eulerAngles.y;
        float targetYaw = startYaw + deltaYaw;

        while (elapsed < turnTime)
        {
            elapsed += Time.fixedDeltaTime;
            float t = Mathf.Clamp01(elapsed / turnTime);
            float newYaw = Mathf.LerpAngle(startYaw, targetYaw, turnCurve.Evaluate(t));
            Rb.MoveRotation(Quaternion.Euler(0f, newYaw, 0f));
            yield return new WaitForFixedUpdate();
        }

        Rb.MoveRotation(Quaternion.Euler(0f, targetYaw, 0f));
    }

    #endregion

    #region Camera Rotation

    private void HandleCameraRotation()
    {
        Vector2 mouseDelta = InputsManagers.Instance.MouseDelta * mouseSensitivity * Time.deltaTime;

        // controla yaw (horizontal)
        yawRotation += mouseDelta.x;

        // controla pitch (vertical)
        xRotation -= mouseDelta.y;
        xRotation = Mathf.Clamp(xRotation, minX, maxX);
        yawRotation = Mathf.Clamp(yawRotation, minY, maxY);

        // aplica a rotação completa na câmara
        Manager.PlayerCamera.transform.localRotation = Quaternion.Euler(xRotation, yawRotation, 0f);
    }

    #endregion

    #region Frictions

    private void ApplyFriction()
    {
        if (!isPushing && Rb.linearVelocity.magnitude > 0.1f)
        {
            Rb.linearVelocity = Vector3.Lerp(Rb.linearVelocity, Vector3.zero, friction * Time.fixedDeltaTime);
        }
    }

    private IEnumerator DecelerateOverTime()
    {
        isPushing = true;
        yield return new WaitForSeconds(0.1f);

        while (Rb.linearVelocity.magnitude > 0.1f)
        {
            Rb.linearVelocity = Vector3.Lerp(Rb.linearVelocity, Vector3.zero, friction * Time.deltaTime);
            yield return null;
        }

        Rb.linearVelocity = Vector3.zero;
        isPushing = false;
    }


    #endregion

    #region Helper Functions

    private bool CanPush()
    {
        return Time.time > lastPushTime + pushCooldown;
    }

    #endregion
}
