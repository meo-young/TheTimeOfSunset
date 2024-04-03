using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerPlatformmerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    :base (currentContext, playerStateFactory){
        IsRootState = true;
        InitSubState();
    }

    public override void EnterState()
    {
        
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void FixedUpdateState()
    {
        Ctx.VerticalVelocity = Vector2.zero;
    }
    public override void ExitState()
    {
        
    }
    public override void InitSubState()
    {
        if (!Ctx.IsMovement) {
            SetSubState(Fty.Idle());
        } else if (!Ctx.IsRunning) {
            SetSubState(Fty.Walk());
        } else {
            SetSubState(Fty.Run());
        }
    }
    public override void CheckSwitchStates()
    {
        if (Ctx.IsJumpPressed) {
            SwitchState(Fty.Jump());
        } else if (!Ctx.IsGrounded) {
            SwitchState(Fty.Fall());
        }
    }
}
