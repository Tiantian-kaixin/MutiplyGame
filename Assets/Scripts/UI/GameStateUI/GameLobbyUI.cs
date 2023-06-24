using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class GameLobbyUI : BaseGameStateUI<GameLobbyState> {
    public Button serverBtn;
    public Button clientBtn;
    public Button hostBtn;

    protected override void Start() {
#if DEDICATED_SERVER
        Debug.Log("excape GameLobbyUI");
        //UnityTransport unityTransport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        //unityTransport.SetConnectionData("111.231.29.242", 7777, "0,0,0,0");
        MultiplayerManager.Instance.StartServer();
        ActiveUI(false);
#endif
        hostBtn.onClick.AddListener(() => {
            MultiplayerManager.Instance.StartHost();
            MyGameManager.Instance.ChangeGameState(GameState.WaitingToStart);
            ActiveUI(false);
        });
        serverBtn.onClick.AddListener(() => {
            MultiplayerManager.Instance.StartServer();
            ActiveUI(false);
        });
        clientBtn.onClick.AddListener(() => {
            MultiplayerManager.Instance.StartClient();
            ActiveUI(false);
        });
    }
}
