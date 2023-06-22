using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GamePlayingTimeUI : BaseGameStateUI<GamePlayingState> {
    [field: SerializeField] public Image clockImg;

    protected override void OnEnable() {
        base.OnEnable();
        MyGameManager.Instance.OnPlayingTimeChange += OnPlayingTimeChange;
    }

    protected override void _OnGameStateChange(IState state) {
        ActiveUI(state is GamePlayingState || state is GamePauseState);
    }

    protected override void OnDisable() {
        base.OnDisable();
        MyGameManager.Instance.OnPlayingTimeChange -= OnPlayingTimeChange;
    }

    private void OnPlayingTimeChange(float curTime) {
        clockImg.fillAmount = Math.Min(curTime / MyGameManager.Instance.gameSetting.PlayDuration, 1);
    }
}

