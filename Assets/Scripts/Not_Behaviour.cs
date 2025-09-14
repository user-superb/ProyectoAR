using UnityEngine;

public class NOT_Behaviour : MonoBehaviour, InterfazComp
{
    
    public ComportamientoEntradas entrada;
    

    
    private MeshRenderer rendererSalida;
    public Color colorEncendido = Color.green;
    public Color colorApagado = Color.red;

    public bool salida;
    private UpdaterLineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = transform.parent.parent.GetComponent<UpdaterLineRenderer>();
        // Tomar el MeshRenderer del propio objeto
        rendererSalida = GetComponent<MeshRenderer>();
        salida = false;
    }
    void Update()
    {
        CalcularSalida();
    }
    public void CalcularSalida(){
        if (entrada != null)
        {
            // lógica booleana NOT
            salida = !entrada.value;
            lineRenderer.lineaActiva = salida;
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