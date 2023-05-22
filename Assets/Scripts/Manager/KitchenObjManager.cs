using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct KitchenSOEnum {
    public KitchenObjEnum kitchenObjEnum;
    public KitchenItemSO kitchenItemSO;
}

public class KitchenObjManager : MonoBehaviour {
    public static KitchenObjManager Instance { private set; get; }
    [SerializeField] public List<KitchenSOEnum> kitchenSOEnumList;

    private void Awake() {
        Instance = this;
    }

    public KitchenItemSO getKitchenSO(KitchenObjEnum kitchenObjEnum) {
        return kitchenSOEnumList.Find(kitchenSOEnum => kitchenSOEnum.kitchenObjEnum == kitchenObjEnum).kitchenItemSO;
    }

    public int GetKichenSOIndex(KitchenItemSO kitchenItemSO) {
        return kitchenSOEnumList.FindIndex(kitchenSOEnum => kitchenSOEnum.kitchenItemSO == kitchenItemSO);
    }

    public KitchenItemSO GetKitchenItemSOByIndex(int index) {
        return kitchenSOEnumList[index].kitchenItemSO;
    }
}
