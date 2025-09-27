// CableTip.cs
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Collider))]
public class CableTip : MonoBehaviour
{
    public LineCable owner;
    public Port attached;
    public float snapDistance = 0.03f;     // 3 cm
    public float detachDistance = 0.12f;   // 12 cm
    public LayerMask portMask;             // setear a capas de tus puertos

    void Update()
    {
        if (attached == null)
        {
            // Autodetección de puerto cercano
            var cols = Physics.OverlapSphere(transform.position, snapDistance, portMask,
                        QueryTriggerInteraction.Collide);
            Port best = null; float bestD = float.MaxValue;
            foreach (var c in cols)
            {
                var p = c.GetComponent<Port>();
                if (!p) continue;
                float d = Vector3.Distance(transform.position, p.snapPoint.position);
                if (d < bestD) { best = p; bestD = d; }
            }
            if (best != null) Attach(best);
        }
        else
        {
            // Mantenerse "pegado" al snapPoint mientras no te alejes demasiado
            float d = Vector3.Distance(transform.position, attached.snapPoint.position);
            if (d > detachDistance) Detach();
            else
            {
                transform.position = attached.snapPoint.position;
                transform.rotation = attached.snapPoint.rotation;
            }
        }
    }

    public void Attach(Port p)
    {
        attached = p;
        transform.position = p.snapPoint.position;
        transform.rotation = p.snapPoint.rotation;
        owner?.Refresh();
        owner?.Log($"SNAPPED {name} -> {p.gateName}/{p.portName} ({p.type})");
    }

    public void Detach()
    {
        if (attached != null)
            owner?.Log($"DETACHED {name} from {attached.gateName}/{attached.portName}");
        attached = null;
        owner?.Refresh();
    }
}
