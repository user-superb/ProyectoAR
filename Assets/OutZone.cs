using UnityEngine;

public class OutZone : MonoBehaviour
{
    private ComportamientoEntradas ce = null;
    UpdaterLineRenderer lineRenderer;
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("output");
        lineRenderer = gameObject.GetComponentInParent<UpdaterLineRenderer>();
    }
    void Update(){
        if (ce != null)
        {
            ce.tomarValor(transform.parent.GetChild(0).GetComponent<InterfazComp>().GetSalida());
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            ce = other.transform.parent.GetChild(0).GetComponent<ComportamientoEntradas>();
            if (!lineRenderer.activo()){ // Est� medio mal pero es un placeholder por ahora
                lineRenderer.actualizarPuntoB(other.transform.parent.gameObject); // Macumba
                other.transform.parent.GetChild(0).GetComponent<ComportamientoEntradas>().EsConnected(true);
                if (transform.parent.GetChild(0).GetComponent<InterfazComp>().GetSalida() != null) //Macumba 1.1
                    other.transform.parent.GetChild(0).GetComponent<ComportamientoEntradas>().tomarValor(transform.parent.GetChild(0).GetComponent<InterfazComp>().GetSalida()); //Toma el valor de la salida y se lo aplica a la entrada de la otra compuerta
                else
                    Debug.Log("log");

            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        lineRenderer.actualizarPuntoB(null);
        other.transform.parent.GetChild(0).GetComponent<ComportamientoEntradas>().EsConnected(false);
        other.transform.parent.GetChild(0).GetComponent<ComportamientoEntradas>().tomarValor(false); //Cuando la compuerta se desconecta se ponen valores en falso
        ce = null;
    }
}
