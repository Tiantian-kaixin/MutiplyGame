using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    [SerializeField] private Button startBtn;
    [SerializeField] private Button quitBtn;

    void Start() {
        startBtn.onClick.AddListener(() => {
            SceneManager.LoadScene(SceneName.GameScene, LoadSceneMode.Single);
        });
        quitBtn.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
