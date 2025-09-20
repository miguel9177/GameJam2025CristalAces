using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerMovementBase playerMovement;
    [SerializeField] Camera playerCamera;

    public Camera PlayerCamera => playerCamera;
}
