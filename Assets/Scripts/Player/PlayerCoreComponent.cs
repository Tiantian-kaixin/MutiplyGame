using UnityEngine;
using System.Collections;

public abstract class PlayerCoreComponent : CoreComponent<Player> {
    protected Transform transform;
    protected Animator animator;

    public PlayerCoreComponent(Player player) {
        this.player = player;
        this.transform = player.transform;
        this.animator = player.animator;
    }

    public virtual void OnDrawGizmos() {

    }
}

