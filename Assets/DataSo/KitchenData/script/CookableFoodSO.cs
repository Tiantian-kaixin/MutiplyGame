using UnityEngine;

[CreateAssetMenu(fileName = "CookableFoodSO", menuName = "Data/kitchenItem/CookableFoodSO")]
public class CookableFoodSO : PreprocessFoodSO {
    public float cookTime { private set; get; } = 3;
}

