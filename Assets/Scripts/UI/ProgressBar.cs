using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
    public Image image;

    public void UpdateUI(float value) {
        image.fillAmount = value;
    }
    private void OnEnable() {
        image.fillAmount = 0;
    }
}
