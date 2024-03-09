using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPopupUIManager : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private Product healthUIPrefab;
    [SerializeField] private float waitingtimeBetweenShowing;
    private Dictionary<GameObject, float> recentlySpawnedAt = new();
    private Dictionary<GameObject, float> accumulatedDamage = new();
    private RectTransform canvas;

    private ObjectsSpawner spawner;

    private void Awake()
    {
        spawner = new ObjectsSpawner(healthUIPrefab);
        canvas = GetComponent<RectTransform>();
        _camera = FindObjectOfType<Camera>();
    }

    private void Start()
    {
        GameEventHandler.Instance.TookDamage += OnTakenDamage;
    }

    private void OnDestroy()
    {
        try
        {
            GameEventHandler.Instance.TookDamage -= OnTakenDamage;
            StopAllCoroutines();

        } catch { }
    }

    private void OnTakenDamage(GameObject obj, float amount)
    {
        if (recentlySpawnedAt.ContainsKey(obj) && accumulatedDamage.ContainsKey(obj))
        {
            accumulatedDamage[obj] += amount;
            return;
        }

        recentlySpawnedAt[obj] = 0;
        accumulatedDamage[obj] = amount;

        SpawnAt(obj.transform.position, amount);

        StartCoroutine(SpawnAfterDelay(obj));
    }

    private void SpawnAt(Vector3 atPos, float amount)
    {
        Product newPopup = spawner._pool.Get();
        newPopup.transform.SetParent(canvas.transform, false);
        newPopup.transform.SetAsFirstSibling();
        newPopup.transform.position = _camera.WorldToScreenPoint(atPos);
        newPopup.GetComponent<HealthUIPopup>().DisplayDamage(amount);
    }

    private IEnumerator SpawnAfterDelay(GameObject obj)
    {
        while(obj != null && obj.activeSelf)
        {
            yield return new WaitForSeconds(waitingtimeBetweenShowing);

            float totalDamage = 0;
            if (accumulatedDamage.ContainsKey(obj))
            {
                totalDamage = accumulatedDamage[obj];
                if (totalDamage != 0 && obj != null && obj.activeSelf)
                {
                    SpawnAt(obj.transform.position, totalDamage);
                }
                // Reset accumulated damage for the object
                accumulatedDamage[obj] = 0;
            }
        }
        if(recentlySpawnedAt.ContainsKey(obj))
        {
            recentlySpawnedAt.Remove(obj);
        }
        if(accumulatedDamage.ContainsKey(obj))
        {
            accumulatedDamage.Remove(obj);
        }
    }
}
