using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RitualFactory : MonoBehaviour
{
    private List<IRitual> rituals = new List<IRitual>();
    private List<Type> ritualTypes = new List<Type>();

    private void Awake()
    {
        // Find all types in the assembly that implement IRecipe.
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(IRitual).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

         ritualTypes.AddRange(types);
    }

    private IRitual GetRitual(Type type)
    {
        if(type == null) return null;
        return Activator.CreateInstance(type) as IRitual;
    }

    private void Start()
    {
        foreach (var type in ritualTypes)
        {
            IRitual ritual = GetRitual(type);

            if(ritual != null)
            { 
                rituals.Add(Activator.CreateInstance(type) as IRitual);
            }
        }
    }
}
