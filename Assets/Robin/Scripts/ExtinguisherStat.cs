using UnityEngine;
using System;

public class ExtinguisherStat : MonoBehaviour
{
    [SerializeField] private float maxFoamValue = 100f;
    [SerializeField] private bool debugMode = false;

    private float currentFoamValue;

    [SerializeField] private float interval = 0.1f;
    private float timer = 0f;

    private void Awake()
    {
        currentFoamValue = maxFoamValue;
    }

    private void OnDisable()
    {
        Debug.Log("Extinguisher is out of foam!");
    }

    public void OnExtinguisherUsed(float reducedValue)
    {
        currentFoamValue -= reducedValue;

        if (currentFoamValue <= 0)
        {
            currentFoamValue = 0;
            enabled = false;
        }

        Debug.Log($"{gameObject.name}'s current foam value: {currentFoamValue}");
    }

    //Tests
    private void Update()
    {
        if (!debugMode) return;
        
        timer += Time.deltaTime;

        if (timer >= interval && Input.GetKey(KeyCode.Space))
        {
            timer = 0f;
            OnExtinguisherUsed(1f);
        }
    }
}
