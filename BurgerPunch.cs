using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BurgerPunch : MonoBehaviour
{
    Rigidbody2D rb;
    LineRenderer line;

    Vector2 dragDir;
    bool dragging;
    float charge;

    [Header("Line Settings")]
    public float maxLineLength = 3f;
    public Color normalColor = Color.white;
    public Color maxPowerColor = Color.red;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        line = GetComponent<LineRenderer>();

        line.positionCount = 2;
        line.enabled = false;
    }

    void Update()
    {
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 burgerPos = rb.position;

        dragDir = (mouseWorld - burgerPos).normalized;

        if (Input.GetMouseButtonDown(0))
        {
            dragging = true;
            charge = 0f;
            line.enabled = true;
        }

        if (Input.GetMouseButton(0) && dragging)
        {
            charge += Time.deltaTime;
            charge = Mathf.Clamp(charge, 0, PunchStats.Instance.maxCharge);
        }

        if (Input.GetMouseButtonUp(0) && dragging)
        {
            Punch();
            dragging = false;
            line.enabled = false;
            charge = 0f;
        }

        UpdateLine(burgerPos);
    }

    void Punch()
    {
        if (dragDir == Vector2.zero)
            return;

        float forceMultiplier = 1f + charge;

        AudioManager.Instance.PlaySFX(AudioManager.Instance.punch);

        rb.AddForce(
            dragDir *
            PunchStats.Instance.punchForce *
            PunchStats.Instance.punchMultiplier *
            forceMultiplier,
            ForceMode2D.Impulse
        );
    }

    void UpdateLine(Vector2 burgerPos)
    {
        float chargePercent = charge / PunchStats.Instance.maxCharge;
        float lineLength = Mathf.Lerp(0.5f, maxLineLength, chargePercent);

        Vector2 endPos = burgerPos + dragDir * lineLength;

        line.SetPosition(0, burgerPos);
        line.SetPosition(1, endPos);

        line.startColor = Color.Lerp(normalColor, maxPowerColor, chargePercent);
        line.endColor = line.startColor;
    }
}
