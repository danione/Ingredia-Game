using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OrderedRecipe : MonoBehaviour, IRecipe<List<IRecipeAction>>
{
    public List<IRecipeAction> ActionContainer => new List<IRecipeAction>();
    protected int currentAction = 0;

    public void Destroy()
    {
        ActionContainer.Clear();
  
    }

    public IRecipeAction GetAction()
    {
        if(currentAction == ActionContainer.Count) { Destroy(); return null; }
        return ActionContainer[currentAction++];
    }

    public void Init(List<IRecipeAction> actions)
    {
        ActionContainer.AddRange(actions);
    }
}
