using UnityEngine;
public class OR_Behaviour : MonoBehaviour, InterfazComp
{
    public ComportamientoEntradas entradaA;
    public ComportamientoEntradas entradaB;
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
                if (entradaA != null && entradaB != null)
        {
            // lógica booleana OR
            salida = entradaA.value | entradaB.value;
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
