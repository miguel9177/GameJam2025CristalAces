using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractableItemBase : MonoBehaviour
{
    private bool playerInsideTrigger = false;

    [SerializeField] private Collider myCollider;
    [SerializeField] private LayerMask interactableLayers;
    [Header("NULLABLE VARIABLES")]
    [SerializeField] private AudioPlayer audioPlayer;
    [SerializeField] private AudioData soundOnInteract;

    public bool PlayerInsideTrigger => playerInsideTrigger;

    private void Start()
    {
        _Start();
    }

    private void OnDestroy()
    {
        _Destroy();
        
    }

    protected virtual void _Start()
    {
        InputsManagers.Instance.OnActionKeyPressed += TryInteract;
    }

    protected virtual void _Destroy()
    {
        if (InputsManagers.Instance != null)
            InputsManagers.Instance.OnActionKeyPressed -= TryInteract;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameplayManager.Instance.Player.gameObject)
        {
            playerInsideTrigger = true;
            bool canInteract = CanInteract();
            GameplayManager.Instance.Player.OnEnterTriggerOfInteractable(this, canInteract);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameplayManager.Instance.Player.gameObject)
        {
            playerInsideTrigger = false;
            bool canInteract = CanInteract();
            GameplayManager.Instance.Player.OnExitTriggerOfInteractable(this, canInteract);
        }
    }

    private void TryInteract()
    {
        if (!playerInsideTrigger) return;

        if (CanInteract())
        {
            Interact();
        }
    }

    public virtual bool CanInteract()
    {
        if (!playerInsideTrigger) return false;

        Camera cam = GameplayManager.Instance.Player.PlayerCamera;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, interactableLayers, QueryTriggerInteraction.Ignore))
        {
            return hit.collider != null && hit.collider.gameObject == myCollider.gameObject;
        }

        return false;
    }

    public void ForceCallInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {
        if (audioPlayer != null && soundOnInteract)
            audioPlayer.PlayAudio(soundOnInteract);
        Debug.Log($"{gameObject.name} foi interagido!");
    }
}
