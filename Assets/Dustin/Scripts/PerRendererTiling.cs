using UnityEngine;

[DisallowMultipleComponent]
public class PerRendererTiling : MonoBehaviour
{
    [Header("Per-object UV controls")]
    public Vector2 tiling = Vector2.one;     // same as material tiling
    public Vector2 offset = Vector2.zero;    // same as material offset

    [Tooltip("If true, multiplies tiling by the object's average world scale so texel density stays similar when you scale the transform uniformly.")]
    public bool compensateForTransformScale = true;

    // Common ST property IDs across pipelines
    static readonly int BASEMAP_ST = Shader.PropertyToID("_BaseMap_ST");       // URP Lit
    static readonly int BASECOLOR_ST = Shader.PropertyToID("_BaseColorMap_ST");  // HDRP Lit
    static readonly int MAINTEX_ST = Shader.PropertyToID("_MainTex_ST");       // Built-in/legacy
    static readonly int BUMPMAP_ST = Shader.PropertyToID("_BumpMap_ST");       // Normal map ST
    static readonly int MASKMAP_ST = Shader.PropertyToID("_MaskMap_ST");       // HDRP mask map

    Vector3 _lastLossyScale;

    void OnEnable() => ApplyNow();

#if UNITY_EDITOR
    void OnValidate() { if (isActiveAndEnabled) ApplyNow(); }
#endif

    void LateUpdate()
    {
        if (!compensateForTransformScale) return;

        // Update if the transform scale changed at runtime
        if (_lastLossyScale != transform.lossyScale)
        {
            ApplyNow();
        }
    }

    void ApplyNow()
    {
        var r = GetComponent<Renderer>();
        if (!r) return;

        _lastLossyScale = transform.lossyScale;

        // Average world scale (good for uniform scaling; non-uniform is not knowable vs UVs)
        float avgScale = (transform.lossyScale.x + transform.lossyScale.y + transform.lossyScale.z) / 3f;
        if (!compensateForTransformScale) avgScale = 1f;

        Vector2 finalTiling = tiling * avgScale;
        Vector2 finalOffset = offset * finalTiling; // keep offset behavior similar to material UI

        var st = new Vector4(finalTiling.x, finalTiling.y, finalOffset.x, finalOffset.y);

        var mpb = new MaterialPropertyBlock();
        r.GetPropertyBlock(mpb);

        // Set ST on whichever textures exist on the material(s)
        if (HasTex(r, "_BaseMap")) mpb.SetVector(BASEMAP_ST, st); // URP
        if (HasTex(r, "_BaseColorMap")) mpb.SetVector(BASECOLOR_ST, st); // HDRP
        if (HasTex(r, "_MainTex")) mpb.SetVector(MAINTEX_ST, st); // Built-in/other
        if (HasTex(r, "_BumpMap")) mpb.SetVector(BUMPMAP_ST, st); // normal map ST
        if (HasTex(r, "_MaskMap")) mpb.SetVector(MASKMAP_ST, st); // HDRP mask

        r.SetPropertyBlock(mpb);
    }

    static bool HasTex(Renderer r, string name)
    {
        foreach (var m in r.sharedMaterials)
            if (m && m.HasProperty(name) && m.GetTexture(name) != null)
                return true;
        return false;
    }
}
