using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;
using Unity.Netcode;

public enum GameState {
    Lobby,
    WaitingToStart,
    Playing,
    Paused,
    GameOver,
}

public class MyGameManager : NetworkBehaviour {
    public static MyGameManager Instance { private set; get; }
    [field: SerializeField] public GameSetting gameSetting { private set; get; }
    public event Action<IState> OnGameStateChange;
    public event Action<float> OnWaitingTimeChange;
    public event Action<float> OnPlayingTimeChange;
    private GameState gameStateVariable = GameState.Lobby;

    private StateMachine<IState> stateMachine = new StateMachine<IState>();
    private void Awake() {
        Instance = this;
        stateMachine = new StateMachine<IState>();
        stateMachine.AddState(new GameLobbyState(stateMachine, this));
        stateMachine.AddState(new WaitingToStartState(stateMachine, this));
        stateMachine.AddState(new GamePlayingState(stateMachine, this));
        stateMachine.AddState(new GameOverState(stateMachine, this));
        stateMachine.AddState(new GamePauseState(stateMachine, this));
    }

    public override void OnNetworkSpawn() {
        base.OnNetworkSpawn();
        stateMachine.OnStateChange += _OnGameStateChange;
    }

    public override void OnNetworkDespawn() {
        base.OnNetworkDespawn();
        stateMachine.OnStateChange -= _OnGameStateChange;
    }

    private void Start() {
        stateMachine.ChangeState<GameLobbyState>();
    }

    private void _OnGameStateChange(IState state) {
        OnGameStateChange?.Invoke(state);
    }

    private void OnGameStateVariableChanged(GameState newValue) {
        Debug.Log("_OnGameStateVariableChanged:" + newValue.ToString());
        switch (newValue) {
            case GameState.Lobby:
                ChangeGameState<GameLobbyState>();
                break;
            case GameState.Playing:
                ChangeGameState<GamePlayingState>();
                break;
            case GameState.WaitingToStart:
                ChangeGameState<WaitingToStartState>();
                break;
            case GameState.Paused:
                ChangeGameState<GamePauseState>();
                break;
            case GameState.GameOver:
                ChangeGameState<GameOverState>();
                break;
        }
    }

    private void Update() {
        stateMachine.Update();
    }

    private void ChangeGameState<T>() where T : IState {
        stateMachine.ChangeState<T>();
    }

    public void WaitingToStartTick(float time) {
        OnWaitingTimeChange?.Invoke(time);
    }

    public void PlayTimeChange(float time) {
        OnPlayingTimeChange?.Invoke(time);
    }

    public bool IsStateRunning<T>() where T : IState {
        return stateMachine.IsStateRunning<T>();
    }

    [ServerRpc(RequireOwnership = false)]
    private void ChangeGameStateNetVariableServerRpc(GameState gameState) {
        Debug.Log("server rpc:" + gameState.ToString());
        ChangeGameStateNetVariableClientRpc(gameState);
    }

    [ClientRpc]
    private void ChangeGameStateNetVariableClientRpc(GameState gameState) {
        Debug.Log("client rpc:" + gameState.ToString());
        OnGameStateVariableChanged(gameState);
    }

    public void ChangeGameState(GameState gameState) {
        ChangeGameStateNetVariableServerRpc(gameState);
    }
}

