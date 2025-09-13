using UnityEngine;

public class AND_Behaviour : MonoBehaviour, InterfazComp
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
        CalcularSalida();
    }
    public void CalcularSalida() {
    if (entradaA != null && entradaB != null)
    {
        salida = entradaA.value && entradaB.value;

        if (rendererSalida != null)
            rendererSalida.material.color = salida ? colorEncendido : colorApagado;
    }
    else
    {
        if (entradaA == null)
            Debug.LogWarning("entradaA no asignada!");
        if (entradaB == null)
            Debug.LogWarning("entradaB no asignada!");
    }
}

    public bool GetSalida()
    {
        return salida;
    }
}
