using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class BurgerBounce : MonoBehaviour
{
    [Header("Bouncing Settings")]
    public PhysicsMaterial2D physicsMaterial;
    public float baseBounciness = 0.3f; 
    public float maxBounciness = 2f;

    [HideInInspector]
    public int currentLevel = 0;
    public int maxLevel = 5;

    Rigidbody2D rb;
    Collider2D col;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        if (physicsMaterial == null)
        {
            physicsMaterial = new PhysicsMaterial2D("BurgerBounce");
            col.sharedMaterial = physicsMaterial;
        }
    }

    void Start()
    {
        ApplyBounce(
            BounceStats.Instance.bounceLevel,
            BounceStats.Instance.maxLevel
        );
    }


    public void ApplyBounce(int level, int maxLevel)
    {
        currentLevel = Mathf.Clamp(level, 0, maxLevel);

        float t = (float)currentLevel / maxLevel;
        physicsMaterial.bounciness = Mathf.Lerp(baseBounciness, maxBounciness, t);

        physicsMaterial.friction = Mathf.Lerp(0.4f, 0.1f, t);
    }
}
