using UnityEngine;
using System.Collections;

public class WaitingToStartState : BaseGameState {
    private float curTime;

    public WaitingToStartState(StateMachine<IState> stateMachine, GameManager owner) : base(stateMachine, owner) {
    }

    public override void OnEnter() {
        curTime = 0;
    }

    public override void OnExist() {

    }

    public override void OnUpdate() {
        curTime += Time.deltaTime;
        if (curTime > owner.gameSetting.ReadyCount) {
            stateMachine.ChangeState<PlayingState>();
            return;
        }
        owner.WaitingToStartTick(owner.gameSetting.ReadyCount - curTime);
    }
}

