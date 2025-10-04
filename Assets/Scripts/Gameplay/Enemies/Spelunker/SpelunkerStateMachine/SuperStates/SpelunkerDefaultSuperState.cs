using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpelunkerSuperState : SpelunkerMasterState{
    public SpelunkerSuperState(Spelunker spelunker, SpelunkerStateMachine spelunkerStateMachine) : base(spelunker, spelunkerStateMachine){
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
