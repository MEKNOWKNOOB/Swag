using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [NonSerialized] private InputAction moveAction;
    [NonSerialized] private InputAction interactAction;
    [NonSerialized] private InputAction attackAction;
    [NonSerialized] private InputAction debugAction;
    [NonSerialized] private InputAction sprintAction;

    [NonSerialized] private InputAction hotbar1Action;
    [NonSerialized] private InputAction hotbar2Action;
    [NonSerialized] private InputAction hotbar3Action;

    public Vector2 MoveVector => moveAction.ReadValue<Vector2>(); // [x, z] floats
    public bool InteractBool => interactAction.WasPressedThisFrame();
    public bool AttackBool => attackAction.WasPressedThisFrame();
    public bool DebugBool => debugAction.WasPressedThisFrame();

    public bool sprintBool => sprintAction.WasPressedThisFrame();
    public bool sprintRelease => sprintAction.WasReleasedThisFrame();

    public bool Hotbar1Bool => hotbar1Action.WasPressedThisFrame();
    public bool Hotbar2Bool => hotbar2Action.WasPressedThisFrame();
    public bool Hotbar3Bool => hotbar3Action.WasPressedThisFrame();

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
        attackAction = InputSystem.actions.FindAction("Attack");
        debugAction = InputSystem.actions.FindAction("Debug");
        sprintAction = InputSystem.actions.FindAction("Sprint");

        hotbar1Action = InputSystem.actions.FindAction("Hotbar1");
        hotbar2Action = InputSystem.actions.FindAction("Hotbar2");
        hotbar3Action = InputSystem.actions.FindAction("Hotbar3");
    }

    void Update()
    {
        
    }
}
