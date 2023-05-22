using UnityEngine;
using System.Collections;

public abstract class BaseGameState : State<GameState, GameManager> {
    public BaseGameState(StateMachine<IState> stateMachine, GameManager owner) : base(stateMachine, owner) {
    }
}

