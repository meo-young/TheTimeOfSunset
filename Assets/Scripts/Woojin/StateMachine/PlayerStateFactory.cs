public class PlayerStateFactory
{
    PlayerPlatformmerStateMachine _context;

    public PlayerStateFactory (PlayerPlatformmerStateMachine curretContext) {
        _context = curretContext;
    }

    public PlayerBaseState Idle() {
        return new PlayerIdleState(_context, this);
    }

    public PlayerBaseState Walk() {
        return new PlayerWalkState(_context, this);
    }

    public PlayerBaseState Run() {
        return new PlayerRunState(_context, this);
    }

    public PlayerBaseState Grounded() {
        return new PlayerGroundedState(_context, this);
    }

    public PlayerBaseState Jump() {
        return new PlayerJumpState(_context, this);
    }

    public PlayerBaseState Hover() {
        return new PlayerHoverState(_context, this);
    }

    public PlayerBaseState Fall() {
        return new PlayerFallState(_context, this);
    }
}
