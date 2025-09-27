// CableSignal.cs (va en el objeto Cable)
using UnityEngine;
public class CableSignal : MonoBehaviour {
    public LineCable cable;
    SignalEmitter src; SignalReceiver dst; bool last;

    void Update() {
        if (cable == null || !cable.IsLinked) { src = null; dst = null; return; }
        if (!src && cable.OutputPort) src = cable.OutputPort.GetComponent<SignalEmitter>();
        if (!dst && cable.InputPort)  dst = cable.InputPort.GetComponent<SignalReceiver>();
        if (src && dst && last != src.current) { last = src.current; dst.Apply(last); Debug.Log($"[CABLE] {name}: {last}"); }
    }
}