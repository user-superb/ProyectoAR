using UnityEngine;

public class OR_Behaviour : MonoBehaviour
{
    
    public ComportamientoEntradas entradaA;
    public ComportamientoEntradas entradaB;

    
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
        if (entradaA != null && entradaB != null)
        {
            // lógica booleana OR
            salida = entradaA.isConnected || entradaB.isConnected;

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
