using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    [Header("Fire Particles")]
    public ParticleSystem[] allFireParticles;
    [Header("Fire Health")]
    public float growRate = 0.1f;
    public float maxHealth = 100f;
    private float currentHealth;
    private bool isBeingSprayed = false;
    private List<ParticleInitialValues> initialValues = new List<ParticleInitialValues>();
    [SerializeField] private Collider fireBlock;
    private struct ParticleInitialValues
    {
        public float startSize;
        public float rateOverTime;
    }
    void Start()
    {
        currentHealth = maxHealth;

        foreach (var particles in allFireParticles)
        {
            var main = particles.main;
            var emission = particles.emission;
            initialValues.Add(new ParticleInitialValues
            {
                startSize = main.startSize.constant,
                rateOverTime = emission.rateOverTime.constant
            });
        }
    }
    void Update()
    {
        if (!isBeingSprayed && currentHealth < maxHealth)
        {
            currentHealth += growRate * Time.deltaTime;
        }

        isBeingSprayed = false;
        UpdateFireSize();

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Destroy(fireBlock);
        }
    }
    public void Douse(float amount)
    {
        isBeingSprayed = true;
        currentHealth -= amount;
        currentHealth = Mathf.Max(0, currentHealth);
    }
    void UpdateFireSize()
    {
        float healthPercent = currentHealth / maxHealth;

        for (int i = 0; i < allFireParticles.Length; i++)
        {
            var main = allFireParticles[i].main;
            var emission = allFireParticles[i].emission;

            main.startSize = initialValues[i].startSize * healthPercent;
            emission.rateOverTime = initialValues[i].rateOverTime * healthPercent;
        }
    }
}
