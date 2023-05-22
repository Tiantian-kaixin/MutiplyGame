using UnityEngine;
using System.Collections;
[CreateAssetMenu(fileName = "KitchenItemSO", menuName = "Data/kitchenItem/KitchenItemSO")]
public class KitchenItemSO : ScriptableObject {
    [field: SerializeField] public GameObject prefab { private set; get; }
    [field: SerializeField] public Sprite icon { private set; get; }
}

