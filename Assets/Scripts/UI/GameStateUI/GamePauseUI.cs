using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePauseUI : BaseGameStateUI<GamePauseState> {
    [SerializeField] private Button resume;
    [SerializeField] private Button mainMenu;

    protected override void Start() {
        base.Start();
        resume.onClick.AddListener(() => {
            GameManager.Instance.ChangeGameState<GamePlayingState>();
        });
        mainMenu.onClick.AddListener(() => {
            SceneManager.LoadScene(SceneName.MainMenuScene, LoadSceneMode.Single);
        });
    }
}

