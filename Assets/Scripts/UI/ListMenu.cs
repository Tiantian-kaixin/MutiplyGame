using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor.Rendering;

public class ListMenu : MonoBehaviour {
    public void addItem(Sprite sprite) {
        GameObject gameObject = new GameObject(sprite.name);
        RectTransform trans = gameObject.AddComponent<RectTransform>();
        Image image = gameObject.AddComponent<Image>();
        image.sprite = sprite;
        gameObject.transform.SetParent(transform);
        gameObject.transform.localPosition = Vector3.zero;
    }

    public void ActiveUI(bool isActive) {
        gameObject.SetActive(isActive);
    }
}

