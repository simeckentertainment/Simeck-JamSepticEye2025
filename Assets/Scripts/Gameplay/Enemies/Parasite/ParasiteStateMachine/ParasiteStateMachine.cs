using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteStateMachine : MonoBehaviour{
    public ParasiteMasterState currentState;
    public void Initialize(ParasiteMasterState startState){
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
   public void changeState(ParasiteMasterState nextState){
        if(currentState != nextState){
            currentState.exit();
            currentState = nextState;
            nextState.enter();
        }
    }
}

