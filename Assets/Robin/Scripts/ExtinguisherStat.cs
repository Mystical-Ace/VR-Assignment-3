using UnityEngine;
using System;
using UnityEngine.InputSystem;
using System.Buffers;

public class ExtinguisherStat : MonoBehaviour
{
    [SerializeField] private float maxFoamValue = 1f;
    //[SerializeField] private bool debugMode = false;
    public bool sprayOn;
    [Header("Input")]

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

    public void SetSpray(bool spray)
    {
        sprayOn = spray;
    }
    //Tests
    private void Update()
    {
        if(sprayOn == true)
        {
            ReduceFoam();
        }
    }
    public void ReduceFoam()
    {
        //if (!debugMode) return;
        
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            timer = 0f;
            OnExtinguisherUsed(1f);
        }
    }
}
