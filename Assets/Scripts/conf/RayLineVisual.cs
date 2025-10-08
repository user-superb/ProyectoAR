using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RayLineVisual : MonoBehaviour
{
    public Transform origin;   // el punto de salida del rayo
    public float length = 3f;  // largo de la línea
    private LineRenderer lr;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void LateUpdate()
    {
        if (origin == null) return;

        Vector3 start = origin.position;
        Vector3 end = start + origin.forward * length;

        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
}
