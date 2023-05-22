using UnityEngine;

public class FoodSpwanCounter : Counter {
    [field: SerializeField] public KitchenObjEnum kitchenObjEnum { private set; get; }
    protected override bool canPlace { get { return false; } }

    public override void Inteactive(IHolder holder) {
        KitchenItemSO kitchenItemSO = KitchenObjManager.Instance.getKitchenSO(kitchenObjEnum);
        if (holder.isHoldable(kitchenItemSO)) {
            spwanItem(kitchenItemSO, holder);
        } else if (holder.GetKitchenObj() is PlateObj plateItem) {
            plateItem.AddKitchenItem(kitchenObjEnum);
        }
    }
}

