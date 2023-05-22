using UnityEngine;
using System.Collections;
using System;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour {
    public static PlayerInputManager Instance { private set; get; }
    public Vector2 Move => playerInputActions.Player.Move.ReadValue<Vector2>();
    public event Action OnInteractPerform;
    public event Action OnInteractAlternatePerform;

    private PlayerInputActions playerInputActions;
    private void Awake() {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    private void OnEnable() {
        playerInputActions.Player.Interact.performed += _InteractPerformed;
        playerInputActions.Player.InteractAlternate.performed += _InteractAlternatePerformed;
    }

    private void OnDisable() {
        playerInputActions.Player.Interact.performed -= _InteractPerformed;
        playerInputActions.Player.InteractAlternate.performed -= _InteractAlternatePerformed;
    }

    private void _InteractPerformed(InputAction.CallbackContext obj) {
        OnInteractPerform?.Invoke();
    }
    private void _InteractAlternatePerformed(InputAction.CallbackContext obj) {
        OnInteractAlternatePerform?.Invoke();
    }
}

