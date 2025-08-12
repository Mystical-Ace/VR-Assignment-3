using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class TwoHandedGrabController : MonoBehaviour
{
    [Header("Interactables")]
    public XRGrabInteractable nozzleInteractable;
    public ExtinguisherSpray extinguisherSprayScript;
    [Header("Input")]
    public InputActionReference sprayAction; 
    [Header("Effects")]
    public ParticleSystem sprayParticles;
    private IXRSelectInteractor primaryInteractor;
    private IXRSelectInteractor secondaryInteractor;
    private XRGrabInteractable bodyInteractable;
    void Start()
    {
        bodyInteractable = GetComponent<XRGrabInteractable>();
        bodyInteractable.selectEntered.AddListener(OnBodyGrabbed);
        bodyInteractable.selectExited.AddListener(OnBodyReleased);
        nozzleInteractable.selectEntered.AddListener(OnNozzleGrabbed);
        nozzleInteractable.selectExited.AddListener(OnNozzleReleased);
    }
    void Update()
    {
        bool isSpraying = false;
        if (secondaryInteractor != null)
        {
            if (primaryInteractor != null)
            {
                transform.rotation = Quaternion.LookRotation(secondaryInteractor.transform.position - primaryInteractor.transform.position);
            }
            if (sprayAction != null && sprayAction.action.ReadValue<float>() > 0.1f)
            {
                extinguisherSprayScript.PerformSpray();
                isSpraying = true;
            }
        }
        if (isSpraying)
        {
            if (sprayParticles != null && !sprayParticles.isPlaying)
            {
                sprayParticles.Play();
            }
        }
        else
        {
            if (sprayParticles != null && sprayParticles.isPlaying)
            {
                sprayParticles.Stop();
            }
        }
    }
    public void OnBodyGrabbed(SelectEnterEventArgs args) { primaryInteractor = args.interactorObject; }
    public void OnBodyReleased(SelectExitEventArgs args) { primaryInteractor = null; secondaryInteractor = null; }
    public void OnNozzleGrabbed(SelectEnterEventArgs args) { secondaryInteractor = args.interactorObject; }
    public void OnNozzleReleased(SelectExitEventArgs args) { secondaryInteractor = null; }
    private void OnDestroy()
    {
        bodyInteractable.selectEntered.RemoveListener(OnBodyGrabbed);
        bodyInteractable.selectExited.RemoveListener(OnBodyReleased);
        nozzleInteractable.selectEntered.RemoveListener(OnNozzleGrabbed);
        nozzleInteractable.selectExited.RemoveListener(OnNozzleReleased);
    }
}
