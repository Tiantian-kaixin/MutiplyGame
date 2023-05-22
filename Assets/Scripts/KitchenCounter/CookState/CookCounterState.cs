using UnityEngine;
using System.Collections;

// public abstract class CookCounterState : IState {
//    public CookState stateEnum;
//    protected CookCounter owner;
//    public CookCounterState(CookCounter cookCounter) {
//        this.owner = cookCounter;
//    }
//    public abstract void OnEnter();
//    public abstract void OnExist();
//    public abstract void OnUpdate();
// }

public abstract class CookCounterState : State<CookStateEnum, CookCounter> {
    protected CookCounterState(StateMachine<IState> stateMachine, CookCounter owner) : base(stateMachine, owner) {
    }
}
