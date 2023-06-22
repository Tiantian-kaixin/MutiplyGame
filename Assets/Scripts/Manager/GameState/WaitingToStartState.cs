using UnityEngine;
using System.Collections;

public class WaitingToStartState : BaseGameState {
    private float curTime;

    public WaitingToStartState(StateMachine<IState> stateMachine, MyGameManager owner) : base(stateMachine, owner) {
        stateEnum = GameState.WaitingToStart;
    }

    public override void OnEnter() {
        curTime = 0;
    }

    public override void OnExist() {

    }

    public override void OnUpdate() {
        curTime += Time.deltaTime;
        if (curTime > owner.gameSetting.ReadyCount) {
            MyGameManager.Instance.ChangeGameState(GameState.Playing);
            return;
        }
        owner.WaitingToStartTick(owner.gameSetting.ReadyCount - curTime);
    }
}

