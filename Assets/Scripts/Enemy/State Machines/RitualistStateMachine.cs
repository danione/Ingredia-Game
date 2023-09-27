using UnityEngine;

public class RitualistStateMachine : EntityStateMachine
{
    public LookoutState LookoutState;
    public IdleState IdleState;
    public SelectIngredientState SelectIngredientState;

    public RitualistStateMachine(Transform ritualistCircle, Transform ingredientCircle)
    {
        LookoutState = new LookoutState(ritualistCircle);
        IdleState = new IdleState();
        SelectIngredientState = new SelectIngredientState(ingredientCircle);
    }
}
