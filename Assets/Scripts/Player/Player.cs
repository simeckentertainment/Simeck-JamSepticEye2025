using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    [System.NonSerialized] public PlayerStateMachine stateMachine; //This gets set at start.
    
    [Header("Unique Treasure Trackers")]
    [SerializeField] public bool hasSkull;
    [SerializeField] public bool hasButters;

    [Header("Uncommon Treasure Trackers")]
    [SerializeField] public bool[] haveConsoles;
    [SerializeField] public bool[] haveControllers;
    [SerializeField] public bool[] haveVinyls;
    List<Treasure> Consoles;
    List<Treasure> Controllers; 
    List<Treasure> Vinyls;
    
    [Header("Common Treasure Trackers")]
    [SerializeField] public bool[] haveBezoars;
    [SerializeField] public bool[] haveFleshes;
    List<Treasure> Bezoars;
    List<Treasure> Fleshes; 




    public PlayerIdleState playerIdleState { get; set; }
    // Start is called before the first frame update
    void Start(){

        GrabTreasures(); //Fills
       stateMachine = GetComponent<PlayerStateMachine>();
       playerIdleState = new PlayerIdleState(this, stateMachine);

       stateMachine.Initialize(playerIdleState);
    
    }

    void GrabTreasures(){
        Treasure[] allTreasures = FindObjectsOfType<Treasure>();
        foreach (Treasure treasure in allTreasures){
            switch (treasure.whatAmI){
                case Treasure.WhatAmI.Skull:

                break;
                case Treasure.WhatAmI.Butters:

                break;
                case Treasure.WhatAmI.Console:
                    Consoles.Add(treasure);
                break;
                case Treasure.WhatAmI.Controller:
                    Controllers.Add(treasure);
                break;
                case Treasure.WhatAmI.Flesh:
                    Fleshes.Add(treasure);
                break;
                case Treasure.WhatAmI.Bezoar:
                    Bezoars.Add(treasure);
                break;
                case Treasure.WhatAmI.Vinyl:
                    Vinyls.Add(treasure);
                break;
                default:
                break;
        }
        haveBezoars = new bool[Bezoars.Count];
        haveConsoles = new bool[Consoles.Count];
        haveControllers = new bool[Controllers.Count];
        haveFleshes = new bool[Fleshes.Count];
        haveVinyls = new bool[Vinyls.Count];
    }

    // Update is called once per frame
    void Update(){
    }
    void OnCollisionEnter(Collision other) {
    }
}

}


