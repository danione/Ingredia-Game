using UnityEngine;

public class GoldenNuggetsFactory : MonoBehaviour
{
    [SerializeField] private Product goldenNuggets;
    private RectTransform canvas;
    private ObjectsSpawner spawner;
    private Camera _camera;


    private void Start()
    {
        _camera = FindObjectOfType<Camera>();
        spawner = new ObjectsSpawner(goldenNuggets);
        canvas = GetComponent<RectTransform>();
        GameEventHandler.Instance.SpawnGoldenNugget += OnSpawnGoldenNugget;
    }

    private void OnDestroy()
    {
        try
        {
            GameEventHandler.Instance.SpawnGoldenNugget -= OnSpawnGoldenNugget;
        }
        catch { }
    }

    private void OnSpawnGoldenNugget(int value, Vector3 position)
    {
        Product product = spawner._pool.Get();
        product.transform.SetParent(canvas.transform, false);
        product.transform.SetAsFirstSibling();
        product.transform.position = _camera.WorldToScreenPoint(position);
        product.GetComponent<GoldenNuggetUI>().DisplayGoldValue(value);
    }
}
