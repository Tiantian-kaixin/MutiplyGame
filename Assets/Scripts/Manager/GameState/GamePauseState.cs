using UnityEngine;
using System.Collections;

public class GamePauseState : BaseGameState {
    public GamePauseState(StateMachine<IState> stateMachine, MyGameManager owner) : base(stateMachine, owner) {
        stateEnum = GameState.Paused;
    }

    public override void OnEnter() {
        PlayerInputManager.Instance.OnTogglePausePerform += _OnToggleGamePause;
        Time.timeScale = 0;
    }

    public override void OnExist() {
        PlayerInputManager.Instance.OnTogglePausePerform -= _OnToggleGamePause;
        Time.timeScale = 1;
    }

    public override void OnUpdate() {

    }

    private void _OnToggleGamePause() {
        MyGameManager.Instance.ChangeGameState(GameState.Playing);
    }
}

