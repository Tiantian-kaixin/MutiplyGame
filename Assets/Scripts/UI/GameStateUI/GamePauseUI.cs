﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePauseUI : BaseGameStateUI<GamePauseState> {
    [SerializeField] private Button resume;
    [SerializeField] private Button mainMenu;

    protected override void Start() {
        base.Start();
        resume.onClick.AddListener(() => {
            MyGameManager.Instance.ChangeGameState(GameState.Playing);
        });
        mainMenu.onClick.AddListener(() => {
            SceneManager.LoadScene(SceneName.MainMenuScene, LoadSceneMode.Single);
        });
    }
}

