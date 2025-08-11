using UnityEngine;

public class FireBlocker : MonoBehaviour
{
    [SerializeField] private Light fireLight;

    public void DisableBlocker()
    {
        // Disables the blocker
        gameObject.SetActive(false);

        // Turns off the fire light
        if (fireLight != null)
        {
            fireLight.enabled = false;
        }
    }
}
