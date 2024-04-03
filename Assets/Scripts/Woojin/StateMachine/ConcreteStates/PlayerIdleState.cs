using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerPlatformmerStateMachine currentContext, PlayerStateFactory playerStateFactory)
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
        Ctx.HorizontalVelocity = Vector2.zero;
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
        } else if (Ctx.IsMovement && !Ctx.IsRunning) {
            SwitchState(Fty.Walk());
        }
    }
}
