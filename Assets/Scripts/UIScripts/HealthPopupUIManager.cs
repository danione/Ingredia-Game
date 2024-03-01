using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPopupUIManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Product healthUIPrefab;
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
        Product newPopup = spawner._pool.Get();
        newPopup.transform.SetParent(canvas.transform, false);
        newPopup.transform.SetAsFirstSibling();
        newPopup.transform.position = _camera.WorldToScreenPoint(atPos);
        newPopup.GetComponent<HealthUIPopup>().DisplayDamage( amount);

    }
}
