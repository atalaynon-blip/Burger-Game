using UnityEngine;

public class RainManager : MonoBehaviour
{
    public static RainManager Instance;

    [Header("Rain Settings")]
    public GameObject rainPrefab;         // ParticleSystem prefab
    public Transform spawnPoint;          // Where the rain spawns
    public int rainLevel = 0;             // Number of upgrades bought
    public int maxLevel = 5;

    private ParticleSystem rainParticles;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (rainPrefab != null)
        {
            GameObject rainObj = Instantiate(rainPrefab, spawnPoint.position, spawnPoint.rotation);
            rainParticles = rainObj.GetComponent<ParticleSystem>();
            rainParticles.Stop();
        }
    }

    public void UpgradeRain()
    {
        if (rainLevel >= maxLevel) return;

        rainLevel++;
        UpdateRain();
    }

    void UpdateRain()
    {
        if (rainParticles == null) return;

        var main = rainParticles.main;
        var emission = rainParticles.emission;

        main.startSpeed = 20f + rainLevel;
        emission.rateOverTime = 10f + rainLevel * 10f;

        if (!rainParticles.isPlaying)
            rainParticles.Play();
    }

    public float GetScoreBonus()
    {
        return rainLevel * 0.5f;
    }
}
