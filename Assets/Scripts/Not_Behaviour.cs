using UnityEngine;
using UnityEngine;

public class NOT_Behaviour : MonoBehaviour
{
    
    public ComportamientoEntradas entrada;
    

    
    private MeshRenderer rendererSalida;
    public Color colorEncendido = Color.green;
    public Color colorApagado = Color.red;

    public bool salida;


    void Start()
    {
        // Tomar el MeshRenderer del propio objeto
        rendererSalida = GetComponent<MeshRenderer>();
        salida = false;
    }
    void Update()
    {
        if (entrada != null)
        {
            // lógica booleana NOT
            salida = !entrada.isConnected;

            // cambiar color del cable
            if (rendererSalida != null)
            {
                rendererSalida.material.color = salida ? colorEncendido : colorApagado;
            }
        }
    }

    public bool GetSalida()
    {
        return salida;
    }
}