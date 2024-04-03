using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerPlatformmerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    :base (currentContext, playerStateFactory){}

   
    public override void EnterState()
    {
        
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void FixedUpdateState()
    {
        Ctx.HorizontalVelocity = (Ctx.MovementInput - Vector2.Dot(Ctx.MovementInput, Ctx.GroundNormal) * Ctx.GroundNormal) * Ctx.WalkSpeed;
    }
    public override void ExitState()
    {
        
    }
    public override void InitSubState()
    {
        
    }
    public override void CheckSwitchStates()
    {
        if (Ctx.IsMovement && Ctx.IsRunning) {
            SwitchState(Fty.Run());
        } else if (!Ctx.IsMovement) {
            SwitchState(Fty.Idle());
        }
    }
}
