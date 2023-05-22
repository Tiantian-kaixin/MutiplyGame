using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecipeUI : MonoBehaviour {
    public GameObject RecipeIconPrefeb;
    public Transform parent;
    public TextMeshProUGUI text;

    public void GenerateRecipeIcon(RecipeData recipeData) {
        recipeData.listSO.ForEach(kitchenItemSO => {
            GameObject recipeIconObj = Instantiate(RecipeIconPrefeb, parent);
            if (recipeIconObj.TryGetComponent<RecipeIcon>(out RecipeIcon recipeIcon)) {
                recipeIcon.setSprite(kitchenItemSO.icon);
            }
        });
        text.SetText(recipeData.recipeName);
    }
}
