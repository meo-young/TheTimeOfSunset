using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHoverState : PlayerBaseState
{
    public PlayerHoverState(PlayerPlatformmerStateMachine currentContext, PlayerStateFactory playerStateFactory)
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
        Ctx.VerticalVelocity -= Ctx.Gravity * Ctx.HoverGravityMultiplier* Vector2.up * Ctx.Dt;
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
        if (Ctx.IsGrounded) {
            SwitchState(Fty.Grounded());
        } else if (Ctx.VerticalVelocity.y <= -Ctx.HoverSpeed) {
            SwitchState(Fty.Fall());
        }
    }
}
