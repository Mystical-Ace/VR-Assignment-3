using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class HoseRenderer : MonoBehaviour
{
    [Header("References")]
    public Transform hoseStartPoint;
    public Transform hoseEndPoint;
    [Header("Hose Settings")]
    public int segments = 20;
    public float sagAmount = 0.5f;
    private LineRenderer lineRenderer;
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    void LateUpdate()
    {
        if (hoseStartPoint == null || hoseEndPoint == null) return;
        DrawHose();
    }
    private void DrawHose()
    {
        lineRenderer.positionCount = segments + 1;
        Vector3 p0 = hoseStartPoint.position;
        Vector3 p2 = hoseEndPoint.position;
        Vector3 p1 = (p0 + p2) / 2 + (Vector3.down * sagAmount);

        for (int i = 0; i <= segments; i++)
        {
            float t = i / (float)segments;
            Vector3 pointOnCurve = (1 - t) * (1 - t) * p0 + 2 * (1 - t) * t * p1 + t * t * p2;
            lineRenderer.SetPosition(i, pointOnCurve);
        }
    }
}
