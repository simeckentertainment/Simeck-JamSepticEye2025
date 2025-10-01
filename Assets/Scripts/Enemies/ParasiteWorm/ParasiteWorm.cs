using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteWorm : MonoBehaviour{
    [System.NonSerialized] public ParasiteWormStateMachine stateMachine; //This gets set at start.
    public ParasiteWormIdleState parasiteWormIdleState { get; set; }
    // Start is called before the first frame update
    void Start(){
       stateMachine = GetComponent<ParasiteWormStateMachine>();
       parasiteWormIdleState = new ParasiteWormIdleState(this, stateMachine);
       stateMachine.Initialize(parasiteWormIdleState);
    }

    // Update is called once per frame
    void Update(){
    }
    private void OnCollisionEnter(Collision other) {
    }
}




