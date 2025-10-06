using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireGrappleState : PlayerAliveState
{
    public PlayerFireGrappleState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }

    Vector2 pushDirection;
    public override void enter()
    {
        player.sfx.PlayOneShot(player.FireGrappler);
        player.ShowHook();
        if (player.GTM.validTarget)
        {
            player.rb.linearVelocity = Vector2.zero;
            SetSuccessSprite();
        }
        else
        { //change state. This acts as a bypass.
            SetFailSprite();
            DetermineStateChange();
        }

        base.enter();
    }

    public override void Update()
    {
        base.Update();
    }
    public override void exit()
    {
        player.HideHook();
        base.exit();
    }
    public override void FixedUpdate()
    {
        if (durationOfState < 2)
        {
            player.SetHookPos(Helper.Midpoint((Vector2)player.transform.position, player.GTM.WorldSpaceGrappleTarget));
            
        }
        else
        {
            player.SetHookPos(player.GTM.WorldSpaceGrappleTarget);
        }
        Vector2 hookDir = (player.HookOBJ.transform.position - player.transform.position).normalized;
        player.HookOBJ.transform.up = hookDir;
        //player.SetHookAngle(Helper.AngleOfTwoPoints((Vector2)player.transform.position,player.GTM.WorldSpaceGrappleTarget));




        if (durationOfState > 1)
        {
            SetMovingSprite();
        }
        if (Helper.isWithinMarginOfError(player.transform.position, player.GTM.WorldSpaceGrappleTarget, 5f))
        {
            player.rb.linearVelocity = Vector2.zero;
        }
        else
        {
            Vector2 Player2DPos = player.transform.position;
            player.rb.AddForce(Helper.AngleOfTwoPoints(Player2DPos,player.GTM.WorldSpaceGrappleTarget) * 20.0f);
        }
        if (!player.input.harpoonPressed)
        {
            DetermineStateChange();
        }
        base.FixedUpdate();
    }

    private void SetMovingSprite()
    {
        if (player.rb.linearVelocityX > 0f)
        {
            player.SetSprite(player.harpoonAfterFireRight);
        }
        else
        {
            player.SetSprite(player.harpoonAfterFireLeft);
        }
    }

    private void DetermineStateChange()
    {
        if (player.touchingWall & !player.onGround)
        {
            player.stateMachine.changeState(player.playerClimbState);
        }
        if (!player.touchingWall & player.onGround)
        {
            player.stateMachine.changeState(player.playerStandState);
        }
        if (!player.touchingWall & !player.onGround)
        {
            player.stateMachine.changeState(player.playerFallState);
        }
        if (player.touchingWall & player.onGround)
        {
            player.stateMachine.changeState(player.playerStandState);
        }
    }

    void SetSuccessSprite()
    {
        if (player.facingDirection == Player.FacingDirection.Left)
        {
            player.SetSprite(player.harpoonLeft);
        }
        else
        {
            player.SetSprite(player.harpoonRight);
        }
    }

    void SetFailSprite()
    {
    if (player.facingDirection == Player.FacingDirection.Left)
        {
            player.SetSprite(player.harpoonFailLeft);
        }
        else
        {
            player.SetSprite(player.harpoonFailRight);
        }
    }
}
