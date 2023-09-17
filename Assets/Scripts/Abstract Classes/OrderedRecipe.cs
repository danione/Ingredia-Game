using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OrderedRecipe : MonoBehaviour, IRecipe<List<IRecipeAction>>
{
    public List<IRecipeAction> ActionContainer => new List<IRecipeAction>();
    protected int currentAction = 0;

    public virtual void Destroy()
    {
        ActionContainer.Clear();
  
    }

    public virtual IRecipeAction GetAction()
    {
        if(currentAction == ActionContainer.Count) { Destroy(); return null; }
        return ActionContainer[currentAction++];
    }

    public virtual void Init(List<IRecipeAction> actions)
    {
        ActionContainer.AddRange(actions);
    }

    public bool IsAllCompleted()
    {
        throw new System.NotImplementedException();
    }

    public void StartRecipe()
    {
        throw new System.NotImplementedException();
    }
}
