// SignalEmitter.cs (va en out_collider)
using UnityEngine;
public class SignalEmitter : MonoBehaviour {
    public bool current;
    public void Set(bool v) { current = v; }
}