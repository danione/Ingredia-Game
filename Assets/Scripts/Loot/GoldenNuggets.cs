using UnityEngine;

public class GoldenNuggets: FallableObject
{
    [SerializeField] public int Amount { get { return 10; } set { Amount = value; } }
}
