using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public enum KitchenObjEnum {
    Tomato = 0,
    TomatoSlices = 1,
    CheeseBlock = 2,
    Bread = 3,
    Cabbage = 4,
    MeatPattyUncooked = 5,
    CabbageSlices = 6,
    MeatPattyCooked = 7,
    CheeseSlices = 8,
    MeatPattyBurned = 9,
    Plate = 10,
}

public class KitchenObj : NetworkBehaviour {
    [field: SerializeField] public KitchenItemSO foodData { get; private set; }
    [field: SerializeField] public KitchenObjEnum kitchenObjEnum { get; private set; }
    protected IHolder holder;
    private Transform followTarget;

    public void setHolder(IHolder holder) {
        this.holder = holder;
        this.holder?.SetKitchenObj(this);
        followTarget = holder?.GetHoldTransform();
    }

    protected void clearHolder() {
        holder.ClearKitchenObj();
    }

    public void DestroySelf() {
        destroyNetworkObjServerRpc(NetworkObject);
    }

    [ServerRpc(RequireOwnership = false)]
    private void destroyNetworkObjServerRpc(NetworkObjectReference networkObjectReference) {
        networkObjectReference.TryGet(out NetworkObject networkObject);
        destroyNetworkClientRpc(networkObjectReference);
        Destroy(networkObject.gameObject);
    }
    [ClientRpc]
    private void destroyNetworkClientRpc(NetworkObjectReference networkObjectReference) {
        if (IsServer) {
            return;
        }
        holder.ClearKitchenObj();
        holder = null;
    }

    private void LateUpdate() {
        if (followTarget == null) {
            return;
        }
        transform.position = followTarget.transform.position;
        transform.rotation = followTarget.transform.rotation;
    }
}
