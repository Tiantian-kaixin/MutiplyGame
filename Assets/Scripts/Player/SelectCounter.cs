using UnityEngine;
using System.Collections;

public class SelectCounter : PlayerCoreComponent {
    private Counter selectCounter;
    public SelectCounter(Player player) : base(player) {
        player.AddComponent(this);
    }

    private void OnInteractPerform() {
        if (selectCounter != null && selectCounter is IInteactable inteactable) {
            inteactable.Inteactive(player);
        }
    }

    private void OnInteractAlternatePerform() {
        if (selectCounter != null && selectCounter is IInteractAlternate interactAlternate) {
            interactAlternate.InteactiveAlternate(player);
        }
    }

    public override void Update() {
        selectCounter?.SetSelected(false);
        if (Physics.Raycast(transform.position, transform.forward,
            out RaycastHit raycastHit, 1, player.layerMask)) {
            if (raycastHit.transform.TryGetComponent<Counter>(out Counter counter)) {
                selectCounter = counter;
            } else {
                selectCounter = null;
            }
        } else {
            selectCounter = null;
        }
        selectCounter?.SetSelected(true);
    }

    public override void OnEnable() {
        PlayerInputManager.Instance.OnInteractPerform += OnInteractPerform;
        PlayerInputManager.Instance.OnInteractAlternatePerform += OnInteractAlternatePerform;
    }

    public override void onDisable() {
        PlayerInputManager.Instance.OnInteractPerform -= OnInteractPerform;
        PlayerInputManager.Instance.OnInteractAlternatePerform -= OnInteractAlternatePerform;
    }
}

