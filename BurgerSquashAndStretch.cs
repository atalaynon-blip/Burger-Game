using UnityEngine;
using System.Collections;

public class BurgerSquashAndStretch : MonoBehaviour
{
    Vector3 originalScale;

    [Header("Squash Settings")]
    public float squashAmount = 0.15f;
    public float bigSquashAmount = 0.25f;
    public float returnSpeed = 12f;

    Coroutine squashRoutine;

    void Awake()
    {
        originalScale = transform.localScale;
    }

    public void Squash(Vector2 impactDir)
    {
        ApplySquash(impactDir, squashAmount);
    }

    public void BigSquash(Vector2 impactDir)
    {
        ApplySquash(impactDir, bigSquashAmount);
    }

    void ApplySquash(Vector2 impactDir, float amount)
    {
        if (squashRoutine != null)
            StopCoroutine(squashRoutine);

        squashRoutine = StartCoroutine(SquashRoutine(impactDir.normalized, amount));
    }

    IEnumerator SquashRoutine(Vector2 dir, float amount)
    {
        Vector3 targetScale = originalScale;


        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            targetScale.x = originalScale.x * (1f - amount);
            targetScale.y = originalScale.y * (1f + amount);
        }
        else
        {
            targetScale.y = originalScale.y * (1f - amount);
            targetScale.x = originalScale.x * (1f + amount);
        }

        transform.localScale = targetScale;

        while (Vector3.Distance(transform.localScale, originalScale) > 0.001f)
        {
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                originalScale,
                Time.deltaTime * returnSpeed
            );
            yield return null;
        }

        transform.localScale = originalScale;
    }
}
