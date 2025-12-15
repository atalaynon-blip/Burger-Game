using UnityEngine;

public class BurgerCollision : MonoBehaviour
{
    public GameObject hitVFX;

    public int baseScore = 10;
    public float minImpactForce = 2.5f;
    public float bigHitThreshold = 6f;

    public float hitStopDuration = 0.03f;
    public float bigHitStopDuration = 0.06f;
    BurgerSquashAndStretch squash;

    BurgerHealth health;

    void Start()
    {
        
        health = GetComponent<BurgerHealth>();

        squash = GetComponent<BurgerSquashAndStretch>();

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Wall"))
            return;

        float impact = collision.relativeVelocity.magnitude;

        if (impact < minImpactForce)
            return;

        Vector2 hitPoint = collision.GetContact(0).point;

        GameObject vfx = Instantiate(hitVFX, hitPoint, Quaternion.identity);
        vfx.transform.localScale *= JuiceManager.Instance.vfxScale;

        if (impact > bigHitThreshold)
        {
            ComboManager.Instance.AddCombo();
            AudioManager.Instance.PlaySFX(AudioManager.Instance.hitBig, 0.9f, 1.1f);
        }
        else
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.hitSmall);
        }

        int comboMultiplier = ComboManager.Instance.GetMultiplier();
        int effectiveCombo = Mathf.CeilToInt(comboMultiplier * 0.75f);

        int score = Mathf.RoundToInt(
            Mathf.Sqrt(impact) * (baseScore + RainManager.Instance.GetScoreBonus()) * effectiveCombo
        );
        ScoreManager.Instance.AddScore(score);
        Vector2 impactDir = collision.relativeVelocity.normalized;

        if (impact >= bigHitThreshold)
        {
            squash.BigSquash(impactDir);
        }
        else
        {
            squash.Squash(impactDir);
        }


        CameraShake.Instance.Shake(
            0.5f,
            0.1f
        );

        float damage = impact * impact * 0.4f;

        if (impact >= bigHitThreshold)
            damage *= 1.5f;

        if (impact >= bigHitThreshold)
            damage *= 1.5f;

        health.TakeDamage(damage);


        if (impact >= bigHitThreshold)
        {
            HitStop.Instance.Stop(bigHitStopDuration);
        }
        else
        {
            HitStop.Instance.Stop(hitStopDuration);
        }

        if (impact >= bigHitThreshold)
        {
            TimeSlow.Instance.Slow(0.2f, 0.08f);
        }
    }
}

