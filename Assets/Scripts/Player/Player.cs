using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Unity.Netcode;

public partial class Player : NetworkBehaviour {

    public LayerMask layerMask;
    public Transform holdPos;
    [HideInInspector] public Animator animator;
    private List<PlayerCoreComponent> playerCoreComponents = new List<PlayerCoreComponent>();
    private void Awake() {
        animator = GetComponentInChildren<Animator>();
        new MoveComponent(this);
        new SelectCounter(this);
    }
    void Start() {

    }

    private void Update() {
        if (!IsOwner) {
            return;
        }
        playerCoreComponents.ForEach(comp => {
            comp.Update();
        });
    }

    private void OnEnable() {
        playerCoreComponents.ForEach(comp => {
            comp.OnEnable();
        });
    }

    private void OnDisable() {
        playerCoreComponents.ForEach(comp => {
            comp.onDisable();
        });
    }

    public void AddComponent<T>(T playerCoreComponent) where T : PlayerCoreComponent {
        T comp = playerCoreComponents.OfType<T>().FirstOrDefault();
        if (comp != null) {
            return;
        }
        playerCoreComponents.Add(playerCoreComponent);
    }

    public T getComponent<T>() where T : PlayerCoreComponent {
        T comp = playerCoreComponents.OfType<T>().FirstOrDefault();
        if (comp != null) {
            return comp;
        }
        comp = GetComponentInChildren<T>();
        if (comp != null) {
            return comp;
        }
        Debug.LogError("not contain component");
        return null;
    }

    private void OnDrawGizmos() {
        //playerCoreComponents.ForEach(comp => {
        //    comp.OnDrawGizmos();
        //});
    }
}
