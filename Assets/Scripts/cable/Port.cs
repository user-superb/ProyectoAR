// Port.cs
using UnityEngine;

public enum PortType { Output, Input }

[DisallowMultipleComponent]
public class Port : MonoBehaviour
{
    public PortType type;
    public Transform snapPoint;       // opcional; default = este transform
    public string gateName;           // solo para logs bonitos
    public string portName;           // ej. "out", "in1", "in2"

    void Reset() {
        snapPoint = transform;
        gateName = transform.root.name;
        portName = name;
        var col = GetComponent<Collider>();
        if (col) col.isTrigger = true;
    }

    void OnDrawGizmos() {
        Gizmos.color = (type == PortType.Output) ? Color.red : Color.green;
        var p = snapPoint ? snapPoint.position : transform.position;
        Gizmos.DrawSphere(p, 0.01f);
    }
}
