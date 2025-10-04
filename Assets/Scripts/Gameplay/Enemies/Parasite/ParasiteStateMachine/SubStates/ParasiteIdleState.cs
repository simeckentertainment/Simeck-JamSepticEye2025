using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteIdleState : ParasiteSuperState{
    public ParasiteIdleState(Parasite parasite, ParasiteStateMachine parasiteStateMachine) : base(parasite, parasiteStateMachine){
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
