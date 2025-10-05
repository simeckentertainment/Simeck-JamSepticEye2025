using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbState : PlayerAliveState
{
    public PlayerClimbState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }
    int GraphicChangeThreshold;
    int currentGraphicIndex;
    int graphicChangeCounter;
    bool temporaryignoreJump;
    Sprite[] currentGraphics;
    bool contactLossBufferFrameUsed;
    bool ignoreGrappleInput;
    public override void enter()
    {
        ignoreGrappleInput = true;
        Debug.Log("Climb!");
        contactLossBufferFrameUsed = false;
        GraphicChangeThreshold = 3;
        currentGraphicIndex = 0;
        graphicChangeCounter = 0;

        if (player.facingDirection == Player.FacingDirection.Left) { currentGraphics = player.climbLeft; } else { currentGraphics = player.climbRight; }
        player.SetSprite(currentGraphics[0]);



        if (player.input.jumpPressed)
        {
            temporaryignoreJump = true;
        }
        base.enter();
    }
    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (temporaryignoreJump & !player.input.jumpPressed) { temporaryignoreJump = false; }
        player.rb.linearVelocityY = 0.0f;
        player.rb.linearVelocityX = 0.0f;
        RunGraphics();
        if (player.input.moveVal.y > 0.2f)
        {
            player.rb.AddForceY(player.climbSpeed, ForceMode2D.Impulse);
        }
        else if (player.input.moveVal.y < -0.2f)
        {
            player.rb.AddForceY(player.climbSpeed * -1.0f, ForceMode2D.Impulse);
        }
        if (player.input.jumpPressed & !temporaryignoreJump)
        {
            player.stateMachine.changeState(player.playerJumpState);
        }
        if (player.playerCollider.GetContacts(new ContactPoint2D[10]) == 0)
        {
            if (!contactLossBufferFrameUsed)
            {
                contactLossBufferFrameUsed = true;
                return;
            }
            player.stateMachine.changeState(player.playerHopOnPlatformState);
        }
        else
        {
            contactLossBufferFrameUsed = false;
        }
        if (player.playerCollider.GetContacts(new ContactPoint2D[10]) == 4)
        {
            player.touchingWall = false;
            player.stateMachine.changeState(player.playerStandState);
        }

        if (!player.input.harpoonPressed & ignoreGrappleInput) //Ignoring grappler input until it's released once.
        {
            ignoreGrappleInput = false;
        }
        if (player.input.harpoonPressed & !ignoreGrappleInput)
        {
            player.stateMachine.changeState(player.playerFireGrappleState);
        }
    }


    void RunGraphics()
    {
        if (player.input.moveVal.y == 0.0f) { return; } //don't do anything if we're not doing anything.
        graphicChangeCounter++;
        if (player.facingDirection == Player.FacingDirection.Left) { currentGraphics = player.climbLeft; } else { currentGraphics = player.climbRight; }
        if (graphicChangeCounter < GraphicChangeThreshold) { return; }
        currentGraphicIndex++;
        if (currentGraphicIndex >= currentGraphics.Length) { currentGraphicIndex = 0; }
        player.SetSprite(currentGraphics[currentGraphicIndex]);
        graphicChangeCounter = 0;
    }
}
