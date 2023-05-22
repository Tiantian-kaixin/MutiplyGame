using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject {
    [SerializeField] public List<RecipeData> recipeData;
}