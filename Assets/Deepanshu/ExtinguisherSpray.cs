using UnityEngine;

public class ExtinguisherSpray : MonoBehaviour
{
    public Transform nozzle;
    public float sprayDistance = 10f;
    public float douseRate = 25f;
    public void PerformSpray()
    {
        if (Physics.Raycast(nozzle.position, nozzle.forward, out RaycastHit hit, sprayDistance))
        {
            FireController fire = hit.collider.GetComponent<FireController>();
            if (fire != null)
            {
                fire.Douse(douseRate * Time.deltaTime); 
            }
        }
    }
}
