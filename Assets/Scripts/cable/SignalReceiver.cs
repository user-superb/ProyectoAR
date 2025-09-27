// SignalReceiver.cs (va en input_collider_*)
using UnityEngine;
public class SignalReceiver : MonoBehaviour {
    public bool current;
    public void Apply(bool v) { current = v; }
}