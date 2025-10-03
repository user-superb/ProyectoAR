using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EntradaZone : MonoBehaviour
{
    // Referencia al “modelo” de la entrada (tu script que pinta y guarda value)
    [SerializeField] private ComportamientoEntradas entrada; 

    // La salida con la que estoy conectado ahora
    private IOutputProvider salidaConectada;
    private Transform salidaRoot; // para chequear OnTriggerExit exacto
    [SerializeField] private LineRenderer cable; // opcional: un cable por entrada

    [Header("Capas")]
    [SerializeField] private string capaSalida = "output"; // la capa de las salidas

    void Reset()
    {
        // Si me olvidé de asignar, intento encontrar el ComportamientoEntradas en mi parent
        if (entrada == null) entrada = GetComponentInParent<ComportamientoEntradas>();
    }

    void Update()
    {
        if (salidaConectada != null && entrada != null)
        {
            bool v = salidaConectada.GetSalida();
            entrada.tomarValor(v);

            // Opcional: actualizá el cable si necesitás posiciones dinámicas
            if (cable != null)
            {
                // Punto A = mi entrada, Punto B = la salida
                cable.positionCount = 2;
                cable.SetPosition(0, transform.position);
                cable.SetPosition(1, salidaRoot != null ? salidaRoot.position : transform.position);
            }
        }
    }

void OnTriggerEnter(Collider other)
{
    Debug.Log($"[EntradaZone] OnTriggerEnter con {other.gameObject.name}, capa {other.gameObject.layer}");

    // ¿Entró una salida?
    if (other.gameObject.layer == LayerMask.NameToLayer(capaSalida))
    {
        Debug.Log($"[EntradaZone] {other.gameObject.name} está en la capa de salidas ({capaSalida})");

        // Busco un provider en esa salida o en sus padres
        var provider = other.GetComponentInParent<IOutputProvider>();
        if (provider == null)
        {
            Debug.LogWarning($"[EntradaZone] {other.gameObject.name} no tiene IOutputProvider en sus padres");
            return;
        }

        // Si ya estaba conectada, ignorar (o permitir reemplazo)
        if (salidaConectada != null)
        {
            Debug.Log($"[EntradaZone] Ya estaba conectada a {salidaRoot?.name}, ignoro esta nueva conexión con {other.gameObject.name}");
            return;
        }

        // Me conecto
        salidaConectada = provider;
        salidaRoot = other.transform;
        Debug.Log($"[EntradaZone] Conectada a salida {salidaRoot.name}");

        if (entrada != null)
        {
            entrada.EsConnected(true);
            Debug.Log("[EntradaZone] Marcada como conectada en ComportamientoEntradas");
        }

        // Inicializo valor al instante
        bool v = salidaConectada.GetSalida();
        Debug.Log($"[EntradaZone] Valor inicial leído de salida: {v}");
        entrada?.tomarValor(v);

        // Cable opcional
        if (cable != null)
        {
            cable.enabled = true;
            cable.positionCount = 2;
            cable.SetPosition(0, transform.position);
            cable.SetPosition(1, salidaRoot.position);
            Debug.Log($"[EntradaZone] Cable activado entre {gameObject.name} y {salidaRoot.name}");
        }
    }
    else
    {
        Debug.Log($"[EntradaZone] {other.gameObject.name} no está en la capa de salida, ignoro");
    }
}


    void OnTriggerExit(Collider other)
    {
        // Me desconecto solo si el que salió es la MISMA salida con la que estaba
        if (salidaRoot != null && other.transform == salidaRoot)
        {
            if (entrada != null)
            {
                entrada.EsConnected(false);
                entrada.tomarValor(false); // caída a falso al desconectar
            }

            salidaConectada = null;
            salidaRoot = null;

            if (cable != null)
            {
                cable.enabled = false;
                cable.positionCount = 0;
            }
        }
    }
}
