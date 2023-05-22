using UnityEngine;
using System.Collections;

public class CookCounterIdelState : CookCounterState {
    public CookCounterIdelState(StateMachine<IState> stateMachine, CookCounter cookCounter) : base(stateMachine, cookCounter) {
        stateEnum = CookStateEnum.IDLE;
    }
    public override void OnEnter() {
    }

    public override void OnExist() {
    }

    public override void OnUpdate() {

    }
}