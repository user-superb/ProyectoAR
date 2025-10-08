using UnityEngine;

public class SafeSpawner : MonoBehaviour
{
    [Header("Qué spawnear")]
    public GameObject prefab;

    [Header("Dónde spawnear")]
    public Transform puntoSpawn;           // arrastrás aquí tu SpawnPoint fijo
    public Vector3 offset = Vector3.zero;  // por si querés ajustar un poquito

    [Header("Chequeo de colisiones")]
    public LayerMask mascaraObstaculos = ~0; // qué capas chequea (por defecto todas)
    public Vector3 halfExtentsFallback = new Vector3(0.15f, 0.15f, 0.15f);

    public void SpawnSeguro()
    {
        if (prefab == null || puntoSpawn == null)
        {
            Debug.LogWarning("SafeSpawner: falta prefab o puntoSpawn asignado.");
            return;
        }

        // Posición final
        Vector3 pos = puntoSpawn.position + offset;
        Quaternion rot = puntoSpawn.rotation;

        // Calcular tamaño del prefab
        Vector3 halfExtents = CalcularHalfExtents(prefab) + Vector3.one * 0.02f;

        // Verificar si el lugar está libre
        bool libre = !Physics.CheckBox(pos, halfExtents, rot, mascaraObstaculos, QueryTriggerInteraction.Ignore);

        if (libre)
        {
            Instantiate(prefab, pos, rot);
        }
        else
        {
            Debug.Log("SafeSpawner: el lugar fijo está ocupado, no se instanció nada.");
        }
    }

    private Vector3 CalcularHalfExtents(GameObject pf)
    {
        var col = pf.GetComponentInChildren<Collider>();
        if (col != null) return col.bounds.extents;

        var rend = pf.GetComponentInChildren<Renderer>();
        if (rend != null) return rend.bounds.extents;

        return halfExtentsFallback;
    }
}
