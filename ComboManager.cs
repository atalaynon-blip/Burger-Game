using UnityEngine;
using TMPro;
using System.Collections;

public class ComboManager : MonoBehaviour
{
    public static ComboManager Instance;

    public int combo = 0;
    public int maxCombo = 10;
    public float comboResetTime = 2f;

    float lastHitTime;

    public TextMeshProUGUI comboText;

    RectTransform rect;
    Vector2 basePos;

    void Awake()
    {
        Instance = this;
        rect = comboText.rectTransform;
        basePos = rect.anchoredPosition;
        UpdateUI();
    }

    void Update()
    {
        if (combo > 0 && Time.time - lastHitTime > comboResetTime)
        {
            combo = 0;
            UpdateUI();
        }
    }

    void LateUpdate()
    {
        rect.anchoredPosition = basePos;
    }

    public void AddCombo()
    {
        combo = Mathf.Min(combo + 1, maxCombo);
        lastHitTime = Time.time;
        UpdateUI();

        if (combo == maxCombo)
            StartCoroutine(ShakeCombo());
    }

    public int GetMultiplier()
    {
        return Mathf.Max(1, combo);
    }

    void UpdateUI()
    {
        if (combo <= 1)
        {
            comboText.text = "";
            return;
        }

        comboText.text = $"x{combo}";

        float t = (float)combo / maxCombo;
        comboText.color = Color.Lerp(Color.white, Color.red, t);

        float scale = 1f + t * 0.4f;
        comboText.transform.localScale = Vector3.one * scale;
    }

    IEnumerator ShakeCombo()
    {
        float duration = 0.15f;
        float intensity = 8f;

        float timer = 0f;

        while (timer < duration)
        {
            rect.anchoredPosition =
                basePos + Random.insideUnitCircle * intensity;

            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        rect.anchoredPosition = basePos;
    }
}
