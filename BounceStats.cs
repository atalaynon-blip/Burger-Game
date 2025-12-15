using UnityEngine;

public class BounceStats : MonoBehaviour
{
    public static BounceStats Instance;

    public int bounceLevel = 0;
    public int maxLevel = 5;

    void Awake()
    {
        Instance = this;
    }
}
