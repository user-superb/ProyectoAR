using UnityEngine;

public class GateDeletable : MonoBehaviour, IDeletable
{
    public void Delete()
    {
        // Si usás pooling: Pool.Despawn(gameObject);
        Destroy(gameObject);
    }

    public string GetDisplayName() => gameObject.name;
}
