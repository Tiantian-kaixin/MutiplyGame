using UnityEngine;
using System.Collections;

public class GamePlayingState : BaseGameState {
    private float curTime;
    private bool isInterrupt = false;

    public GamePlayingState(StateMachine<IState> stateMachine, MyGameManager owner) : base(stateMachine, owner) {
        stateEnum = GameState.Playing;
    }

    public override void OnEnter() {
        PlayerInputManager.Instance.OnTogglePausePerform += _OnToggleGamePause;
        if (!isInterrupt) {
            curTime = 0;
        }
    }

    public override void OnExist() {
        PlayerInputManager.Instance.OnTogglePausePerform -= _OnToggleGamePause;
        isInterrupt = false;
    }

    public override void OnUpdate() {
        curTime += Time.deltaTime;
        if (curTime >= owner.gameSetting.PlayDuration) {
            MyGameManager.Instance.ChangeGameState(GameState.GameOver);
            return;
        }
        owner.PlayTimeChange(curTime);
    }
    private void _OnToggleGamePause() {
        MyGameManager.Instance.ChangeGameState(GameState.Paused);
        isInterrupt = true;
    }
}

