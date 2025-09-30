using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [NonSerialized] public static InputManager instance;

    [NonSerialized] private InputAction moveAction;
    [NonSerialized] private InputAction interactAction;

    public Vector2 MoveVector => moveAction.ReadValue<Vector2>(); // [x, z] floats
    public bool InteractBool => interactAction.WasPressedThisFrame();

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;

        moveAction = InputSystem.actions.FindAction("Move");
        interactAction = InputSystem.actions.FindAction("Interact");
    }

    void Update()
    {
        
    }
}
