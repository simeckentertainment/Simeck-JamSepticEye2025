using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandState : PlayerAliveState{
    public PlayerStandState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine){
    }

    public override void enter(){
        Debug.Log("Stand!");
        if (player.facingDirection == Player.FacingDirection.Left)
        {
            player.SetSprite(player.standLeft);
        }
        if (player.facingDirection == Player.FacingDirection.Right)
        {
            player.SetSprite(player.standRight);
        }
        base.enter();
    }
    public override void Update(){
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (player.input.moveVal.x != 0.0f)
        {
            player.stateMachine.changeState(player.playerRunState);
        }
        if (player.input.jumpPressed)
        {
            player.stateMachine.changeState(player.playerJumpState);
        }
        if (player.input.harpoonPressed)
        {
            player.stateMachine.changeState(player.playerFireGrappleState);
        }
        
    }
}
