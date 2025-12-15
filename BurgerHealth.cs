using UnityEngine;

public class BurgerHealth : MonoBehaviour
{
    public float maxHealth = 20f;
    private float currentHealth;

    [Header("Visuals")]
    public SpriteRenderer spriteRenderer;
    public Color fullHealthColor = Color.white;
    public Color lowHealthColor = Color.red;

    public GameObject deathVFX;

    void Start()
    {
        currentHealth = maxHealth;

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        currentHealth = Mathf.Max(currentHealth, 0f);

        UpdateColor();

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void UpdateColor()
    {
        float t = 1f - (currentHealth / maxHealth);
        spriteRenderer.color = Color.Lerp(fullHealthColor, lowHealthColor, t);
    }

    void Die()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.burgerDestroy);
        BurgerManager.Instance.OnBurgerDestroyed();

        Instantiate(deathVFX, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
