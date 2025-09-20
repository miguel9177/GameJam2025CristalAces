using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : PlayerMovementBase
{
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private float maxVelocity = 10f;
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float timeToDeactivateGravityAfterJump = 0.5f;
    public LayerMask groundMask;

    private bool isGrounded;
    private float yRotation = 0f;
    private float xRotation = 0f;
    private float initialGravity;

    private void Start()
    {     
        Rb.freezeRotation = true;
        initialGravity = gravity;
        InputsManagers.Instance.OnSpaceKeyPressed += OnSpaceKeyPressed;
    }

    private void OnDestroy()
    {
        InputsManagers.Instance.OnSpaceKeyPressed -= OnSpaceKeyPressed;
    }

    private void Update()
    {
        MovePlayer();
        ClampVelocity();
        RotatePlayer();
        ApplyGravity();
    }

    #region Movement 

    private void MovePlayer()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        Vector2 input = InputsManagers.Instance.MoveAxis;
        Vector3 move = transform.right * input.x + transform.forward * input.y;

        Rb.AddForce(move.normalized * moveSpeed, ForceMode.VelocityChange);
    }

    private void OnSpaceKeyPressed()
    {
        //NO JUMP
        return;
        if (isGrounded)
        {
            Rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            StartCoroutine(IE_ReduceGravity(timeToDeactivateGravityAfterJump));
        }
    }

    private void ClampVelocity()
    {
        Vector3 horizontalVel = new Vector3(Rb.linearVelocity.x, 0f, Rb.linearVelocity.z);

        if (horizontalVel.magnitude > maxVelocity)
        {
            Vector3 limitedVel = horizontalVel.normalized * maxVelocity;
            Rb.linearVelocity = new Vector3(limitedVel.x, Rb.linearVelocity.y, limitedVel.z);
        }
    }


    private void RotatePlayer()
    {
        Vector2 mouseDelta = InputsManagers.Instance.MouseDelta * mouseSensitivity * Time.deltaTime;

        yRotation += mouseDelta.x;
        Rb.MoveRotation(Quaternion.Euler(0f, yRotation, 0f));

        xRotation -= mouseDelta.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        Manager.PlayerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void ApplyGravity()
    {
        if (!isGrounded)
        {
            Rb.AddForce(Vector3.up * gravity, ForceMode.Acceleration);
        }
    }

    #endregion

    #region Modify Forces

    private bool reducingGravity = false;
    private IEnumerator IE_ReduceGravity(float time)
    {
        if (reducingGravity)
            yield break;

        reducingGravity = true;
        initialGravity = gravity;
        gravity = 0;
        yield return new WaitForSeconds(time);
        gravity = initialGravity;
        reducingGravity = false;
    }

    #endregion

    private void OnDrawGizmos()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
