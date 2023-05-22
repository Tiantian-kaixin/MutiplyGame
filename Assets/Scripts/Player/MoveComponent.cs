using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class MoveComponent : PlayerCoreComponent {
    private int walkingID = Animator.StringToHash("walking");

    public MoveComponent(Player player) : base(player) {
        player.AddComponent(this);
    }

    public override void onDisable() {
    }

    public override void OnEnable() {

    }

    public override void Update() {
        Vector3 move = PlayerInputManager.Instance.Move;
        if (move != Vector3.zero) {
            Vector3 moveTo = new Vector3(-move.x, 0, -move.y).normalized;
            float speed = 5;
            Vector3 forward = Vector3.Slerp(transform.forward, moveTo, Time.deltaTime * 10);
            transform.forward = forward;

            if (Physics.CapsuleCast(transform.position,
                transform.position + Vector3.up * 2,
                0.5f, moveTo, speed * Time.deltaTime, player.layerMask)) {
                animator.SetBool(walkingID, false);
                return;
            }
            transform.position += moveTo * speed * Time.deltaTime;
            animator.SetBool(walkingID, true);
        } else {
            animator.SetBool(walkingID, false);
        }
    }

    public override void OnDrawGizmos() {
        base.OnDrawGizmos();
        Gizmos.DrawWireCube(transform.position + Vector3.up, new Vector3(1, 2, 1));
        Gizmos.DrawRay(transform.position + Vector3.up,
            new Vector3(-PlayerInputManager.Instance.Move.x, 0, -PlayerInputManager.Instance.Move.y).normalized);
        Gizmos.DrawWireSphere(transform.position + Vector3.up, 1);
    }
}

