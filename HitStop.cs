using UnityEngine;
using System.Collections;

public class HitStop : MonoBehaviour
{
    public static HitStop Instance;

    bool isStopping;

    void Awake()
    {
        Instance = this;
    }

    public void Stop(float duration)
    {
        if (isStopping) return;
        StartCoroutine(StopRoutine(duration));
    }

    IEnumerator StopRoutine(float duration)
    {
        isStopping = true;

        float originalTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = originalTimeScale;
        isStopping = false;
    }
}
