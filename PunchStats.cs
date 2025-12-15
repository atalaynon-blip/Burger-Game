using UnityEngine;

public class PunchStats : MonoBehaviour
{
    public static PunchStats Instance;

    public float punchForce = 5f;
    public float punchMultiplier = 1f;
    public float maxCharge = 2f;

    void Awake()
    {
        Instance = this;
    }
}
