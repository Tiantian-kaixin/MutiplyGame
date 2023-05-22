using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeIcon : MonoBehaviour {
    public Image image;
    public void setSprite(Sprite sprite) {
        image.sprite = sprite;
    }
}
