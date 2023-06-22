using UnityEngine;
using System.Collections.Generic;
using Unity.Netcode;
using System;

public class DeliveryManager : NetworkBehaviour {
    [HideInInspector] public static DeliveryManager Instance { get; private set; }
    public RecipeSO recipeSO;
    public float queueTime = 3;
    public int maxRecipeNum = 5;
    private float curTime = 0;
    private List<RecipeData> recipeDatas = new List<RecipeData>();

    public event Action OnCompleteOrder;
    public event Action OnFailOrder;
    public event Action<RecipeData> OnAddRecipe;
    public event Action<int> OnRemoveRecipe;

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if (!MyGameManager.Instance.IsStateRunning<GamePlayingState>()) {
            return;
        }
        if (maxRecipeNum <= recipeDatas.Count) {
            curTime = 0;
            return;
        }
        curTime += Time.deltaTime;
        if (curTime >= queueTime) {
            curTime = 0;
            int index = UnityEngine.Random.Range(0, recipeSO.recipeData.Count);
            GenerateRecipeClientRpc(index);
        }
    }

    public bool CheckMatch(List<KitchenObjEnum> kitchenObjEnums) {
        bool isMatch = false;
        for (int i = 0; i < recipeDatas.Count; i++) {
            RecipeData recipeData = recipeDatas[i];
            if (recipeData.listSO.Count == kitchenObjEnums.Count) {
                isMatch = true;
                kitchenObjEnums.ForEach(kitchenObjEnum => {
                    if (recipeData.listSO.Find(KitchenItemSO => KitchenItemSO == KitchenObjManager.Instance.getKitchenSO(kitchenObjEnum)) == null) {
                        isMatch = false;
                        return;
                    }
                });
            }
            if (isMatch) {
                completeOrderServerRpc(i);
                break;
            }
        };
        if (!isMatch) {
            failedOrderServerRpc();
        }
        return isMatch;
    }

    [ClientRpc]
    private void GenerateRecipeClientRpc(int index) {
        var recipeData = recipeSO.recipeData[index];
        recipeDatas.Add(recipeData);
        OnAddRecipe?.Invoke(recipeData);
    }
    [ServerRpc(RequireOwnership = false)]
    private void completeOrderServerRpc(int index) {
        completeOrderClientRpc(index);
    }
    [ClientRpc]
    private void completeOrderClientRpc(int index) {
        recipeDatas.RemoveAt(index);
        OnRemoveRecipe?.Invoke(index);
        OnCompleteOrder?.Invoke();
    }
    [ServerRpc(RequireOwnership = false)]
    private void failedOrderServerRpc() {
        failedOrderClientRpc();
    }

    [ClientRpc]
    private void failedOrderClientRpc() {
        OnFailOrder?.Invoke();
    }
}

