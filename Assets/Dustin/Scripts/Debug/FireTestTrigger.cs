using UnityEngine;

public class FireTestTrigger : MonoBehaviour
{
    [SerializeField] private FireBlocker fireBlocker;
    [SerializeField] private Transform firePrefabRoot;
    [SerializeField] private Light fireLight;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("MainCamera") && !other.CompareTag("Player"))
            return;

        if (fireBlocker != null)
            fireBlocker.DisableBlocker();

        if (firePrefabRoot != null)
        {
            var particles = firePrefabRoot.GetComponentsInChildren<ParticleSystem>(true);
            foreach (var ps in particles)
                if (ps) ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }

        if (fireLight == null && firePrefabRoot != null)
            fireLight = firePrefabRoot.GetComponentInChildren<Light>(true);

        if (fireLight != null)
            fireLight.enabled = false;
    }
}
