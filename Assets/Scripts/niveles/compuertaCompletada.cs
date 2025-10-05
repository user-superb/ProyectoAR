using UnityEngine;


public class compuertaCompletada : MonoBehaviour
{

    public bool esperarPositivo;

    public float tiempoNecesario = 3f;

    private float tiempoAcumulado = 0f;
    private bool gano = false;
    private IOutputProvider comp; // o el tipo de tu script de compuerta
    private ganasteNivel scriptGanasteNivel;
    void Start()
    {
        comp = GetComponent<IOutputProvider>();
        scriptGanasteNivel = GetComponent<ganasteNivel>();
        Debug.Log(comp);
    }

    void Update()
    {
        if (gano) return;
        //Debug.Log($"[compuertaCompletada] se espera un {esperarPositivo} y recibire un {comp.GetSalida()}");
        if (comp != null && (comp.GetSalida() == esperarPositivo)) // o comp.salida == true
        {
                tiempoAcumulado += Time.deltaTime;

                if (tiempoAcumulado >= tiempoNecesario)
                {
                    Debug.Log($"[compuertaCompletada] Compuerta encendida por {tiempoAcumulado:F1}s → GANASTE");
                    scriptGanasteNivel?.ganaste();
                    gano = true;

                }
        }
        else
        {
            //si se apaga, se resetea el contador
            if (tiempoAcumulado > 0)
                Debug.Log("[compuertaCompletada] Se apagó antes de llegar al tiempo necesario, reseteando temporizador.");
            tiempoAcumulado = 0f;
        }

    }
}
