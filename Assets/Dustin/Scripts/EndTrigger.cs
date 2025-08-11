using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public FadeAndRestart fadeController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera") || other.CompareTag("Player"))
        {
            fadeController.TriggerFade();
        }
    }
}
