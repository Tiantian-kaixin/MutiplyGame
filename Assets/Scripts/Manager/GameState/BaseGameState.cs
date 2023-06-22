using UnityEngine;
using System.Collections;

public abstract class BaseGameState : State<GameState, MyGameManager> {
    public BaseGameState(StateMachine<IState> stateMachine, MyGameManager owner) : base(stateMachine, owner) {
    }
}

