using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbState : PlayerAliveState{
    public PlayerClimbState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine){
    }

    public override void enter(){
        base.enter();
    }
    public override void Update(){
        base.Update();
    }

    public override void FixedUpdate(){
        base.FixedUpdate();
    }
}
