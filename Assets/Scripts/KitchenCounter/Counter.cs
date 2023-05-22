using UnityEngine;
using System.Collections;
using Unity.Netcode;
using System;
public class Counter : NetworkBehaviour, IInteactable, IHolder {
    [field: SerializeField] public Transform topPosition { private set; get; }
    protected KitchenObj curKitchenObj;
    protected virtual bool canPlace { get { return true; } }
    private Material material;

    public static event Action onAnythingPlaceOnCounter;

    public void Start() {
        Transform kitchenCounter = transform.Find("KitchenCounter");
        if (kitchenCounter != null && kitchenCounter.gameObject.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer)) {
            material = Instantiate(meshRenderer.material);
            meshRenderer.material = material;
        };
    }

    public virtual void Inteactive(IHolder player) {
        if (player.GetKitchenObj() != null) {
            if (GetKitchenObj() != null) {
                // player 填充
                if (player.GetKitchenObj() is PlateObj plateItem && plateItem.AddKitchenItem(GetKitchenObj())) {
                    GetKitchenObj().DestroySelf();
                    ClearKitchenObj();
                }
                // counter 填充
                else if (GetKitchenObj() is PlateObj plateObj && plateObj.AddKitchenItem(player.GetKitchenObj())) {
                    player.GetKitchenObj().DestroySelf();
                    player.ClearKitchenObj();
                }
                // player 交换
                else {
                    switchKitchenObjServerRpc(player.GetNetworkObject(), GetNetworkObject());
                }
            }
            // player 放置
            else if (isHoldable(player.GetKitchenObj().foodData)) {
                putKitchenObjServerRpc(player.GetNetworkObject(), GetNetworkObject());
                onAnythingPlaceOnCounter?.Invoke();
            }
        }
        // player 获取
        else if (this.GetKitchenObj() != null && player.isHoldable(this.GetKitchenObj().foodData)) {
            putKitchenObjServerRpc(GetNetworkObject(), player.GetNetworkObject());
        }
    }

    public virtual KitchenObj GetKitchenObj() {
        return curKitchenObj;
    }

    public virtual bool isHoldable(KitchenItemSO kitchenItemSO = null) {
        return curKitchenObj == null;
    }

    public virtual void SetKitchenObj(KitchenObj kitchenObj) {
        curKitchenObj = kitchenObj;
    }

    public virtual Transform GetHoldTransform() {
        return topPosition;
    }

    public void SetSelected(bool isSelectd) {
        if (material != null) {
            material.color = isSelectd ? new Color(0.7f, 0.7f, 0.7f) : Color.white;
        }
    }

    public NetworkObject GetNetworkObject() {
        return NetworkObject;
    }

    public void ClearKitchenObj() {
        this.curKitchenObj = null;
    }

    public void spwanItem(KitchenItemSO kitchenItemSO, IHolder holder) {
        spwanKitchenObjServerRpc(KitchenObjManager.Instance.GetKichenSOIndex(kitchenItemSO), holder.GetNetworkObject());
    }

    [ServerRpc(RequireOwnership = false)]
    private void spwanKitchenObjServerRpc(int index, NetworkObjectReference networkObjectReference) {
        KitchenItemSO kitchenItemSO = KitchenObjManager.Instance.GetKitchenItemSOByIndex(index);
        NetworkObject spwanObj = Instantiate(kitchenItemSO.prefab)?.GetComponent<NetworkObject>();
        spwanObj.transform.localPosition = Vector3.zero;
        spwanObj.Spawn(true);
        setHolderKitchenObjClientRpc(spwanObj, networkObjectReference);
    }
    [ClientRpc]
    private void setHolderKitchenObjClientRpc(NetworkObjectReference spwanObj, NetworkObjectReference holder) {
        spwanObj.TryGet(out NetworkObject spwanObjNetworkObj);
        holder.TryGet(out NetworkObject holderNetworkObj);
        spwanObjNetworkObj.GetComponent<KitchenObj>()?.setHolder(holderNetworkObj.GetComponent<IHolder>());
    }

    [ServerRpc(RequireOwnership = false)]
    private void putKitchenObjServerRpc(NetworkObjectReference giver, NetworkObjectReference holder) {
        putKitchenObjClientRpc(giver, holder);
    }
    [ClientRpc]
    private void putKitchenObjClientRpc(NetworkObjectReference giver, NetworkObjectReference holder) {
        giver.TryGet(out NetworkObject giverNetworkObj);
        holder.TryGet(out NetworkObject holderNetworkObj);
        IHolder giverHoder = giverNetworkObj.GetComponent<IHolder>();
        IHolder getHolder = holderNetworkObj.GetComponent<IHolder>();
        giverHoder.GetKitchenObj().setHolder(getHolder);
        giverHoder.ClearKitchenObj();
    }

    [ServerRpc(RequireOwnership = false)]
    private void switchKitchenObjServerRpc(NetworkObjectReference giver, NetworkObjectReference holder) {
        switchKitchenObjClientRpc(giver, holder);
    }
    [ClientRpc]
    private void switchKitchenObjClientRpc(NetworkObjectReference giver, NetworkObjectReference holder) {
        giver.TryGet(out NetworkObject giverNetworkObj);
        holder.TryGet(out NetworkObject holderNetworkObj);
        IHolder giverHoder = giverNetworkObj.GetComponent<IHolder>();
        IHolder getHolder = holderNetworkObj.GetComponent<IHolder>();
        KitchenObj kitchenObj = getHolder.GetKitchenObj();
        giverHoder.GetKitchenObj().setHolder(getHolder);
        kitchenObj.setHolder(giverHoder);
    }
}