using UnityEngine;
using System;

public class ExtinguisherStat : MonoBehaviour
{
    [SerializeField] private float maxFoamValue = 20f;

    private float currentFoamValue;
    
    [NonSerialized] public bool isUsable = true;

    private void Awake()
    {
        currentFoamValue = maxFoamValue;
    }

    public void OnExtinguisherUsed(float reducedValue)
    {
        if (currentFoamValue <= 0)
        {
            isUsable = false;
            return;
        }   

        currentFoamValue -= reducedValue;

        if (currentFoamValue < 0)
            currentFoamValue = 0;

        Debug.Log($"Extinguisher's current foam value: {currentFoamValue}");
    }

    //Tests
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TestExtinguisherLimitFunctionality();
    }

    public void TestExtinguisherLimitFunctionality()
    {
        OnExtinguisherUsed(3f);

        if (!isUsable)
            Debug.Log("Extinguisher is out of foam!");
    }
}
