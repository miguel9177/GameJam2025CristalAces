using System;
using UnityEngine;

public class InputsManagers : MonoBehaviour
{
    public static InputsManagers Instance { get; private set; }
    public Vector2 MoveAxis { get; private set; }
    public Action OnActionKeyPressed;
    public Action OnSpaceKeyPressed;

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

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        MoveAxis = new Vector2(x, y);

        if (Input.GetKeyDown(KeyCode.E))
        {
            OnActionKeyPressed?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnSpaceKeyPressed?.Invoke();
        }
    }
}
