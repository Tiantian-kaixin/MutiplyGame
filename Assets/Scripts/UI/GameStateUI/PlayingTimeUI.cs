using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PlayingTimeUI : BaseGameStateUI<PlayingState> {
    [field: SerializeField] public Image clockImg;

    protected override void OnEnable() {
        base.OnEnable();
        GameManager.Instance.OnPlayingTimeChange += OnPlayingTimeChange;
    }
    protected override void OnDisable() {
        base.OnDisable();
        GameManager.Instance.OnPlayingTimeChange -= OnPlayingTimeChange;
    }
    private void OnPlayingTimeChange(float curTime) {
        clockImg.fillAmount = Math.Min(curTime / GameManager.Instance.gameSetting.PlayDuration, 1);
    }
}

