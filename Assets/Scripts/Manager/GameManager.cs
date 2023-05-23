using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;

public enum GameState {
    Lobby,
    WaitingToStart,
    Playing,
    Paused,
    GameOver,
}

public class GameManager : MonoBehaviour {
    public static GameManager Instance { private set; get; }
    [field: SerializeField] public GameSetting gameSetting { private set; get; }
    public event Action<IState> OnGameStateChange;
    public event Action<float> OnWaitingTimeChange;
    public event Action<float> OnPlayingTimeChange;

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

    private void OnEnable() {
        stateMachine.OnStateChange += _OnGameStateChange;
    }

    private void OnDisable() {
        stateMachine.OnStateChange -= _OnGameStateChange;
    }

    private void Start() {
        stateMachine.ChangeState<GameLobbyState>();
    }

    private void _OnGameStateChange(IState state) {
        OnGameStateChange?.Invoke(state);
    }

    private void Update() {
        stateMachine.Update();
    }

    public void ChangeGameState<T>() where T : IState {
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
}

