using UnityEngine;
using System.Collections;
using System;

public abstract class BaseGameStateUI<T> : MonoBehaviour where T : IState {
    public GameObject UIContanier;

    protected virtual void Start() {
        ActiveUI(false);
    }
    protected virtual void OnEnable() {
        GameManager.Instance.OnGameStateChange += _OnGameStateChange;
    }

    protected virtual void OnDisable() {
        GameManager.Instance.OnGameStateChange -= _OnGameStateChange;
    }

    private void _OnGameStateChange(IState state) {
        ActiveUI(state is T);
    }

    protected void ActiveUI(bool active) {
        UIContanier.SetActive(active);
    }
}

