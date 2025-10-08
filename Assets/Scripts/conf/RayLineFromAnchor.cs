using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RayLineFromAnchor : MonoBehaviour
{
    public float length = 3f;
    public LayerMask mask = ~0;
    LineRenderer lr; Transform t;

    void Awake() {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.useWorldSpace = true;
        t = transform;
    }

    void LateUpdate() {
        Vector3 start = t.position;
        Vector3 dir   = t.forward;

        Vector3 end = start + dir * length;
        if (Physics.Raycast(start, dir, out var hit, length, mask))
            end = hit.point;

        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
}
