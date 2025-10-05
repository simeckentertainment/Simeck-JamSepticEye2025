using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerAliveState
{
    public PlayerRunState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }
    int GraphicChangeThreshold;
    int currentGraphicIndex;
    int graphicChangeCounter;
    Sprite[] currentGraphics;

    public override void enter()
    {

        GraphicChangeThreshold = 3;
        currentGraphicIndex = 0;
        graphicChangeCounter = 0;
        base.enter();
    }
    public override void Update()
    {



        base.Update();
    }

    public override void FixedUpdate()
    {
        RunGraphics();
        player.facingDirection = player.input.moveVal.x > 0.0f ? Player.FacingDirection.Right : Player.FacingDirection.Left;
        player.rb.AddForceX(player.input.moveVal.x, ForceMode2D.Impulse);
        if (player.rb.linearVelocityX > player.maxLateralSpeed) { player.rb.linearVelocityX = player.maxLateralSpeed; }
        if (player.rb.linearVelocityX < (player.maxLateralSpeed * -1)) { player.rb.linearVelocityX = (player.maxLateralSpeed * -1); }

        if (player.input.moveVal.x == 0.0f)
        {
            player.rb.linearVelocityX = player.rb.linearVelocityX < 0.02f & player.rb.linearVelocityX > -0.02f ? player.rb.linearVelocityX = 0.0f : player.rb.linearVelocityX *= 0.2f;
        }


        if (player.rb.linearVelocityX == 0.0f)
        {
            player.stateMachine.changeState(player.playerStandState);
        }
        if (player.input.jumpPressed)
        {
            player.stateMachine.changeState(player.playerJumpState);
        }

        if (player.touchingWall)
        {
            player.stateMachine.changeState(player.playerClimbState);
        }
        base.FixedUpdate();
    }


    void RunGraphics()
    {
        graphicChangeCounter++;
        if (player.facingDirection == Player.FacingDirection.Left) { currentGraphics = player.runLeft; } else { currentGraphics = player.runRight; }

        //If we're in the middle of counting up, then don't worry about it.
        if (graphicChangeCounter < GraphicChangeThreshold) { return; }
        currentGraphicIndex++;
        if (currentGraphicIndex >= currentGraphics.Length) { currentGraphicIndex = 0; }
        player.SetSprite(currentGraphics[currentGraphicIndex]);
        graphicChangeCounter = 0;
        
    }



}
