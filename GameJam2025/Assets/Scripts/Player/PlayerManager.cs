using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Camera playerCamera;

    public Camera PlayerCamera => playerCamera;
}
