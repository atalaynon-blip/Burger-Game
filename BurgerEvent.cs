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
    public float slowMoTime = 0.1f;
    public float slowMoScale = 0.2f; 
    public float textScaleDuration = 0.3f; 

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

        
        if (impactVFX != null)
            Instantiate(impactVFX, col.transform.position, Quaternion.identity);

        
        if (eventTextPrefab != null)
        {
            TextMeshProUGUI text = Instantiate(
                eventTextPrefab, 
                eventTextPrefab.transform.parent
            );
            text.gameObject.SetActive(true);
            text.transform.parent = GameObject.Find("Canvas").transform;

            text.text = $"{eventName}\n+{scoreValue}";

            Vector3 initialScale = Vector3.one; 
            text.transform.localScale = initialScale;

          
            StartCoroutine(ScaleDownText(text.transform, textScaleDuration, initialScale));
        }

        
        ScoreManager.Instance.AddScore(scoreValue);

        
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

           
            t = t * t * ((2.70158f) * t - 1.70158f);

            textTransform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }

        Destroy(textTransform.gameObject);

        Destroy(gameObject);
    }
}

