using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parasite : MonoBehaviour{
    [System.NonSerialized] public ParasiteStateMachine stateMachine; //This gets set at start.
    public ParasiteIdleState parasiteIdleState { get; set; }
    // Start is called before the first frame update
    void Start(){
       stateMachine = GetComponent<ParasiteStateMachine>();
       parasiteIdleState = new ParasiteIdleState(this, stateMachine);
       stateMachine.Initialize(parasiteIdleState);
    }

    // Update is called once per frame
    void Update(){
    }
    private void OnCollisionEnter(Collision other) {
    }
}




