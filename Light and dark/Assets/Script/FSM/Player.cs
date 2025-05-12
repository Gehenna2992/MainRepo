using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class Player : Entity
{
  
    #region States
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerState_idle idleState { get; private set; }
    public PlayerState_move moveState { get; private set; }
    public PlayerState_Jump jumpState { get; private set; }
    public PlayerState_Air airState { get; private set; }
    public PlayerState_PrimaryAttack primaryAttack { get; private set; }
    #endregion
    #region Input
    public PlayerInput inputControl { get; private set; }
    public Vector2 AxisInput;
    #endregion
    #region Info
    public bool isBusy;
    [Header("Move Info")]
    public float moveSpeed;

    [Header("Jump Info")]
    public float jumpForce;

    #endregion

    private void OnEnable() => inputControl.Enable();
    private void OnDisable() => inputControl.Disable();
    protected override void Awake()
    {
        base.Awake();

        StateMachine = new PlayerStateMachine();
        idleState = new PlayerState_idle(this, StateMachine, "Idle");
        moveState = new PlayerState_move(this, StateMachine, "Move");
        jumpState = new PlayerState_Jump(this, StateMachine, "Jump");
        airState = new PlayerState_Air(this, StateMachine, "Jump");
        primaryAttack = new PlayerState_PrimaryAttack(this, StateMachine, "Attack");
        inputControl = new PlayerInput();
    }

    protected override void Start()
    {
        base.Start();

        StateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        GatherInput();
        GroundCheck();
        StateMachine.currentState.Update();
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    void GatherInput()
    {
        AxisInput = inputControl.Player.Move.ReadValue<Vector2>();
    }

   
    public void AnimationTrigger()=>StateMachine.currentState.AnimationFinishTrigger();

    public bool IsAttacking()
    {
        return StateMachine.currentState == primaryAttack;
    }

    public IEnumerator BusyFor(float _seconds)
    { 
        isBusy = true;
        Debug.Log("I am busy.");
        yield return new WaitForSeconds(_seconds);
        Debug.Log("I am not busy.");
        isBusy = false;
    }
}

