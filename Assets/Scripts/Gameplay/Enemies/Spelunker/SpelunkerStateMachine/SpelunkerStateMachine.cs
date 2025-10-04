using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpelunkerStateMachine : MonoBehaviour{
    public SpelunkerMasterState currentState;
    public void Initialize(SpelunkerMasterState startState){
        currentState = startState;
        currentState.enter();
}

// Update is called once per frame
    public void Update(){
        currentState.Update();
    }
    public void FixedUpdate(){
        currentState.FixedUpdate();
    }
   public void changeState(SpelunkerMasterState nextState){
        if(currentState != nextState){
            currentState.exit();
            currentState = nextState;
            nextState.enter();
        }
    }
}

