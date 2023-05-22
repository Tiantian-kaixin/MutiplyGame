using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct KitchenItemStruct {
    KitchenObjEnum kitchenObjEnum;
    GameObject gameObject;
}
public class PlateCompleteItem : MonoBehaviour {
    [field: SerializeField]
    private List<KitchenItemStruct> kitchenItemStructs =
        new List<KitchenItemStruct>();

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
