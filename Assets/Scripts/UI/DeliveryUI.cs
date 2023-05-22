using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryUI : MonoBehaviour {
    public GameObject rightImg;
    public GameObject wrongImg;

    private void Start() {
        DeliveryManager.Instance.OnCompleteOrder += onCompleteOrder;
        DeliveryManager.Instance.OnFailOrder += onFailedOrder;
    }

    private void OnDestroy() {
        DeliveryManager.Instance.OnCompleteOrder -= onCompleteOrder;
        DeliveryManager.Instance.OnFailOrder -= onFailedOrder;
    }

    private void onCompleteOrder() {
        gameObject.SetActive(true);
        StartCoroutine(showResult(true));
    }
    private void onFailedOrder() {
        gameObject.SetActive(true);
        StartCoroutine(showResult(false));
    }

    private IEnumerator showResult(bool isRight) {
        if (isRight) {
            rightImg.SetActive(true);
        } else {
            wrongImg.SetActive(true);
        }
        yield return new WaitForSeconds(2);
        rightImg.SetActive(false);
        wrongImg.SetActive(false);
        gameObject.SetActive(false);
    }
}
