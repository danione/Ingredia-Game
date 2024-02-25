using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricksterEnemy : Enemy
{
    [SerializeField] private float maxTimeGathering;
    [SerializeField] private int maxIngredientsNeeded;

    private TricksterStateMachine m_StateMachine;
    private List<GameObject> capturedIngredients = new();
    private Dictionary<GameObject, int> ingredients = new();

    private void Start()
    {
        m_StateMachine = new TricksterStateMachine(capturedIngredients, maxTimeGathering, maxIngredientsNeeded, transform.GetChild(0), this, enemyData);
        m_StateMachine.Initialise(m_StateMachine.TricksterGatheringState);
        GameEventHandler.Instance.CapturedNeededIngredients += OnCapturedNeededIngredients;
        GameEventHandler.Instance.FinishedThrowingTrickster += OnFinishedThrowing;
        GameEventHandler.Instance.DestroyedObject += OnDestroyedObject;
    }

    private void Update()
    {
        m_StateMachine.CurrentState.Update();
    }

    // Saves references to the currently circulating ingredients
    public void AddCapturedIngredient(GameObject ingredient)
    {
        capturedIngredients.Add(ingredient);
        Debug.Log(capturedIngredients.Count);
        ingredients[ingredient] = capturedIngredients.Count - 1; 
    }

    private void OnDestroyedObject(GameObject obj)
    {
        if(ingredients.ContainsKey(obj))
        {
            capturedIngredients.Remove(obj);
            ingredients.Remove(obj);
        }
    }
    
    private void OnCapturedNeededIngredients()
    {
        for (int i = capturedIngredients.Count - 1; i >= 0; i--) 
        {
            capturedIngredients[i].GetComponent<FallableObject>().SwapToMove();
            ingredients.Remove(capturedIngredients[i]);
            GameEventHandler.Instance.SpawnATricksterProjectileAt(capturedIngredients[i].transform.position, this);
            GameEventHandler.Instance.DestroyObject(capturedIngredients[i]);
            capturedIngredients.RemoveAt(i);
        }

        m_StateMachine.TransitiontTo(m_StateMachine.TricksterThrowingState);
    }

    private void OnFinishedThrowing()
    {
        m_StateMachine.TransitiontTo(m_StateMachine.TricksterGatheringState);
    }
    
    public void AddCapturedProjectile(Product projectile)
    {
        capturedIngredients.Add(projectile.gameObject);
        ingredients[projectile.gameObject] = capturedIngredients.Count - 1;
    }
}
