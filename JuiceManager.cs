using UnityEngine;

public class JuiceManager : MonoBehaviour
{
    public static JuiceManager Instance;

    public float vfxScale = 1f;
    public float screenShake = 1f;

    void Awake()
    {
        Instance = this;
    }
}
