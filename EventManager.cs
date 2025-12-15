using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour
{
    [Header("Event Prefabs")]
    public GameObject[] eventPrefabs;

    [Header("Spawn Area")]
    public Transform spawnArea;
    public float minX = -5f;
    public float maxX = 5f;
    public float yPos = -3f;

    [Header("Timing")]
    public float spawnInterval = 10f;

    void Start()
    {
        StartCoroutine(SpawnEvents());
    }

    IEnumerator SpawnEvents()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (eventPrefabs.Length == 0) continue;

            GameObject prefab = eventPrefabs[Random.Range(0, eventPrefabs.Length)];
            Vector3 spawnPos = spawnArea.position + new Vector3(Random.Range(minX, maxX), yPos, 0);
            GameObject dip = Instantiate(prefab, spawnPos, Quaternion.identity);
        }
    }
}
