using UnityEngine;
using System.Collections;

public abstract class State : IState {
    protected StateMachine<IState> stateMachine;

    protected State(StateMachine<IState> stateMachine) {
        this.stateMachine = stateMachine;
    }

    public abstract void OnEnter();
    public abstract void OnExist();
    public abstract void OnUpdate();
}
public abstract class State<T> : State where T : System.Enum {
    public T stateEnum;

    protected State(StateMachine<IState> stateMachine) : base(stateMachine) {
    }
}

public abstract class State<T1, T2> : State<T1> where T1 : System.Enum where T2 : class {
    public T2 owner;
    protected State(StateMachine<IState> stateMachine, T2 owner) : base(stateMachine) {
        this.owner = owner;
    }
}

