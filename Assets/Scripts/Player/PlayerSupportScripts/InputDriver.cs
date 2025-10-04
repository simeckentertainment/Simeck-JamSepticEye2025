
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDriver : MonoBehaviour
{
    public InputAction moveAction;
    public Vector2 moveVal;
    public InputAction aimAction;
    public Vector2 gamepadAimVal;
    public Vector2 mousePositionVal;
    public InputAction harpoonAction;
    public bool harpoonPressed;
    public InputAction jumpAction;
    public bool jumpPressed;
    public InputControl currentControl;
    public ControlType controlType;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction.Enable();
        aimAction.Enable();
        harpoonAction.Enable();
        jumpAction.Enable();
    }

    void FixedUpdate()
    {

        moveVal = moveAction.ReadValue<Vector2>();
        RunAim();
        harpoonPressed = harpoonAction.ReadValue<float>() >= 0.5f;
        jumpPressed = jumpAction.ReadValue<float>() == 1.0f;

    }

    private void RunAim()
    {
        if (aimAction.activeControl is Gamepad)
        {
            controlType = ControlType.Gamepad;
            gamepadAimVal = aimAction.ReadValue<Vector2>();
            mousePositionVal = Vector2.zero;
        } else {
            controlType = ControlType.KBM;
            mousePositionVal = aimAction.ReadValue<Vector2>();
            gamepadAimVal = Vector2.zero;
        }

        
    }
    public enum ControlType{ KBM, Gamepad};
}
