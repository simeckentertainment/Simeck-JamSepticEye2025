using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] public PlayerStateMachine stateMachine; //This gets set at start.
    [Header("Common Gameplay Vars")]
    public FacingDirection facingDirection;
    public InputDriver input;
    public Rigidbody2D rb;
    public BoxCollider2D playerCollider;
    public bool onGround;
    public bool touchingWall;
    public float MaximumJumpHold;
    public float maxLateralSpeed;
    public float climbSpeed;
    public int GrapplerDist;
    public GrapplerTargetingMachine GTM;

    [Header("Graphics Storage")]
    [SerializeField] public SpriteRenderer playerSpriteRenderer;
    [SerializeField] public Sprite standLeft;
    [SerializeField] public Sprite standRight;
    [SerializeField] public Sprite harpoonLeft;
    [SerializeField] public Sprite harpoonAfterFireLeft;
    [SerializeField] public Sprite harpoonRight;
    [SerializeField] public Sprite harpoonAfterFireRight;
    [SerializeField] public Sprite harpoonFailLeft;
    [SerializeField] public Sprite harpoonFailRight;
    [SerializeField] public Sprite[] runLeft;
    [SerializeField] public Sprite[] runRight;
    [SerializeField] public Sprite[] climbLeft;
    [SerializeField] public Sprite[] climbRight;
    [SerializeField] public Sprite harpoonArrow;
    [SerializeField] public Sprite jumpLeft;
    [SerializeField] public Sprite jumpRight;
    [SerializeField] public GameObject HookOBJ;



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
    bool gameBeaten;
    [Header("Harpoon Stuff")]
    [SerializeField] public GameObject LeftHookshotLaunchPoint;
    [SerializeField] public GameObject RightHookshotLaunchPoint;
    [SerializeField] public Transform targetingReticle;


    [Header("State Machine States")]

    public PlayerIdleState playerIdleState { get; set; }
    public PlayerFallState playerFallState { get; set; }
    public PlayerClimbState playerClimbState { get; set; }
    public PlayerJumpState playerJumpState { get; set; }
    public PlayerRunState playerRunState { get; set; }
    public PlayerStandState playerStandState { get; set; }
    public PlayerHopOnPlatformState playerHopOnPlatformState { get; set; }
    public PlayerFireGrappleState playerFireGrappleState { get; set; }
    public PlayerWinState playerWinState { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        gameBeaten = false;
        GrabTreasures(); //Fills
        stateMachine = GetComponent<PlayerStateMachine>();
        playerIdleState = new PlayerIdleState(this, stateMachine);
        playerClimbState = new PlayerClimbState(this, stateMachine);
        playerFallState = new PlayerFallState(this, stateMachine);
        playerJumpState = new PlayerJumpState(this, stateMachine);
        playerRunState = new PlayerRunState(this, stateMachine);
        playerStandState = new PlayerStandState(this, stateMachine);
        playerHopOnPlatformState = new PlayerHopOnPlatformState(this, stateMachine);
        playerWinState = new PlayerWinState(this, stateMachine);
        playerFireGrappleState = new PlayerFireGrappleState(this, stateMachine);
        stateMachine.Initialize(playerFallState);

    }

    void GrabTreasures()
    {
        Consoles = new List<Treasure>();
        Controllers = new List<Treasure>();
        Fleshes = new List<Treasure>();
        Bezoars = new List<Treasure>();
        Vinyls = new List<Treasure>();

        Treasure[] allTreasures = FindObjectsByType<Treasure>(FindObjectsSortMode.None);
        foreach (Treasure treasure in allTreasures)
        {
            switch (treasure.whatAmI)
            {
                case Treasure.WhatAmI.Skull:

                    break;
                case Treasure.WhatAmI.Butters:

                    break;
                case Treasure.WhatAmI.Console:

                    treasure.treasureIndex = Consoles.Count;
                    Consoles.Add(treasure);
                    break;
                case Treasure.WhatAmI.Controller:
                    treasure.treasureIndex = Controllers.Count;
                    Controllers.Add(treasure);
                    break;
                case Treasure.WhatAmI.Flesh:
                    treasure.treasureIndex = Fleshes.Count;
                    Fleshes.Add(treasure);
                    break;
                case Treasure.WhatAmI.Bezoar:
                    treasure.treasureIndex = Bezoars.Count;
                    Bezoars.Add(treasure);
                    break;
                case Treasure.WhatAmI.Vinyl:
                    treasure.treasureIndex = Vinyls.Count;
                    Vinyls.Add(treasure);
                    break;
                default:
                    break;
            }
        }
        haveBezoars = new bool[Bezoars.Count];
        haveConsoles = new bool[Consoles.Count];
        haveControllers = new bool[Controllers.Count];
        haveFleshes = new bool[Fleshes.Count];
        haveVinyls = new bool[Vinyls.Count];
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (input.moveVal.x > 0.0f)
        {
            facingDirection = FacingDirection.Right;
        }
        else if (input.moveVal.x < 0.0f)
        {
            facingDirection = FacingDirection.Left;
        }
        else if (input.moveVal.x == 0.0f)
        {
        }

        targetingReticle.position = GTM.WorldSpaceGrappleTarget; //The targeting reticle is in screen space.
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Ground":
                foreach (ContactPoint2D contact in other.contacts)
                {
                    onGround = contact.normal.y > 0.7f;
                    touchingWall = Mathf.Abs(contact.normal.x) > 0.7f;
                }
                break;
            case "Enemy":

                break;
            default:

                break;
        }
        if (other.gameObject.tag == "Ground")
        {

        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Ground":
                foreach (ContactPoint2D contact in other.contacts)
                {
                    onGround = !(contact.normal.y > 0.7f);
                    touchingWall = !(Mathf.Abs(contact.normal.x) > 0.7f);
                }
                break;
            case "Enemy":

                break;
            default:

                break;
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Treasure")
        {
            Treasure treasureObj = collision.gameObject.GetComponent<Treasure>();
            switch (treasureObj.whatAmI)
            {
                case Treasure.WhatAmI.Skull:
                    hasSkull = true;
                    Destroy(collision.gameObject);
                    break;
                case Treasure.WhatAmI.Butters:
                    hasButters = true;
                    Destroy(collision.gameObject);
                    break;
                case Treasure.WhatAmI.Console:
                    haveConsoles[treasureObj.treasureIndex] = true;
                    Destroy(collision.gameObject);
                    break;
                case Treasure.WhatAmI.Controller:
                    haveControllers[treasureObj.treasureIndex] = true;
                    Destroy(collision.gameObject);
                    break;
                case Treasure.WhatAmI.Flesh:
                    haveFleshes[treasureObj.treasureIndex] = true;
                    Destroy(collision.gameObject);
                    break;
                case Treasure.WhatAmI.Bezoar:
                    haveBezoars[treasureObj.treasureIndex] = true;
                    Destroy(collision.gameObject);
                    break;
                case Treasure.WhatAmI.Vinyl:
                    haveVinyls[treasureObj.treasureIndex] = true;
                    Destroy(collision.gameObject);
                    break;
                default:
                    break;
            }
        }
        if (CheckForCompletion())
        {
            stateMachine.changeState(playerWinState);
        }
    }


    bool CheckForCompletion()
    {
        return false;
    }
    public void SetSprite(Sprite whatSprite)
    {
        playerSpriteRenderer.sprite = whatSprite;
    }
    public enum FacingDirection { Left, Right };


    public void ShowHook()
    {
        HookOBJ.SetActive(true);
        targetingReticle.gameObject.SetActive(false);
    }
    public void HideHook()
    {
        HookOBJ.SetActive(false);
        targetingReticle.gameObject.SetActive(true);
    }
    public void SetHookPos(Vector2 pos)
    {
        HookOBJ.transform.position = pos;
    }
    public void SetHookAngle(Vector2 rot)
    {
        HookOBJ.transform.rotation = Quaternion.Euler(rot);
    }
    public void SetHookLaunchPoint(Vector2 pos)
    {
        HookOBJ.GetComponent<ChainManager>().launchPoint = pos; //Not performant, but fuck you for judging me. ~Randy
    }
}




