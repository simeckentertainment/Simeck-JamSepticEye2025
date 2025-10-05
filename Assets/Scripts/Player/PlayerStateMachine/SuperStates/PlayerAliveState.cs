using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAliveState : PlayerMasterState{
    public PlayerAliveState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine){
    }

    //Here is where we will do  the grappler stuff.
    public override void enter(){
        base.enter();
    }

    public override void Update(){
        base.Update();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
