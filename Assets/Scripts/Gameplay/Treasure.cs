using UnityEngine;

public class Treasure : MonoBehaviour
{

    [SerializeField] public WhatAmI whatAmI;
    [SerializeField] public int treasureIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag != "Player" ){
            return;
        }

    }
    public enum WhatAmI{ Bezoar, Butters, Console, Controller, Flesh, Skull, Vinyl}
}
