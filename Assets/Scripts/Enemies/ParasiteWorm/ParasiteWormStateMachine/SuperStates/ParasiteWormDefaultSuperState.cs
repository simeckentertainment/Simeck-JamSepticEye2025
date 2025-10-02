using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteWormSuperState : ParasiteWormMasterState{
    public ParasiteWormSuperState(ParasiteWorm parasiteWorm, ParasiteWormStateMachine parasiteWormStateMachine) : base(parasiteWorm, parasiteWormStateMachine){
    }


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
