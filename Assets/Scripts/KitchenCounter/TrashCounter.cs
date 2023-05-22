public class TrashCounter : Counter, IInteractAlternate {

    public override void Inteactive(IHolder holder) {
        InteactiveAlternate(holder);
    }

    public void InteactiveAlternate(IHolder player) {
        KitchenObj kitchenObj = player.GetKitchenObj();
        if (kitchenObj != null) {
            kitchenObj.DestroySelf();
        }
        player.SetKitchenObj(null);
    }
}

