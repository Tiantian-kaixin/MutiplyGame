using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameWaitingForStartUI : BaseGameStateUI<WaitingToStartState> {
    [SerializeField] private TextMeshProUGUI textUI;

    protected override void OnEnable() {
        base.OnEnable();
        MyGameManager.Instance.OnWaitingTimeChange += updateUI;
    }

    protected override void OnDisable() {
        base.OnDisable();
        MyGameManager.Instance.OnWaitingTimeChange -= updateUI;
    }

    private void updateUI(float count) {
        textUI.text = Mathf.Ceil(count).ToString();
    }
}
