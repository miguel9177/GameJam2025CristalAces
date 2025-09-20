using UnityEngine;

public abstract class PlayerMovementBase : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] PlayerManager manager;

    public Rigidbody Rb => rb;
    public PlayerManager Manager => manager;
}
