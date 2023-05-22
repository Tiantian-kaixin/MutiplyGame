using UnityEngine;
using System.Collections;

public class CookCounterWarningState : CookCounterRunState {

    public CookCounterWarningState(StateMachine<IState> stateMachine, CookCounter cookCounter) : base(stateMachine, cookCounter) {
        stateEnum = CookStateEnum.WARNING;
    }

    private void cutTimeChanged(float curTime) {
        KitchenObj curKitchenItem = owner.GetCurKitchenItem();
        if (curKitchenItem != null && curKitchenItem.foodData is CookableFoodSO cookableFoodSO && curTime > cookableFoodSO.cookTime) {
            owner.OnCurTimechange -= cutTimeChanged;
            curKitchenItem.DestroySelf();
            owner.spwanItem(cookableFoodSO.processedFoodSO, owner);
            owner.changCookStateServerRpc(CookStateEnum.IDLE);
        }
    }
}