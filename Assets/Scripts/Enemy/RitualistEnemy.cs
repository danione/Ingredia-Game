using System;
using System.Collections;
using UnityEngine;

public class RitualistEnemy : Enemy
{
    public static int RitualistEnemyCount = 0;
    private Transform player; // Used to detect ingredients near the player
    private Transform ritualistEnemyCapsule;
    [SerializeField] public GameObject[] ingredientTypes;

    private void Awake()
    {
        player = PlayerController.Instance.gameObject.GetComponent<Transform>();
    }

    private void Start()
    {
        ritualistEnemyCapsule = player.GetChild(1); // Second child should always be the ritualist circle
        if(ritualistEnemyCapsule.GetComponent<GetNearbyObjects>() == null)
        {
            Destroy(gameObject);
        }
        ritualistEnemyCapsule.gameObject.SetActive(true);

        RitualistEnemyCount++;
        StartCoroutine(SwapIngredients());
    }

    IEnumerator SwapIngredients()
    {
     
        while(true)
        {
            
            foreach (var ingredient in ritualistEnemyCapsule.GetComponent<GetNearbyObjects>().ingredients)
            {
                SwapIngredient(ingredient.Value);
            }
            yield return new WaitForSeconds(1f);
        }

    }

    private void SwapIngredient(GameObject ingredient)
    {
        if(ingredient == null) { return; }
        Vector3 currentPos = ingredient.transform.position;
        GameObject newIngredient = ingredientTypes[UnityEngine.Random.Range(0, ingredientTypes.Length)];
        
        Destroy(ingredient);

        ingredient = Instantiate(newIngredient, currentPos, Quaternion.identity);
    }

    private void OnDestroy()
    {
        Debug.Log("Destroyed Enemy");
        RitualistEnemyCount--;
        if(RitualistEnemyCount <= 0 && ritualistEnemyCapsule.gameObject != null)
        {
            ritualistEnemyCapsule.gameObject.SetActive(false);
        }
    }
}
