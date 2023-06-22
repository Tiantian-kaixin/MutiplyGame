using UnityEngine;
using System.Collections;
using Unity.Netcode;
using System;

public class MultiplayerManager : NetworkBehaviour {

    private void Start() {
        Instance = this;
    }

    public static MultiplayerManager Instance;

    private void OnEnable() {

        //NetworkManager.Singleton.ConnectionApprovalCallback += OnConnectionApprovalCallback;
    }

    //private void OnConnectionApprovalCallback(NetworkManager.ConnectionApprovalRequest arg1, NetworkManager.ConnectionApprovalResponse arg2) {
    //    throw new NotImplementedException();
    //}

    private void OnDisable() {
    }

    public void StartServer() {
        //NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
        //NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnectCallback;
        NetworkManager.Singleton.StartServer();
    }

    public void StartHost() {
        NetworkManager.Singleton.StartHost();
    }

    public void StartClient() {
        NetworkManager.Singleton.StartClient();
        MyGameManager.Instance.ChangeGameState(GameState.WaitingToStart);
    }

    private void OnClientConnectedCallback(ulong obj) {
        Debug.Log("OnClientConnectedCallback:" + obj);
    }
    private void OnClientDisconnectCallback(ulong obj) {
        Debug.Log("OnClientDisconnectCallback:" + obj);
    }
}

