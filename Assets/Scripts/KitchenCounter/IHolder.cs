using UnityEngine;
using System.Collections;
using Unity.Netcode;

public interface IHolder {
    public abstract Transform GetHoldTransform();
    public abstract KitchenObj GetKitchenObj();
    public abstract bool isHoldable(KitchenItemSO kitchenItemSO);
    public abstract void SetKitchenObj(KitchenObj kitchenObj);
    public abstract NetworkObject GetNetworkObject();
    public abstract void ClearKitchenObj();
}

