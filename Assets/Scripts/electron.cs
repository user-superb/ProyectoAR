using UnityEngine;

public class electron : MonoBehaviour, InterfazComp
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
        salida = true;
    }
    void Update()
    {
        CalcularSalida();
    }
    public void CalcularSalida(){
        if (entrada != null)
        {
            // lógica booleana NOT
            salida = true;

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