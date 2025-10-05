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
    public Vector2 WorldSpaceGrappleTarget;
    RaycastHit2D distChecker;
    float RayLength;

    public bool validTarget;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        validTarget = false;
        resolutionMaxX = Screen.width;
        resolutionMaxY = Screen.height;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!player.input.harpoonPressed)
        {
            MousePos = input.mousePositionVal; //Should be between 0x0 and resolution max;
        }
        Vector2 ScreenSpacePlayerPos = Camera.main.WorldToScreenPoint(player.transform.position);



        if (input.controlType == InputDriver.ControlType.KBM)
        {
            if (MousePos.x >= ScreenSpacePlayerPos.x)
            {
                WorldLaunchPos = player.RightHookshotLaunchPoint.transform.position;
            }
            if (MousePos.x < ScreenSpacePlayerPos.x)
            {
                WorldLaunchPos = player.LeftHookshotLaunchPoint.transform.position;
            }
            if (!player.input.harpoonPressed)
            {
                FinalAimAngle = GetMouseAimAngle();
            }
        }
            else
            {
                if (input.gamepadAimVal.x > 0.0f)
                {
                    WorldLaunchPos = player.RightHookshotLaunchPoint.transform.position;
                }
                if (input.gamepadAimVal.x < 0.0f)
                {
                    WorldLaunchPos = player.LeftHookshotLaunchPoint.transform.position;
                }
                FinalAimAngle = input.gamepadAimVal; //Should be between -1 and 1.
                if (input.gamepadAimVal.x == 0.0f)
                {
                    if (player.facingDirection == Player.FacingDirection.Left)
                    {
                        WorldLaunchPos = player.LeftHookshotLaunchPoint.transform.position;
                    }
                    else
                    {
                        WorldLaunchPos = player.RightHookshotLaunchPoint.transform.position;
                    }
                }
            }
        //Now we have the launch position for the grappling hook.
        ScreenLaunchPos = Camera.main.WorldToScreenPoint(WorldLaunchPos);
        if (!player.input.harpoonPressed)
        {
            WorldSpaceGrappleTarget = GetWorldSpaceGrappleTargetPoint();
            ScreenSpaceGrappleTarget = Camera.main.WorldToScreenPoint(WorldSpaceGrappleTarget);
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

            if (distChecker.collider.gameObject.tag == "Ground")
            {
                validTarget = true;
            }
            else
            {
                validTarget = false;
            }

        }
        else
        {
            Debug.DrawRay(WorldLaunchPos, FinalAimAngle * player.GrapplerDist, Color.red);
            returnVal = WorldLaunchPos + (FinalAimAngle * player.GrapplerDist);
            validTarget = false;
        }


        return returnVal;
    }


}
