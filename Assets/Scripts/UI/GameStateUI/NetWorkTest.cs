using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NetWorkTest : BaseGameStateUI<LobbyState> {
    public Button hostBtn;
    public Button clientBtn;

    protected override void Start() {
        hostBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
            GameManager.Instance.ChangeGameState<WaitingToStartState>();
            ActiveUI(false);
        });
        clientBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartClient();
            GameManager.Instance.ChangeGameState<WaitingToStartState>();
            ActiveUI(false);
        });
    }
}
