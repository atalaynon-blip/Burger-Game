using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score;
    private int displayedScore;

    public TextMeshProUGUI scoreText;

    [Header("Count Up Settings")]
    public float countSpeed = 300f;

    Coroutine countRoutine;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        displayedScore = 0;
        UpdateText();
    }

    public void AddScore(int amount)
    {
        score += amount;

        if (countRoutine != null)
            StopCoroutine(countRoutine);

        countRoutine = StartCoroutine(CountUp());
    }

    IEnumerator CountUp()
    {
        while (displayedScore != score)
        {
            int diff = Mathf.Abs(score - displayedScore);

            int step = Mathf.CeilToInt(diff * 6f * Time.deltaTime);

            if (step < 1)
                step = 1;

            if (displayedScore < score)
                displayedScore += step;
            else
                displayedScore -= step;

            // Clamp
            if ((displayedScore > score && displayedScore - step < score) ||
                (displayedScore < score && displayedScore + step > score))
            {
                displayedScore = score;
            }

            UpdateText();
            yield return null;
        }
    }



    void UpdateText()
    {
        scoreText.text = displayedScore.ToString("000");
    }
}
