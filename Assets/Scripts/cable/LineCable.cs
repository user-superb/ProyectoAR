// LineCable.cs
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(LineRenderer))]
public class LineCable : MonoBehaviour
{
    public CableTip tipA, tipB;
    public int segments = 24;
    public float slack = 0.10f; // panza (en metros)
    LineRenderer lr;

    void Awake() { lr = GetComponent<LineRenderer>(); if (lr) lr.positionCount = segments; }
    void OnValidate() { if (!lr) lr = GetComponent<LineRenderer>(); if (lr) lr.positionCount = segments; }
    void LateUpdate() { Refresh(); }

    public void Refresh()
    {
        if (!lr || !tipA || !tipB) return;
        Vector3 p0 = tipA.transform.position;
        Vector3 p1 = tipB.transform.position;
        Vector3 mid = (p0 + p1) * 0.5f + Vector3.down * slack;

        for (int i = 0; i < segments; i++)
        {
            float t = i / (float)(segments - 1);
            Vector3 a = Vector3.Lerp(p0, mid, t);
            Vector3 b = Vector3.Lerp(mid, p1, t);
            lr.SetPosition(i, Vector3.Lerp(a, b, t));
        }
    }

    public bool IsLinked =>
        tipA && tipB && tipA.attached != null && tipB.attached != null
        && tipA.attached.type != tipB.attached.type; // válido solo si une Input con Output

    public Port OutputPort =>
        (tipA.attached && tipA.attached.type == PortType.Output) ? tipA.attached :
        (tipB.attached && tipB.attached.type == PortType.Output) ? tipB.attached : null;

    public Port InputPort =>
        (tipA.attached && tipA.attached.type == PortType.Input) ? tipA.attached :
        (tipB.attached && tipB.attached.type == PortType.Input) ? tipB.attached : null;

    public void Log(string msg) => Debug.Log($"[CABLE] {name}: {msg}");
}
