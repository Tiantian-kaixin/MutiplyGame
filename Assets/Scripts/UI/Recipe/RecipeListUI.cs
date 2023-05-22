using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeListUI : MonoBehaviour {
    public GameObject recipeUIPrefab;

    private void Start() {
        DeliveryManager.Instance.OnAddRecipe += AddRecipeData;
        DeliveryManager.Instance.OnRemoveRecipe += RemoveRecipeByIndex;
    }

    private void OnDestroy() {
        DeliveryManager.Instance.OnAddRecipe -= AddRecipeData;
        DeliveryManager.Instance.OnRemoveRecipe -= RemoveRecipeByIndex;
    }
    public void AddRecipeData(RecipeData recipeData) {
        GameObject recipeUiObj = Instantiate(recipeUIPrefab, transform);
        if (recipeUiObj.TryGetComponent<RecipeUI>(out RecipeUI recipeUI)) {
            recipeUI.GenerateRecipeIcon(recipeData);
        }
    }

    public void RemoveRecipeByIndex(int index) {
        Destroy(transform.GetChild(index).gameObject);
    }
}
