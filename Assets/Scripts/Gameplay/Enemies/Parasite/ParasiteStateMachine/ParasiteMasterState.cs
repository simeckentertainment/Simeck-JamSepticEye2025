using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ParasiteMasterState{
    protected Parasite parasite;
    protected ParasiteStateMachine parasiteStateMachine;
    protected int durationOfState = 0;
    public ParasiteMasterState(Parasite parasite, ParasiteStateMachine parasiteStateMachine){
        this.parasite = parasite;
        this.parasiteStateMachine = parasiteStateMachine;
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
