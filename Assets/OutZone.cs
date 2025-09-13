using UnityEngine;

public class OutZone : MonoBehaviour
{
    UpdaterLineRenderer lineRenderer;
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("output");

        lineRenderer = gameObject.GetComponentInParent<UpdaterLineRenderer>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            if (!lineRenderer.activo()){ // Est� medio mal pero es un placeholder por ahora
                lineRenderer.actualizarPuntoB(other.transform.parent.gameObject); // Macumba
                other.transform.parent.GetChild(0).GetComponent<ComportamientoEntradas>().EsConnected();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        lineRenderer.actualizarPuntoB(null);
        other.transform.parent.GetChild(0).GetComponent<ComportamientoEntradas>().EsConnected();
    }
}
