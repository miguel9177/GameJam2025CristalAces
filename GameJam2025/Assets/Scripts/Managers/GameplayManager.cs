using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance { get; private set; }

    [SerializeField] List<PlayerManager> players;

    public static readonly string PlayerTag = "Player";
    public PlayerManager Player 
    { 
        get 
        { 
            for(int i = 0; i < players.Count; i++)
            {
                if (players[i].gameObject.activeInHierarchy == true)
                    return players[i];
            }

            return players[0];
        } 
    }
        

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
