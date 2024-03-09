using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPopupUIManager : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private Product healthUIPrefab;
    [SerializeField] private float waitingtimeBetweenShowing;
    private Dictionary<Vector3, float> recentlySpawnedAt = new();
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
        yield return new WaitForSeconds(waitingtimeBetweenShowing);
        float value = recentlySpawnedAt[atPos];
        recentlySpawnedAt.Remove(atPos);

        if (value != 0)
            SpawnAt(atPos, value);
    }
}
