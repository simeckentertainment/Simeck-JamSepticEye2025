using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spelunker : MonoBehaviour{
    [System.NonSerialized] public SpelunkerStateMachine stateMachine; //This gets set at start.
    public SpelunkerIdleState spelunkerIdleState { get; set; }
    // Start is called before the first frame update
    void Start(){
       stateMachine = GetComponent<SpelunkerStateMachine>();
       spelunkerIdleState = new SpelunkerIdleState(this, stateMachine);
       stateMachine.Initialize(spelunkerIdleState);
    }

    // Update is called once per frame
    void Update(){
    }
    private void OnCollisionEnter(Collision other) {
    }
}




