using UnityEngine;

public class OutZone : MonoBehaviour
{
    // Guarda la referencia a la entrada de otra compuerta cuando la conectamos
    private ComportamientoEntradas ce = null;

    // Referencia al script que maneja el dibujado de la línea (cable)
    UpdaterLineRenderer lineRenderer;

    void Start()
    {
        // Pone este objeto en la capa "output" (seguramente la salida de la compuerta)
        gameObject.layer = LayerMask.NameToLayer("output");

        // Busca el LineRenderer en el padre, para después poder actualizar el cable
        lineRenderer = gameObject.GetComponentInParent<UpdaterLineRenderer>();
    }

    void Update(){
        // Si tenemos una entrada conectada (ce != null)
        if (ce != null)
        {
            // Le pasa a la entrada el valor lógico que tiene la salida de esta compuerta
            // O sea: cada frame le copia el valor de salida de esta compuerta a la otra
            ce.tomarValor(transform.parent.GetChild(0).GetComponent<InterfazComp>().GetSalida());
            
        }
    }

void OnTriggerEnter(Collider other)
{
    // Si el objeto que entra está en la capa 10 (entrada de otra compuerta)
    if (other.gameObject.layer == 10)
    {
        // Guardo la referencia a esa entrada
        ce = other.transform.parent.GetChild(0).GetComponent<ComportamientoEntradas>();

        // Si todavía no hay un cable dibujado
        if (!lineRenderer.activo()) 
        {
            // Le digo al LineRenderer que el punto B es esa otra compuerta (dibuja el cable)
            lineRenderer.actualizarPuntoB(other.transform.parent.gameObject);

            // Marco la entrada de la otra compuerta como "conectada"
            ce.EsConnected(true);

            // Pregunto si mi salida tiene un valor lógico
            bool? salida = transform.parent.GetChild(0).GetComponent<InterfazComp>().GetSalida();

            if (salida != null) 
            {
                // Paso ese valor lógico a la entrada de la otra compuerta
                ce.tomarValor(salida.Value);
            }
            else
            {
                // Si no hay valor, solo tiro un mensaje de debug
                Debug.Log("log");
            }
        }
    }
}


    void OnTriggerExit(Collider other)
    {
        // Si se separan, borra el punto B de la línea (corta el cable)
        lineRenderer.actualizarPuntoB(null);

        // Marca la entrada como desconectada
        other.transform.parent.GetChild(0).GetComponent<ComportamientoEntradas>().EsConnected(false);

        // Le manda un valor falso (0 lógico) a la entrada de la otra compuerta
        // Esto simula que si no está conectada, la entrada queda en "falso"
        other.transform.parent.GetChild(0).GetComponent<ComportamientoEntradas>().tomarValor(false);

        // Resetea la referencia a la entrada
        ce = null;
    }
}
