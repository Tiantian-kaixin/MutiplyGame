using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class GameLobbyUI : BaseGameStateUI<GameLobbyState> {
    public Button hostBtn;
    public Button clientBtn;

    protected override void Start() {
        hostBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
            GameManager.Instance.ChangeGameState(GameState.WaitingToStart);
            ActiveUI(false);
        });
        clientBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartClient();
            GameManager.Instance.ChangeGameState(GameState.WaitingToStart);
            ActiveUI(false);
        });
    }
}
