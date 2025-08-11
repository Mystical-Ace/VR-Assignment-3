using UnityEngine;
using System;
using System.Collections;

public class ExtinguisherStat : MonoBehaviour
{
    [SerializeField] private float maxFoamValue = 100f;

    private float currentFoamValue;
    
    [NonSerialized] public bool isUsable = true;
    [SerializeField] private float interval = 0.1f;
    private float timer = 0f;

    private void Awake()
    {
        currentFoamValue = maxFoamValue;
    }

    public void OnExtinguisherUsed(float reducedValue)
    {
        currentFoamValue -= reducedValue;

        if (currentFoamValue <= 0)
        {
            currentFoamValue = 0;
            isUsable = false;
        }

        Debug.Log($"Extinguisher's current foam value: {currentFoamValue}");
    }

    //Tests
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval && Input.GetKey(KeyCode.Space))
        {
            timer = 0f;
            TestExtinguisherLimitFunctionality();
        }
    }

    public void TestExtinguisherLimitFunctionality()
    {
        if (!isUsable)
            Debug.Log("Extinguisher is out of foam!");
        else
            OnExtinguisherUsed(1f);
    }
}
