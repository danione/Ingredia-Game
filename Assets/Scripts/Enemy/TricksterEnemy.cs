using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricksterEnemy : Enemy
{
    [SerializeField] private float maxTimeGathering;
    [SerializeField] private int maxIngredientsNeeded;

    private TricksterStateMachine m_StateMachine;
    private List<GameObject> capturedIngredients = new();

    private void Start()
    {
        m_StateMachine = new TricksterStateMachine(capturedIngredients, maxTimeGathering, maxIngredientsNeeded, transform.GetChild(0), this);
        m_StateMachine.Initialise(m_StateMachine.TricksterGatheringState);
        GameEventHandler.Instance.CapturedNeededIngredients += OnCapturedNeededIngredients;
    }

    private void Update()
    {
        m_StateMachine.CurrentState.Update();
    }

    // Saves references to the currently circulating ingredients
    public void AddCapturedIngredient(GameObject ingredient)
    {
        capturedIngredients.Add(ingredient);
    }
    
    private void OnCapturedNeededIngredients()
    {
        for (int i = capturedIngredients.Count - 1; i >= 0; i--) 
        {
            capturedIngredients[i].GetComponent<FallableObject>().SwapToMove();
            GameEventHandler.Instance.SpawnATricksterProjectileAt(capturedIngredients[i].transform.position, this);
            GameEventHandler.Instance.DestroyObject(capturedIngredients[i]);
            capturedIngredients.RemoveAt(i);
        }

        m_StateMachine.TransitiontTo(m_StateMachine.TricksterThrowingState);
    }
    
    public void AddCapturedProjectile(Product projectile)
    {
        capturedIngredients.Add(projectile.gameObject);
    }

}
