using UnityEngine;
using System.Collections;
using TMPro;

public class BurgerManager : MonoBehaviour
{
    public static BurgerManager Instance;

    [Header("UI")]
    public GameObject noBurgersPanel;

    public TextMeshProUGUI Text1;
    public TextMeshProUGUI Text2;

    [Header("Narrator")]
    public AudioSource narratorSource;
    public AudioClip noBurgerLine;

    [Header("Spawning")]
    public BurgerSpawner spawner;
    public float delayBeforeNewBurger = 2f;

    int burgersAlive;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        burgersAlive = FindObjectsOfType<BurgerHealth>().Length;
    }

    public void RegisterBurger()
    {
        burgersAlive++;
    }

    public void OnBurgerDestroyed()
    {
        burgersAlive--;

        if (burgersAlive <= 0)
        {
            StartCoroutine(NoBurgersRoutine());
        }
    }

    IEnumerator NoBurgersRoutine()
    {
        // Show UI
        noBurgersPanel.SetActive(true);

        yield return new WaitForSeconds(3);

        Text1.text = "";
        Text2.text = "...More BURGERS.";

        yield return new WaitForSeconds(2);

        // Spawn new burger
        spawner.SpawnBurgers();

        // Hide UI
        noBurgersPanel.SetActive(false);
    }
}
