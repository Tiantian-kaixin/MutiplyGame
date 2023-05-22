using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class WaitingForStartUI : BaseGameStateUI<WaitingToStartState> {
    [SerializeField] private TextMeshProUGUI textUI;

    protected override void OnEnable() {
        base.OnEnable();
        GameManager.Instance.OnWaitingTimeChange += updateUI;
    }

    protected override void OnDisable() {
        base.OnDisable();
        GameManager.Instance.OnWaitingTimeChange -= updateUI;
    }

    private void updateUI(float count) {
        textUI.text = Mathf.Ceil(count).ToString();
    }
}
