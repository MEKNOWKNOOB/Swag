using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [NonSerialized] private InputAction moveAction;
    [NonSerialized] private InputAction interactAction;
    [NonSerialized] private InputAction debugAction;
    [NonSerialized] private InputAction sprintAction;

    public Vector2 MoveVector => moveAction.ReadValue<Vector2>(); // [x, z] floats
    public bool InteractBool => interactAction.WasPressedThisFrame();
    public bool DebugBool => debugAction.WasPressedThisFrame();

    public bool sprintBool => sprintAction.WasPressedThisFrame();
    public bool sprintRelease => sprintAction.WasReleasedThisFrame();

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        moveAction = InputSystem.actions.FindAction("Move");
        interactAction = InputSystem.actions.FindAction("Interact");
        debugAction = InputSystem.actions.FindAction("Debug");
        sprintAction = InputSystem.actions.FindAction("Sprint");
    }

    void Update()
    {
        
    }
}
