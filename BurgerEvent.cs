using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;

public class BurgerEvent : MonoBehaviour
{
    [Header("Settings")]
    public string eventName = "KETCHUP DIP";
    public int scoreValue = 1000;
    public GameObject impactVFX;
    public float slowMoTime = 0.1f;   // duration of slow-motion
    public float slowMoScale = 0.2f;  // time scale during slow-motion
    public float textScaleDuration = 0.3f; // text scaling duration

    [Header("Text Prefab")]
    public TextMeshProUGUI eventTextPrefab;

    bool triggered = false;

    Vector3 startScale;

    void Start()
    {

        startScale = eventTextPrefab.transform.localScale;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (triggered) return;
        if (!col.CompareTag("Burger")) return;

        triggered = true;

        // Spawn VFX at burger position
        if (impactVFX != null)
            Instantiate(impactVFX, col.transform.position, Quaternion.identity);

        // Spawn Event Text
        if (eventTextPrefab != null)
        {
            TextMeshProUGUI text = Instantiate(
                eventTextPrefab, 
                eventTextPrefab.transform.parent
            );
            text.gameObject.SetActive(true);
            text.transform.parent = GameObject.Find("Canvas").transform;

            text.text = $"{eventName}\n+{scoreValue}";

            Vector3 initialScale = Vector3.one; // start large
            text.transform.localScale = initialScale;

            // Start built-in coroutine to scale down
            StartCoroutine(ScaleDownText(text.transform, textScaleDuration, initialScale));
        }

        // Add score
        ScoreManager.Instance.AddScore(scoreValue);

        // Hit slow-motion
        TimeSlow.Instance.Slow(slowMoScale, slowMoTime);
    }

    IEnumerator ScaleDownText(Transform textTransform, float duration, Vector3 startScale)
    {
        Vector3 endScale = Vector3.zero;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            float t = timer / duration;

            // EaseInBack-like curve
            t = t * t * ((2.70158f) * t - 1.70158f);

            textTransform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }

        Destroy(textTransform.gameObject);

        Destroy(gameObject);
    }
}
