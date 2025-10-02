using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ParasiteWormMasterState{
    protected ParasiteWorm parasiteWorm;
    protected ParasiteWormStateMachine parasiteWormStateMachine;
    protected int durationOfState = 0;
    public ParasiteWormMasterState(ParasiteWorm parasiteWorm, ParasiteWormStateMachine parasiteWormStateMachine){
        this.parasiteWorm = parasiteWorm;
        this.parasiteWormStateMachine = parasiteWormStateMachine;
    }
    public virtual void enter(){
        durationOfState = 0;
    }
    public virtual void enterNoanimate(){
        durationOfState = 0;
    }
    // Start is called before the first frame update
    void Start(){
        
    }
    // Update is called once per frame
    public virtual void Update(){

    }
    public virtual void FixedUpdate(){
        durationOfState++;
    }
    public virtual void exit(){

    }
}
