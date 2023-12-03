using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public static Constants Instance;
    [SerializeField] private IngredientsFactory ingredientsFactory;
    [SerializeField] public DifficultyModifiers DifficultyModifiers;


    public int IngredientsCount { get { return ingredientsFactory.GetCountOfIngredients(); } }



    private void Start()
    {
        if (Instance == null) { Instance = this; }
        else Destroy(gameObject);
    }
}

[System.Serializable]
public class DifficultyModifiers {
    [SerializeField] private float countWeight;
    public float CountWeight { get { return countWeight; } }
    [SerializeField] private float quantityWeight;
    public float QuantityWeight { get { return quantityWeight; } }
    [SerializeField] private float timeWeight;
    public float TimeWeight { get { return timeWeight; } }
}
