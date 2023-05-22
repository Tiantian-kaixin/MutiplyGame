using UnityEngine;
using System.Collections;
using Unity.Netcode;

public partial class Player : IHolder {

    private KitchenObj holdObject;

    public bool isHoldable(KitchenItemSO kitchenItemSO) {
        return holdObject == null;
    }

    public KitchenObj GetKitchenObj() {
        return holdObject;
    }

    public Transform GetHoldTransform() {
        return holdPos;
    }

    public void SetKitchenObj(KitchenObj kitchenObj) {
        holdObject = kitchenObj;
    }

    public NetworkObject GetNetworkObject() {
        return NetworkObject;
    }

    public void ClearKitchenObj() {
        holdObject = null;
    }
}

