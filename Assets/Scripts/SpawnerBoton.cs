using UnityEngine;

public class SpawnerBoton : MonoBehaviour
{
    [Header("Referencia")]
    [Tooltip("Objeto de referencia (Button, Canvas, cualquier Transform). Si está vacío usa este mismo.")]
    public Transform referencia;

    [Header("Spawn (offset relativo a la referencia)")]
    public Vector3 offsetLocal = new Vector3(0f, 0f, 0f);
    public Quaternion rotacionExtra = Quaternion.identity;

    [Header("UI")]
    [Tooltip("Opcional: contenedor donde colocar UI (Canvas). Si está vacío, se usa el Canvas raíz de la referencia.")]
    public Transform contenedorUI;

    [Header("Prefab")]
    public GameObject prefab;

    // Método para enganchar al botón
    public void SpawnPrefab()
    {
        if (prefab == null)
        {
            Debug.LogWarning("No hay prefab asignado en SpawnerBoton");
            return;
        }

        Transform refT = referencia != null ? referencia : transform;

        RectTransform refRect = refT as RectTransform;
        RectTransform prefabRect = prefab.GetComponent<RectTransform>();

        // Si ambos son UI (tienen RectTransform), hacemos spawn UI independiente
        if (refRect != null && prefabRect != null)
            SpawnUIIndependiente(refRect, prefab);
        else
            Spawn3DIndependiente(refT, prefab);
    }

    void Spawn3DIndependiente(Transform refT, GameObject prefabGO)
    {
        // Posición y rotación relativas a la referencia
        Vector3 posMundo = refT.TransformPoint(offsetLocal);
        Quaternion rotMundo = refT.rotation * rotacionExtra;

        // Instanciar sin padre (independiente)
        Instantiate(prefabGO, posMundo, rotMundo, null);
    }

    void SpawnUIIndependiente(RectTransform refRect, GameObject prefabGO)
    {
        // Encontrar Canvas destino (no el botón)
        Transform destino = contenedorUI;
        if (destino == null)
        {
            Canvas rootCanvas = refRect.GetComponentInParent<Canvas>();
            if (rootCanvas == null)
            {
                Debug.LogError("No se encontró un Canvas en la jerarquía. Asigná 'contenedorUI' o poné el objeto de referencia dentro de un Canvas.");
                return;
            }
            // Usamos el canvas raíz para que quede independiente del botón
            destino = rootCanvas.rootCanvas != null ? rootCanvas.rootCanvas.transform : rootCanvas.transform;
        }

        // 1) Calculamos posición en mundo a partir del offset local del ref
        Vector3 posMundo = refRect.TransformPoint(offsetLocal);

        // 2) Instanciamos temporalmente en la raíz de la escena (sin padre) para setear posición en mundo
        GameObject go = Instantiate(prefabGO, posMundo, Quaternion.identity, null);

        // 3) Ajustamos rotación en mundo = rotRef * extra
        go.transform.rotation = refRect.rotation * rotacionExtra;

        // 4) Lo movemos al Canvas destino conservando posición/rotación de mundo
        go.transform.SetParent(destino, worldPositionStays: true);

        // 5) Si es UI, asegurarnos de usar RectTransform y que no dependa del layout del botón
        RectTransform goRect = go.GetComponent<RectTransform>();
        if (goRect != null)
        {
            // Nada crítico que resetear acá; ya preservamos world position.
            // (Opcional) Si querés fijarlo a pixel-perfect en el Canvas:
            // goRect.anchoredPosition = (Vector2)goRect.anchoredPosition;
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Spawn (Editor)")]
    void SpawnDesdeContextMenu() => SpawnPrefab();
#endif
}
