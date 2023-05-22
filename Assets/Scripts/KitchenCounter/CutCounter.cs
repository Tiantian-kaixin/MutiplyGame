using Unity.Collections.LowLevel.Unsafe;
using Unity.Netcode;

public class CutCounter : Counter, IInteractAlternate {
    public ProgressBar progressBar;
    private NetworkVariable<int> cutTimes = new NetworkVariable<int>(0);
    private NetworkVariable<int> maxCutTimes = new NetworkVariable<int>(0);

    public override void OnNetworkSpawn() {
        base.OnNetworkSpawn();
        cutTimes.OnValueChanged += cutTimesChange;
    }
    public override void OnNetworkDespawn() {
        base.OnNetworkDespawn();
        cutTimes.OnValueChanged -= cutTimesChange;
    }

    private void cutTimesChange(int preValue, int newValue) {
        float progressValue = maxCutTimes.Value == 0 ? 0 : (float)newValue / maxCutTimes.Value;
        UpdateUI(progressValue);
        updateCuttingTimeServerRpc(newValue);
    }

    [ServerRpc(RequireOwnership = false)]
    private void updateCuttingTimeServerRpc(int newValue) {
        if (newValue >= maxCutTimes.Value) {
            if (curKitchenObj.foodData is CuttableFoodSO curKitchenItemSO) {
                curKitchenObj.DestroySelf();
                KitchenItemSO processedFoodSO = curKitchenItemSO.processedFoodSO;
                spwanItem(processedFoodSO, this);
                resetCuttingTimeServerRpc();
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void changeCuttingTimeServerRpc() {
        cutTimes.Value++;
    }

    [ServerRpc(RequireOwnership = false)]
    private void resetCuttingTimeServerRpc() {
        cutTimes.Value = 0;
    }

    [ServerRpc(RequireOwnership = false)]
    private void setMaxCuttingTimeServerRpc(int maxCuttingTimes) {
        maxCutTimes.Value = maxCuttingTimes;
    }

    public override void Inteactive(IHolder holder) {
        base.Inteactive(holder);
        if (GetKitchenObj() == null) {
            ActiveUI(false);
        } else if (curKitchenObj.foodData is CuttableFoodSO curKitchenItemSO) {
            setMaxCuttingTimeServerRpc(curKitchenItemSO.cutTime);
            resetCuttingTimeServerRpc();
        }
    }

    public void InteactiveAlternate(IHolder inteactive) {
        if (curKitchenObj != null && curKitchenObj.foodData is CuttableFoodSO curKitchenItemSO) {
            changeCuttingTimeServerRpc();
        }
    }
    private void UpdateUI(float value) {
        ActiveUI(value != 0 && value != 1);
        progressBar.UpdateUI(value);
    }

    private void ActiveUI(bool active) {
        progressBar.gameObject.SetActive(active);
    }

    public override bool isHoldable(KitchenItemSO kitchenItemSO = null) {
        return curKitchenObj == null && kitchenItemSO is CuttableFoodSO;
    }
}

