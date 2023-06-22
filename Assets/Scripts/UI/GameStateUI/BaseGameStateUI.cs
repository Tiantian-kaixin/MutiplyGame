using UnityEngine;
using System.Collections;
using System;

public abstract class BaseGameStateUI<T> : MonoBehaviour where T : IState {

    protected virtual void Start() {
        MyGameManager.Instance.OnGameStateChange += _OnGameStateChange;
        ActiveUI(false);
    }
    protected virtual void OnEnable() {
    }

    protected virtual void OnDisable() {
    }

    private void OnDestroy() {
        MyGameManager.Instance.OnGameStateChange -= _OnGameStateChange;
    }

    protected virtual void _OnGameStateChange(IState state) {
        ActiveUI(state is T);
    }

    protected virtual void ActiveUI(bool active) {
        gameObject.SetActive(active);
    }
}

