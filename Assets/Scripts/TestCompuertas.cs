using UnityEngine;

public class TestCompuertas : MonoBehaviour
{
    public CompuertaLogica compuerta;

    bool salidaAnterior;

    void Start()
    {
        if (compuerta == null)
        {
            Debug.LogError("No se asignó la compuerta lógica.");
        }
    }

    void Update()
    {
        if (compuerta != null)
        {
            bool salida = compuerta.CalcularSalida();

            // Solo mostrar si cambió
            if (salida != salidaAnterior)
            {
                Debug.Log("Nueva salida: " + salida);
                salidaAnterior = salida;

                Renderer rend = GetComponent<Renderer>();
                if (rend != null)
                    rend.material.color = salida ? Color.green : Color.red;
            }
        }
    }
}
