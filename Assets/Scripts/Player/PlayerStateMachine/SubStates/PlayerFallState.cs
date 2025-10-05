using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerAliveState{
    public PlayerFallState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine){
    }

    public override void enter(){
        Debug.Log("Fall!");
        if (player.facingDirection == Player.FacingDirection.Left)
        {
            player.SetSprite(player.jumpLeft);
        }
        else
        {
            player.SetSprite(player.jumpRight);
        }
        base.enter();
    }
    public override void Update()
    {
        base.Update();
        player.rb.AddForceX(player.input.moveVal.x);
        if (player.onGround)
        {
            if (player.input.moveVal.x == 0.0f)
            {
                player.stateMachine.changeState(player.playerStandState);
            }
            else
            {
                player.stateMachine.changeState(player.playerRunState);
            }
        }
        if (player.touchingWall)
        {
            player.stateMachine.changeState(player.playerClimbState);
        }
    }

    public override void FixedUpdate(){
        base.FixedUpdate();
    }
}
