using UnityEngine;
using UnityEngine.Pool;

[System.Serializable]
public class ObjectsSpawner
{
    public IObjectPool<Product> _pool;
    [SerializeField] private bool collectionCheck = true; // throw if we try to return an existing item
    [SerializeField] private int defaultCapacity = 40;
    [SerializeField] private int maxSize = 100;
    [SerializeField] private Product prefab;

    public ObjectsSpawner() {
        Init();
    }

    public ObjectsSpawner(Product prefab)
    {
        Init();
        this.prefab = prefab;
    }

    private void Init()
    {
        _pool = new ObjectPool<Product>(CreateProduct, OnGetFromPool, OnReleaseToPool, OnDestroyFromPool, collectionCheck, defaultCapacity, maxSize);
    }

    private Product CreateProduct()
    {
        Product product = Product.Instantiate(prefab);
        product.ObjectPool = _pool;
        return product;
    }

    private void OnGetFromPool(Product pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(Product pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    private void OnDestroyFromPool(Product pooledObject)
    {
        Product.Destroy(pooledObject);
    }

    // Ingredient
    public Product GetProduct(Vector3 pos, IngredientData data)
    {
        Product product = _pool.Get();
        product.GetComponent<IIngredient>().Initialise(data);
        product.gameObject.transform.position = pos;
        return product;
    }

}
