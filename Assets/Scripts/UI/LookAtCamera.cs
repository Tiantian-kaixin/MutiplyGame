using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {
    private void LateUpdate() {
        transform.LookAt(Camera.main.transform);
    }
}

