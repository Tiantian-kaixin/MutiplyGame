using UnityEngine;
using System.Collections;

public class DeliveryCounter : Counter {
    public override void Inteactive(IHolder holder) {
        if (holder.GetKitchenObj() != null && holder.GetKitchenObj() is PlateObj plateObj) {
            bool isMatch = DeliveryManager.Instance.CheckMatch(plateObj.GetAddedObjEnums());
            if (isMatch) {
                KitchenObj kitchenObj = holder.GetKitchenObj();
                if (kitchenObj != null) {
                    kitchenObj.DestroySelf();
                }
                holder.SetKitchenObj(null);
            }
        }
    }
}

