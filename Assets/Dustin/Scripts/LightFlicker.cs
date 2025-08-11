using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light firelight;
    public float minIntensity = 1.0f;
    public float maxIntensity = 2.0f;
    public float flickerSpeed = 10.0f;

    private void Update()
    {
        if (firelight == null)
        {
            return;
        }

        // Smooth random change using Perlin noise
        float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, 0.0f);
        firelight.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }
}
