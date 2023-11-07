using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameObjectsHolder : MonoBehaviour
{
    public static GameObjectsHolder instance;
    [SerializeField] public GameObject GoldenNuggets;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
}