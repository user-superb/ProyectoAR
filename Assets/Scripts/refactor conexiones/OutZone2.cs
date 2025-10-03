using UnityEngine;

public class OutZone2 : MonoBehaviour, IOutputProvider
{
    // No guardo entradas.
    // Solo leo el valor desde mi compuerta (padre) que implementa InterfazComp.

    public bool GetSalida()
    {
        var comp = transform.parent.GetChild(0).GetComponent<InterfazComp>();
        return comp != null && comp.GetSalida();
    }
}
