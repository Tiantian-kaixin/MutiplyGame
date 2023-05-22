using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

[Serializable]
public struct PlateCompleteObj {
    public KitchenObjEnum kitchenObjEnum;
    public GameObject gameObject;
}

public class PlateObj : KitchenObj {
    public List<PlateCompleteObj> plateCompleteObjs = new List<PlateCompleteObj>();
    public ListMenu listMenu;
    private List<KitchenObjEnum> addedObjEnums = new List<KitchenObjEnum>();

    public bool AddKitchenItem(KitchenObj item) {
        if (item != null && IsPlateAddKitchenObj(item.kitchenObjEnum)) {
            addedObjEnums.Add(item.kitchenObjEnum);
            activeGameObj(item.kitchenObjEnum);
            updateUI(item.foodData.icon);
            return true;
        }
        return false;
    }

    public bool AddKitchenItem(KitchenObjEnum kitchenObjEnum) {
        if (IsPlateAddKitchenObj(kitchenObjEnum)) {
            addedObjEnums.Add(kitchenObjEnum);
            activeGameObj(kitchenObjEnum);
            updateUI(KitchenObjManager.Instance.getKitchenSO(kitchenObjEnum).icon);
            return true;
        }
        return false;
    }

    public bool IsPlateAddKitchenObj(KitchenObjEnum kitchenObjEnum) {
        return IsPlateCompleteItem(kitchenObjEnum) && !HasAddKitchenItem(kitchenObjEnum);
    }

    private bool IsPlateCompleteItem(KitchenObjEnum kitchenObjEnum) {
        return plateCompleteObjs.Any(plateCompleteObj => plateCompleteObj.kitchenObjEnum == kitchenObjEnum);
    }

    private bool HasAddKitchenItem(KitchenObjEnum kitchenObjEnum) {
        return addedObjEnums.Any(hasAddItem => kitchenObjEnum == hasAddItem);
    }

    public void activeGameObj(KitchenObjEnum kitchenObjEnum) {
        plateCompleteObjs.ForEach(plateCompleteObj => {
            if (plateCompleteObj.kitchenObjEnum == kitchenObjEnum) {
                plateCompleteObj.gameObject.SetActive(true);
                return;
            }
        });
    }

    public List<KitchenObjEnum> GetAddedObjEnums() {
        return addedObjEnums;
    }

    private void updateUI(Sprite sprite) {
        listMenu.addItem(sprite);
    }
}

