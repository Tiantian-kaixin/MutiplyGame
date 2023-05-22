using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class StateMachine<T> where T : IState {
    public T curState;
    public event Action<T> OnStateChange;
    private List<T> states = new();

    public StateMachine() {
    }

    public void Update() {
        curState?.OnUpdate();
    }

    public void AddState(T state) {
        states.Add(state);
    }

    public void ChangeState<Type>() where Type : T {
        Type state = states.OfType<Type>().FirstOrDefault();
        if (state != null) {
            ChangeState(state);
        } else {
            Debug.LogError("not find register state: " + typeof(Type).FullName);
        }
    }

    public void ChangeState(T state) {
        curState?.OnExist();
        curState = state;
        OnStateChange?.Invoke(state);
        curState.OnEnter();
    }

    public List<T> GetStates() {
        return states;
    }
}


