public abstract class PlayerBaseState
{
    private bool _isRootState = false;
    private PlayerPlatformmerStateMachine _ctx;
    private PlayerStateFactory _fty;
    private PlayerBaseState _currentSubState, _currentSuperState;

    protected bool IsRootState {set=> _isRootState = value;}
    protected PlayerPlatformmerStateMachine Ctx => _ctx;
    protected PlayerStateFactory Fty => _fty;

    public PlayerBaseState(PlayerPlatformmerStateMachine ctx, PlayerStateFactory fty) {
        _ctx = ctx;
        _fty = fty;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchStates();
    public abstract void InitSubState();

    public void UpdateStates(){
        UpdateState();
        if (_currentSubState != null) {
            _currentSubState.UpdateStates();
        }
    }

    public void FixedUpdateStates(){
        FixedUpdateState();
        if (_currentSubState != null) {
            _currentSubState.FixedUpdateStates();
        }
    }

    protected void SwitchState(PlayerBaseState newState){
        ExitState();
        newState.EnterState();
        if (_isRootState)
            _ctx.CurrentState = newState;
        else if (_currentSuperState != null) {
            _currentSuperState.SetSubState(newState);
        }
    }
    protected void SetSuperState(PlayerBaseState newSuperState){
        _currentSuperState = newSuperState;
    }
    protected void SetSubState(PlayerBaseState newSubState){
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }

}
