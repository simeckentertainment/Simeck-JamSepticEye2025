using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAliveState{
    public PlayerJumpState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine){
    }
    Vector2 pushDir;
    float xFactor;
    bool stillJumpMode;
    public override void enter()
    {
        Debug.Log("Jump!");
        xFactor = player.input.moveVal.x;
        stillJumpMode = xFactor == 0 ? true : false;

        player.sfx.PlayOneShot(player.jumpSound);
        if (player.facingDirection == Player.FacingDirection.Left)
        {
            player.SetSprite(player.jumpLeft);
        }
        else
        {
            player.SetSprite(player.jumpRight);
        }


        player.onGround = false;


        if (player.touchingWall)
        {
            player.touchingWall = false;

            //SHould change it to sense what direction the sticky wall is.
            if (player.facingDirection == Player.FacingDirection.Right)
            {
                player.rb.linearVelocityX = -5.0f;
            }
            else
            {
                player.rb.linearVelocityX = 5.0f;
            }
        }
        base.enter();
    }
    public override void Update(){
    }

    public override void FixedUpdate(){
        float pushX = stillJumpMode ? player.input.moveVal.x * 10 : player.input.moveVal.x;

        //Pushes the player up when jump is pressed.
        float pushY = player.input.jumpPressed & (durationOfState < player.MaximumJumpHold) ? 150.0f : 0.0f;
        pushDir = new Vector2(pushX, pushY);
        player.rb.AddForce(pushDir);
        if (player.rb.linearVelocityY < 0.0f)
        {
            player.stateMachine.changeState(player.playerFallState);
        }
        if (player.touchingWall && durationOfState > 3)
        {
            player.stateMachine.changeState(player.playerClimbState);
        }
        if (player.input.harpoonPressed)
        {
            player.stateMachine.changeState(player.playerFireGrappleState);
        }
        base.FixedUpdate();
    }
}
