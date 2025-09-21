using System;
using UnityEngine;

public class InputsManagers : MonoBehaviour
{
    public static InputsManagers Instance { get; private set; }
    public Vector2 MoveAxis { get; private set; }
    public Vector2 MouseDelta { get; private set; }
    public float MouseScrollWheelY { get; private set; }
    public bool OnPressingLeftClickMouse { get; private set; }
    public bool OnPressingRightClickMouse { get; private set; }

    public Action OnActionKeyPressed;
    public Action OnSpaceKeyPressed;
    public Action OnMouseLeftClick;
    public Action OnMouseRightClick;
    public Action OnEscapeClick;

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
        ResetBoolVariables();

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        MoveAxis = new Vector2(x, y);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        MouseDelta = new Vector2(mouseX, mouseY);

        MouseScrollWheelY = Input.mouseScrollDelta.y;

        if (Input.GetKeyDown(KeyCode.E))
        {
            OnActionKeyPressed?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnSpaceKeyPressed?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscapeClick?.Invoke();
        }
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseLeftClick?.Invoke();
        }
        if (Input.GetMouseButton(0))
        {
            OnPressingLeftClickMouse = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            OnMouseRightClick?.Invoke();
        }
        if(Input.GetMouseButton(1))
        {
            OnPressingRightClickMouse = true;
        }
    }

    private void ResetBoolVariables()
    {
        OnPressingLeftClickMouse = false;
        OnPressingRightClickMouse = false;
    }
}
