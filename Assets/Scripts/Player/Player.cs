using System.Collections;
using System.Collections.Generic;
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


    [Header("Graphics Storage")]
    [SerializeField] public SpriteRenderer playerSpriteRenderer;
    [SerializeField] public Sprite standLeft;
    [SerializeField] public Sprite standRight;
    [SerializeField] public Sprite harpoonLeft;
    [SerializeField] public Sprite harpoonAfterFireLeft;
    [SerializeField] public Sprite harpoonRight;
    [SerializeField] public Sprite harpoonAfterFireRight;
    [SerializeField] public Sprite[] runLeft;
    [SerializeField] public Sprite[] runRight;
    [SerializeField] public Sprite[] climbLeft;
    [SerializeField] public Sprite[] climbRight;
    [SerializeField] public Sprite harpoonArrow;
    [SerializeField] public Sprite jumpLeft;
    [SerializeField] public Sprite jumpRight;



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
[Header("State Machine States")]

    public PlayerIdleState playerIdleState { get; set; }
    public PlayerFallState playerFallState { get; set; }
    public PlayerClimbState playerClimbState { get; set; }
    public PlayerJumpState playerJumpState { get; set; }
    public PlayerRunState playerRunState { get; set; }
    public PlayerStandState playerStandState { get; set; }
    public PlayerHopOnPlatformState playerHopOnPlatformState { get; set; }
    public PlayerFireGrappleState playerFireGrappleState { get; set; }
    // Start is called before the first frame update
    void Start()
    {

        GrabTreasures(); //Fills
        stateMachine = GetComponent<PlayerStateMachine>();
        playerIdleState = new PlayerIdleState(this, stateMachine);
        playerClimbState = new PlayerClimbState(this, stateMachine);
        playerFallState = new PlayerFallState(this, stateMachine);
        playerJumpState = new PlayerJumpState(this, stateMachine);
        playerRunState = new PlayerRunState(this, stateMachine);
        playerStandState = new PlayerStandState(this, stateMachine);
        playerHopOnPlatformState = new PlayerHopOnPlatformState(this, stateMachine);
        stateMachine.Initialize(playerFallState);

    }

    void GrabTreasures()
    {
        Treasure[] allTreasures = FindObjectsOfType<Treasure>();
        foreach (Treasure treasure in allTreasures)
        {
            switch (treasure.whatAmI)
            {
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
        else
        {
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag){
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
        if (other.gameObject.tag == "Ground"){
            
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

    public void SetSprite(Sprite whatSprite)
    {
        playerSpriteRenderer.sprite = whatSprite;
    }
    public enum FacingDirection { Left, Right };
}




