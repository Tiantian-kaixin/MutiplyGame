using UnityEngine;
using System.Collections;

public class GameOverState : BaseGameState {

    public GameOverState(StateMachine<IState> stateMachine, GameManager owner) : base(stateMachine, owner) {
        stateEnum = GameState.GameOver;
    }

    public override void OnEnter() {

    }

    public override void OnExist() {

    }

    public override void OnUpdate() {

    }
}

