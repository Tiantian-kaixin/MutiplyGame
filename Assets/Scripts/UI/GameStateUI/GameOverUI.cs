using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class GameOverUI : BaseGameStateUI<GameOverState> {
    [SerializeField] private TextMeshProUGUI textUI;
    [SerializeField] private Button replayBtn;
    private int score = 0;

    protected override void OnEnable() {
        base.OnEnable();
        DeliveryManager.Instance.OnCompleteOrder += onAddScore;
        replayBtn.onClick.AddListener(() => {
            GameManager.Instance.ChangeGameState(GameState.WaitingToStart);
        });
    }

    protected override void OnDisable() {
        base.OnDisable();
        DeliveryManager.Instance.OnCompleteOrder -= onAddScore;
    }

    private void onAddScore() {
        score += 1;
        textUI.text = score.ToString();
    }
}

