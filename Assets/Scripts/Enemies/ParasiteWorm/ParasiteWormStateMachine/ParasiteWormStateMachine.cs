using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteWormStateMachine : MonoBehaviour{
    public ParasiteWormMasterState currentState;
    public void Initialize(ParasiteWormMasterState startState){
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
   public void changeState(ParasiteWormMasterState nextState){
        if(currentState != nextState){
            currentState.exit();
            currentState = nextState;
            nextState.enter();
        }
    }
}

