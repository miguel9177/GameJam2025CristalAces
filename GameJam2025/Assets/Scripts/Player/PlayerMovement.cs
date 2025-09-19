using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        InputsManagers.Instance.OnSpaceKeyPressed += OnSpaceKeyPressed;
    }

    private void OnDestroy()
    {
        InputsManagers.Instance.OnSpaceKeyPressed -= OnSpaceKeyPressed;
    }

    private void Update()
    {
        MovePlayer();
    }

    #region Movement 

    private void MovePlayer()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        Vector2 input = InputsManagers.Instance.MoveAxis;
        Vector3 move = transform.right * input.x + transform.forward * input.y;

        rb.AddForce(move.normalized * moveSpeed, ForceMode.VelocityChange);
    }

    private void OnSpaceKeyPressed()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    #endregion

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
