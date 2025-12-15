using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    Vector3 originalPos;

    float shakeTime;
    float shakeIntensity;

    float noiseOffset;

    void Awake()
    {
        Instance = this;
        originalPos = transform.localPosition;
        noiseOffset = Random.Range(0f, 100f);
    }

    void Update()
    {
        if (shakeTime > 0f)
        {
            shakeTime -= Time.unscaledDeltaTime;

            float fade = shakeTime;
            float x = (Mathf.PerlinNoise(noiseOffset, Time.unscaledTime * 20f) - 0.5f) * 2f;
            float y = (Mathf.PerlinNoise(noiseOffset + 1f, Time.unscaledTime * 20f) - 0.5f) * 2f;

            Vector3 offset = new Vector3(x, y, 0f) * shakeIntensity * fade;
            transform.localPosition = originalPos + offset;
        }
        else
        {
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                originalPos,
                Time.unscaledDeltaTime * 15f
            );
        }
    }

    public void Shake(float intensity, float duration)
    {
        shakeIntensity = Mathf.Max(shakeIntensity, intensity);
        shakeTime = Mathf.Max(shakeTime, duration);
    }
}
