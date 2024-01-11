using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Scriptable Objects/Ingredient")]
public class IngredientData : ScriptableObject
{
    public string ingredientName;
    public Sprite sprite;
    public Sprite highlightedSprite;
    public float spawnChance;
}
