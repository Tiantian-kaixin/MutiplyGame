using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class CookCounterRunState : CookCounterState {

    public CookCounterRunState(StateMachine<IState> stateMachine, CookCounter cookCounter) : base(stateMachine, cookCounter) {
        stateEnum = CookStateEnum.COOKING;
    }

    public override void OnEnter() {
        owner.resetCurTimeServerRpc();
        owner.OnCurTimechange += cutTimeChanged;
    }

    private void cutTimeChanged(float curTime) {
        KitchenObj curKitchenItem = owner.GetCurKitchenItem();
        if (curKitchenItem != null && curKitchenItem.foodData is CookableFoodSO cookableFoodSO && curTime > cookableFoodSO.cookTime) {
            owner.OnCurTimechange -= cutTimeChanged;
            curKitchenItem.DestroySelf();
            owner.spwanItem(cookableFoodSO.processedFoodSO, owner);
            owner.changCookStateServerRpc(CookStateEnum.WARNING);
        }
    }

    public override void OnExist() {
        owner.OnCurTimechange -= cutTimeChanged;
    }

    public override void OnUpdate() {
        owner.updateCurTimeServerRpc(Time.deltaTime);
        KitchenObj curKitchenItem = owner.GetCurKitchenItem();
        if (curKitchenItem == null || curKitchenItem.foodData is not CookableFoodSO cookableFoodSO) {
            owner.changCookStateServerRpc(CookStateEnum.IDLE);
        }
    }
}
