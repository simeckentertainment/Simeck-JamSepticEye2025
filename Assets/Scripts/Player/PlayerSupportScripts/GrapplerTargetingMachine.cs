using UnityEngine;

public class GrapplerTargetingMachine : MonoBehaviour
{
    public bool hooked;
    Vector2 MousePos;
    int resolutionMaxX;
    int resolutionMaxY;
    Vector2 RightStickAngle;
    Vector2 WorldLaunchPos;
    Vector2 ScreenLaunchPos;

    [SerializeField] InputDriver input;
    [SerializeField] Player player;
    public Vector2 FinalAimAngle;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resolutionMaxX = Screen.width;
        resolutionMaxY = Screen.height;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.facingDirection == Player.FacingDirection.Left)
        {
            WorldLaunchPos = player.LeftHookshotLaunchPoint.transform.position;
        }
        else
        {
            WorldLaunchPos = player.RightHookshotLaunchPoint.transform.position;
        }
        //Now we have the launch position for the grappling hook.
        ScreenLaunchPos = Camera.main.WorldToScreenPoint(WorldLaunchPos);

        MousePos = input.mousePositionVal; //Should be between 0x0 and resolution max;
        RightStickAngle = input.gamepadAimVal; //Should be between -1 and 1.


    }





    Vector2 GetMouseAimAngle()
    {


        return Vector2.zero;
    }

    Vector2 GetGamepadAimAngle()
    {


        return Vector2.zero;
    }
}
