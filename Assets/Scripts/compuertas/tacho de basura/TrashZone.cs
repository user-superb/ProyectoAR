using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TrashZone : MonoBehaviour
{
    [Tooltip("Opcional: solo permite borrar objetos con esta tag (ej: 'Gate')")]
    public string requiredTag = "Gate";

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[TrashZone] Detectado objeto: {other.name}, tag: {other.tag}");

        // Chequeo de tag (si está configurado)
        if (!string.IsNullOrEmpty(requiredTag) && !other.CompareTag(requiredTag))
        {
            Debug.Log($"[TrashZone] {other.name} ignorado: no coincide con tag requerido '{requiredTag}'.");
            return;
        }

        // Buscamos si el objeto (o algún padre) implementa IDeletable
        var deletable = other.GetComponentInParent<IDeletable>();
        if (deletable != null)
        {
            Debug.Log($"[TrashZone] Eliminando objeto válido: {deletable.GetDisplayName()} (detectado en {other.name})");
            deletable.Delete();
            Debug.Log($"[TrashZone] {deletable.GetDisplayName()} eliminado correctamente.");
        }
        else
        {
            Debug.Log($"[TrashZone] {other.name} no tiene componente IDeletable en su jerarquía.");
        }
    }
}
