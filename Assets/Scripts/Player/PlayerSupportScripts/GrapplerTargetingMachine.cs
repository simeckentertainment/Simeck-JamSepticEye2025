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
    public Vector2 ScreenSpaceGrappleTarget;
    RaycastHit2D distChecker;
    float RayLength;



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

        if (input.controlType == InputDriver.ControlType.KBM)
        {
            FinalAimAngle = GetMouseAimAngle();
        }
        else
        {
            FinalAimAngle = input.gamepadAimVal; //Should be between -1 and 1.
        }
    }
    Vector2 GetMouseAimAngle()
    {
        return Helper.AngleOfTwoPoints(ScreenLaunchPos, MousePos);
    }

    public Vector2 GetWorldSpaceGrappleTargetPoint()
    {
        Vector2 returnVal = new Vector2();
        distChecker = Physics2D.Raycast(WorldLaunchPos, FinalAimAngle, player.GrapplerDist);
        if (distChecker.collider != null)
        {
            RayLength = Vector2.Distance(distChecker.point, WorldLaunchPos);
            Debug.DrawRay(WorldLaunchPos, FinalAimAngle * RayLength, Color.red);
            returnVal = distChecker.point;

        }
        else
        {
            Debug.DrawRay(WorldLaunchPos, FinalAimAngle * player.GrapplerDist, Color.red);
            returnVal = WorldLaunchPos + (FinalAimAngle * player.GrapplerDist);
        }
        return returnVal;
    }


}
