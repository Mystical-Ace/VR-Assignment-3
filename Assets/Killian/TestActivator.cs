using UnityEngine;

public class TestActivator : MonoBehaviour
{
    public ParticleSystem particleSystem;  // Assign your particle system here
    public KeyCode activationKey = KeyCode.Space;  // Change to any key you want

    void Update()
    {
        // Start particles when key is pressed down
        if (Input.GetKeyDown(activationKey))
        {
            particleSystem.Play();
        }

        // Stop particles when key is released
        if (Input.GetKeyUp(activationKey))
        {
            particleSystem.Stop();
        }
    }
}
