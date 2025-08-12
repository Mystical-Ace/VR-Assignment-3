using UnityEngine;

public class ExtinguisherSpray : MonoBehaviour
{
    public Transform nozzle;
    public float sprayDistance = 10f;
    public float douseRate = 25f;
    public bool sprayActive;
    public float NozOffset = .5f;

    public void SetExSpray(bool Exspray)
    {
        sprayActive = Exspray;
    }
    //Tests
    private void Update()
    {
        if (sprayActive == true)
        {
            PerformSpray();
        }
    }
    public void PerformSpray()
    {
        if (Physics.Raycast(nozzle.position + -nozzle.up * NozOffset, -nozzle.up, out RaycastHit hit, sprayDistance))
        {
            FireController fire = hit.collider.GetComponent<FireController>();
            if (fire != null)
            {
                fire.Douse(douseRate * Time.deltaTime); 
            }
        }
    }
}
