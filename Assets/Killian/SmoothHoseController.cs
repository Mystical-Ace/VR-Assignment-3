using UnityEngine;

public class SmoothHoseController : MonoBehaviour
{
    public Transform[] hoseBones;   // Ordered from root to nozzle
    public Transform nozzleTarget;  // VR grip target for nozzle
    public Transform hoseBase;      // Fixed attachment point on extinguisher
    [Range(0f, 1f)] public float bendSmoothness = 0.5f;

    private Quaternion[] restLocalRotations;
    private Vector3[] restLocalPositions;

    void Start()
    {
        if (hoseBones == null || hoseBones.Length < 2)
        {
            Debug.LogError("HoseFollower: Please assign hose bones in order from root to nozzle.");
            enabled = false;
            return;
        }

        restLocalRotations = new Quaternion[hoseBones.Length];
        restLocalPositions = new Vector3[hoseBones.Length];
        for (int i = 0; i < hoseBones.Length; i++)
        {
            restLocalRotations[i] = hoseBones[i].localRotation;
            restLocalPositions[i] = hoseBones[i].localPosition;
        }
    }

    void LateUpdate()
    {
        if (nozzleTarget == null || hoseBase == null) return;

        // Keep the base bone locked to extinguisher head
        hoseBones[0].position = hoseBase.position;
        hoseBones[0].rotation = hoseBase.rotation;

        // Pin nozzle to VR target
        hoseBones[hoseBones.Length - 1].position = nozzleTarget.position;
        hoseBones[hoseBones.Length - 1].rotation = nozzleTarget.rotation;

        // Work backwards from second-to-last bone
        for (int i = hoseBones.Length - 2; i > 0; i--)
        {
            Transform bone = hoseBones[i];
            Transform child = hoseBones[i + 1];

            // Reset to rest pose each frame (avoids drift)
            bone.localPosition = restLocalPositions[i];
            bone.localRotation = restLocalRotations[i];

            // Find direction toward child in parent's local space
            Vector3 dirWorld = child.position - bone.position;
            if (dirWorld.sqrMagnitude < 0.000001f) continue;

            Vector3 dirLocal = bone.parent
                ? bone.parent.InverseTransformDirection(dirWorld.normalized)
                : dirWorld.normalized;

            // Y-axis is the bone's length direction from Blender
            Quaternion targetLocalRot = Quaternion.FromToRotation(Vector3.up, dirLocal) * restLocalRotations[i];

            // Smooth toward target
            bone.localRotation = Quaternion.Slerp(restLocalRotations[i], targetLocalRot, bendSmoothness);
        }
    }
}
