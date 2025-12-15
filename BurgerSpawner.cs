using UnityEngine;

public class BurgerSpawner : MonoBehaviour
{
    public static BurgerSpawner Instance;

    public GameObject burgerPrefab;
    public Transform spawnCenter;
    public int burgerCount = 1;
    public float spawnRadius = 1.5f;

    void Awake()
    {
        Instance = this;
    }

    public void SpawnBurgers()
    {
        Vector2 offset = Random.insideUnitCircle * spawnRadius;
        GameObject burger = Instantiate(burgerPrefab, spawnCenter.position + (Vector3)offset, Quaternion.identity);
        BurgerManager.Instance.RegisterBurger();
    }
}
