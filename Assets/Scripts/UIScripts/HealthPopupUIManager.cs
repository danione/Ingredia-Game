using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPopupUIManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Product healthUIPrefab;
    private Dictionary<Vector3, float> recentlySpawnedAt = new();
    private RectTransform canvas;

    private ObjectsSpawner spawner;

    private void Awake()
    {
        spawner = new ObjectsSpawner(healthUIPrefab);
        canvas = GetComponent<RectTransform>();
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

        } catch { }
    }

    private void OnTakenDamage(Vector3 atPos, float amount)
    {
        if (recentlySpawnedAt.ContainsKey(atPos))
        {
            recentlySpawnedAt[atPos] += amount;
            return;
        }

        SpawnAt(atPos, amount);

        recentlySpawnedAt[atPos] = 0;
        StartCoroutine(SpawnAgain(atPos));
    }

    private void SpawnAt(Vector3 atPos, float amount)
    {
        Product newPopup = spawner._pool.Get();
        newPopup.transform.SetParent(canvas.transform, false);
        newPopup.transform.SetAsFirstSibling();
        newPopup.transform.position = _camera.WorldToScreenPoint(atPos);
        newPopup.GetComponent<HealthUIPopup>().DisplayDamage(amount);
    }

    private IEnumerator SpawnAgain(Vector3 atPos)
    {
        yield return new WaitForSeconds(0.3f);
        float value = recentlySpawnedAt[atPos];
        recentlySpawnedAt.Remove(atPos);

        if (value != 0)
            SpawnAt(atPos, value);
    }
}
