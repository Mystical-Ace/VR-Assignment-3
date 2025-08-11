using UnityEngine;

public class SmoothHoseController : MonoBehaviour
{
    public Transform[] hoseBones;
    public Transform nozzleTarget;
    [Range(0f, 1f)] public float bendSmoothness = 0.5f;

    private Quaternion[] restLocalRotations;

    void Start()
    {
        // Cache rest local rotations to use as base for bending
        restLocalRotations = new Quaternion[hoseBones.Length];
        for (int i = 0; i < hoseBones.Length; i++)
        {
            restLocalRotations[i] = hoseBones[i].localRotation;
        }
    }

    void LateUpdate()
    {
        if (hoseBones == null || hoseBones.Length < 2 || nozzleTarget == null) return;

        // Lock nozzle bone position and rotation
        var nozzleBone = hoseBones[hoseBones.Length - 1];
        nozzleBone.position = nozzleTarget.position;
        nozzleBone.rotation = nozzleTarget.rotation;

        for (int i = hoseBones.Length - 2; i >= 0; i--)
        {
            Vector3 dir = hoseBones[i + 1].position - hoseBones[i].position;
            if (dir == Vector3.zero) continue;

            Quaternion lookRotation = Quaternion.LookRotation(dir.normalized);
            Quaternion correction = Quaternion.Euler(-90f, 0f, 0f);
            Quaternion targetGlobalRot = lookRotation * correction;

            Quaternion parentRot = hoseBones[i].parent ? hoseBones[i].parent.rotation : Quaternion.identity;
            Quaternion targetLocalRot = Quaternion.Inverse(parentRot) * targetGlobalRot;

            float maxAngleDelta = 30f * bendSmoothness * Time.deltaTime;
            hoseBones[i].localRotation = Quaternion.RotateTowards(hoseBones[i].localRotation, targetLocalRot, maxAngleDelta);
        }
    }

}
