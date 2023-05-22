using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using System;

public class CookCounter : Counter, IInteractAlternate {
    public ProgressBar progressBar;
    public event Action<float> OnCurTimechange;
    private StateMachine<IState> stateMachine;
    private NetworkVariable<CookStateEnum> curCookState = new NetworkVariable<CookStateEnum>(CookStateEnum.IDLE);
    // fixme find a better way
    private NetworkVariable<float> curTime = new NetworkVariable<float>(0);

    private void Awake() {
        stateMachine = new StateMachine<IState>();
        stateMachine.AddState(new CookCounterIdelState(stateMachine, this));
        stateMachine.AddState(new CookCounterRunState(stateMachine, this));
        stateMachine.AddState(new CookCounterWarningState(stateMachine, this));
    }

    public override void OnNetworkSpawn() {
        base.OnNetworkSpawn();
        curCookState.OnValueChanged += ChangeCookState;
        curTime.OnValueChanged += ChangeCurTime;
    }

    public override void OnNetworkDespawn() {
        base.OnNetworkDespawn();
        curCookState.OnValueChanged -= ChangeCookState;
        curTime.OnValueChanged -= ChangeCurTime;
    }

    private void ChangeCookState(CookStateEnum preValue, CookStateEnum newValue) {
        stateMachine.ChangeState(stateMachine.GetStates().Find((state => state is CookCounterState cookCounterState && cookCounterState.stateEnum == newValue)));
        ActiveUI(newValue == CookStateEnum.COOKING || newValue == CookStateEnum.WARNING ? true : false);
    }

    private void ChangeCurTime(float previousValue, float newValue) {
        if (curKitchenObj != null && curKitchenObj.foodData is CookableFoodSO cookableFoodSO) {
            float progress = newValue / cookableFoodSO.cookTime;
            if (progress <= 1) {
                UpdateUI(newValue / cookableFoodSO.cookTime);
            }
        }
    }

    private void Update() {
        stateMachine.curState?.OnUpdate();
    }

    public void UpdateUI(float value) {
        progressBar.UpdateUI(value);
    }

    private void ActiveUI(bool active) {
        progressBar.gameObject.SetActive(active);
    }

    public KitchenObj GetCurKitchenItem() {
        return curKitchenObj;
    }

    public void SetCurKitchenItem(KitchenObj kitchenItem) {
        curKitchenObj = kitchenItem;
    }

    [ServerRpc(RequireOwnership = false)]
    public void changCookStateServerRpc(CookStateEnum cookState) {
        curCookState.Value = cookState;
    }

    public override bool isHoldable(KitchenItemSO kitchenItemSO = null) {
        return curKitchenObj == null && kitchenItemSO is CookableFoodSO;
    }

    public override void Inteactive(IHolder player) {
        base.Inteactive(player);
    }

    public void InteactiveAlternate(IHolder inteactiver) {
        if (curKitchenObj != null && curCookState.Value == CookStateEnum.IDLE) {
            changCookStateServerRpc(CookStateEnum.COOKING);
        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void updateCurTimeServerRpc(float time) {
        curTime.Value += time;
        OnCurTimechange?.Invoke(curTime.Value);
    }

    [ServerRpc(RequireOwnership = false)]
    public void resetCurTimeServerRpc() {
        curTime.Value = 0;
        OnCurTimechange?.Invoke(0);
    }
}

