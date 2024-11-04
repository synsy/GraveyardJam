using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightingGlow : MonoBehaviour
{
    Light2D light2D;
    public float minIntensity = 1.0f;  // Minimum intensity
    public float maxIntensity = 4.5f;  // Maximum intensity
    public float pulseSpeed = 2.0f;    // Speed of pulsing

    void Awake()
    {
        light2D = GetComponent<Light2D>();
    }

    void Update()
    {
        // Use Mathf.PingPong to create a smooth oscillation between min and max intensity
        light2D.intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PingPong(Time.time * pulseSpeed, 1.0f));
    }
}
