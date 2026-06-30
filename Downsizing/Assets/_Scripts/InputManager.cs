using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private PlayerInput playerInput;
    [SerializeField] private Vector2 pointerPositionScreenSpace;
    [SerializeField] private Vector3 lookDelta;
    [SerializeField] private Vector3 moveDelta;

    public static event Action onPrimaryDown;
    public static event Action onPrimaryUp;
    public static event Action<Vector2> onLook;
    public static event Action<Vector2> onMove;

    protected override void Awake()
    {
        base.Awake();
        if(playerInput == null) playerInput = GetComponent<PlayerInput>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!playerInput.camera)
            playerInput.camera = Camera.main;

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 pos = context.ReadValue<Vector2>();
        moveDelta = pos;
        onMove?.Invoke(moveDelta);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 pos = context.ReadValue<Vector2>();
        lookDelta = pos;
        onLook?.Invoke(lookDelta);

    }

    public void OnPrimaryDown(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        onPrimaryDown?.Invoke();
    }
    
    public void OnPrimaryUp(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        onPrimaryUp?.Invoke();
    }

    public void OnPointerPosition(InputAction.CallbackContext context)
    {
        Vector2 pos = context.ReadValue<Vector2>();
        pos = new Vector2(Mathf.Clamp(pos.x / Screen.width, 0f, 1f), Mathf.Clamp(pos.y / Screen.height, 0f, 1f));
        pointerPositionScreenSpace = pos;

    }


}
