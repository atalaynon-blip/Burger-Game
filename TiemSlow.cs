using UnityEngine;
using System.Collections;

public class TimeSlow : MonoBehaviour
{
    public static TimeSlow Instance;

    void Awake()
    {
        Instance = this;
    }

    public void Slow(float slowAmount, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(SlowRoutine(slowAmount, duration));
    }

    IEnumerator SlowRoutine(float slowAmount, float duration)
    {
        Time.timeScale = slowAmount;
        Time.fixedDeltaTime = 0.02f * slowAmount;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
}
