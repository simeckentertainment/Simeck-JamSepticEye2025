using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    [System.NonSerialized] public PlayerStateMachine stateMachine; //This gets set at start.
    public PlayerIdleState playerIdleState { get; set; }
    // Start is called before the first frame update
    void Start(){
       stateMachine = GetComponent<PlayerStateMachine>();
       playerIdleState = new PlayerIdleState(this, stateMachine);
       stateMachine.Initialize(playerIdleState);
    }

    // Update is called once per frame
    void Update(){
    }
    private void OnCollisionEnter(Collision other) {
    }
}




