using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenNuggetsFactory : MonoBehaviour
{
    [SerializeField] private Transform goldenNuggetObject;

    private void Start()
    {
        GameEventHandler.Instance.SpawnGoldenNugget += OnSpawnGoldenNugget;
    }

    private void OnSpawnGoldenNugget(int value, Vector3 position)
    {
        GameObject goldenNugget = Instantiate(goldenNuggetObject.gameObject, position, Quaternion.identity);
        goldenNugget.GetComponent<GoldenNuggets>().Amount = value;
    }

}
