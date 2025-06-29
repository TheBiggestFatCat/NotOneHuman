using UnityEngine;

public class FrameRateLimiter : MonoBehaviour
{
    [SerializeField] private int targetFrameRate = 60;
    
    void Start()
    {
        Application.targetFrameRate = targetFrameRate;
    }
}