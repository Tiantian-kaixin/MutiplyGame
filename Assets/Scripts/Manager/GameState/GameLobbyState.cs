using UnityEngine;
using System.Collections;

public class GameLobbyState : State<GameState, GameManager> {
    public GameLobbyState(StateMachine<IState> stateMachine, GameManager owner) : base(stateMachine, owner) {
        stateEnum = GameState.Lobby;
    }

    public override void OnEnter() {

    }

    public override void OnExist() {

    }

    public override void OnUpdate() {

    }
}

