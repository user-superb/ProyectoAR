using System.Security.Principal;
using UnityEngine;

public class comportamientoEntradaSalida : MonoBehaviour
{
    public enum TipoConector { input, output, ninguno }

    public TipoConector estadoNuevo = TipoConector.input;
    private TipoConector estadoActual = TipoConector.ninguno;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (estadoActual != estadoNuevo)
        {

            if (estadoNuevo == TipoConector.input)
            {
                configurarEntrada();
            }
            else
            {
                configurarSalida();
            }
            estadoActual = estadoNuevo;

        }

    }
    void configurarEntrada()
    {
        GameObject principalA = gameObject;
        GameObject colliderA = transform.parent.Find("collider").gameObject;
        setearEntrada(principalA, colliderA);


        GameObject principalB = transform.parent.parent.Find("b").Find("b").gameObject;
        GameObject colliderB = transform.parent.parent.Find("b").Find("collider").gameObject;
        setearSalida(principalB, colliderB);

        conectarSalida(principalA, principalB);


        colliderA.GetComponent<BoxCollider>().enabled = false;
        colliderA.GetComponent<BoxCollider>().enabled = true;
    }
    void conectarSalida(GameObject entrada, GameObject salida)
    {
        salidaDelCable componenteSalida = salida.GetComponent<salidaDelCable>();
        componenteSalida.entrada = entrada.GetComponent<ComportamientoEntradas>();

    }
    void configurarSalida()
    {
        GameObject principalA = gameObject;
        GameObject colliderA = transform.parent.Find("collider").gameObject;
        setearSalida(principalA, colliderA);


        GameObject principalB = transform.parent.parent.Find("b").Find("b").gameObject;
        GameObject colliderB = transform.parent.parent.Find("b").Find("collider").gameObject;
        setearEntrada(principalB, colliderB);

        conectarSalida(principalB, principalA);

        colliderB.GetComponent<BoxCollider>().enabled = false;
        colliderB.GetComponent<BoxCollider>().enabled = true;
    }

    void setearSalida(GameObject Principal, GameObject collider)
    {
        // borro los script de salidas
        ComportamientoEntradas entrada = Principal.GetComponent<ComportamientoEntradas>();
        if (entrada != null)
        {
            Destroy(entrada);
        }
        //GameObject collider = transform.parent.Find("collider").gameObject;
        EntradaZone zonaEntrada = collider.GetComponent<EntradaZone>();
        if (zonaEntrada != null)
        {
            Debug.Log("[comportamientoEntradaSalida] se borra zona entrada");
            Destroy(zonaEntrada);
        }
        salidaDelCable salida = Principal.AddComponent<salidaDelCable>();
        OutZone2 zonaSalida = collider.AddComponent<OutZone2>();

        collider.layer = LayerMask.NameToLayer("output");


    }


    void setearEntrada(GameObject Principal, GameObject collider)
    {
        // borro los script de salidas
        salidaDelCable salida = Principal.GetComponent<salidaDelCable>();
        if (salida != null)
        {
            Destroy(salida);
        }
        //GameObject collider = transform.parent.Find("collider").gameObject;
        OutZone2 zonaSalida = collider.GetComponent<OutZone2>();
        if (zonaSalida != null)
        {
            Debug.Log("[comportamientoEntradaSalida] se borra zona salida");
            Destroy(zonaSalida);
        }
        ComportamientoEntradas entradas = Principal.AddComponent<ComportamientoEntradas>();
        EntradaZone zonaEntrada = collider.AddComponent<EntradaZone>();

        zonaEntrada.entrada = entradas;
        GameObject cable = transform.parent.Find("CableEntrada").gameObject;
        zonaEntrada.cable = cable.GetComponent<LineRenderer>();

        collider.layer = LayerMask.NameToLayer("input");
    }


    public void Entrada()
    {
        estadoNuevo = TipoConector.input;
    }
    public void Salida()
    {
        estadoNuevo = TipoConector.output;
    }


}
