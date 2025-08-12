using UnityEngine;

public class ExtinguisherSpawner : MonoBehaviour
{
    [SerializeField] private GameObject extinguisher;
    [SerializeField] private Transform spawnTrans;

    private ExtinguisherStat _extinguisherStat;
    private int _extinguisherCount = 0;

    private void Update()
    {
        if (_extinguisherStat == null)
        {
            SpawnExtinguisher();
            return;
        }
        
        if (!_extinguisherStat.enabled)
            SpawnExtinguisher();
    }

    private void SpawnExtinguisher()
    {
        GameObject spawnedObj = Instantiate(extinguisher, spawnTrans.position, Quaternion.identity);
        
        _extinguisherCount++;
        spawnedObj.name = "Extinguisher #" + _extinguisherCount;

        _extinguisherStat = spawnedObj.GetComponent<ExtinguisherStat>();
        if (_extinguisherStat != null)
            Debug.Log("Find a valid extinguisher!");
    }
}
