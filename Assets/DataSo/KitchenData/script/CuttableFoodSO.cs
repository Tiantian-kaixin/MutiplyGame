using UnityEngine;
[CreateAssetMenu(fileName = "CuttableFoodSO", menuName = "Data/kitchenItem/CuttableFoodSO")]
public class CuttableFoodSO : PreprocessFoodSO {
    public int cutTime { private set; get; } = 3;
}

