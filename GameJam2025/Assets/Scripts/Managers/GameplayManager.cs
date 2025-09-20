using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance { get; private set; }

    [SerializeField] PlayerManager player;

    public static readonly string PlayerTag = "Player";
    public PlayerManager Player => player;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // trava o cursor no centro
        Cursor.visible = false;
    }
}
