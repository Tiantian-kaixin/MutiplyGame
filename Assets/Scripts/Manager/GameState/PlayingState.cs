using UnityEngine;
using System.Collections;

public class PlayingState : BaseGameState {
    private float curTime;

    public PlayingState(StateMachine<IState> stateMachine, GameManager owner) : base(stateMachine, owner) {
    }

    public override void OnEnter() {
        curTime = 0;
    }

    public override void OnExist() {

    }

    public override void OnUpdate() {
        curTime += Time.deltaTime;
        if (curTime >= owner.gameSetting.PlayDuration) {
            stateMachine.ChangeState<GameOverState>();
            return;
        }
        owner.PlayTimeChange(curTime);
    }
}

