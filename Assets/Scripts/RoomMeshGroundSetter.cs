using UnityEngine;
using Oculus.Interaction; // solo si vas a habilitar el FirstPersonLocomotor
using Oculus.Interaction.Locomotion;

public class RoomMeshGroundSetter : MonoBehaviour
{
    [Tooltip("Nombre de la Layer que usás como 'suelo'")]
    public string groundLayerName = "piso";

    [Tooltip("Opcional: tu Locomotor, para habilitarlo cuando haya suelo")]
    public FirstPersonLocomotor locomotor;

    // Firma compatible con OnRoomMeshLoadCompleted (MeshFilter)
    public void OnRoomMeshLoaded(MeshFilter mf)
    {
        if (!mf) return;

        var go = mf.gameObject;

        // Asegurar que tenga MeshCollider
        var mc = go.GetComponent<MeshCollider>();
        if (!mc) mc = go.AddComponent<MeshCollider>();
        mc.sharedMesh = mf.sharedMesh;
        mc.convex = false;
        mc.isTrigger = false;

        // Poner en la capa Ground
        int groundLayer = LayerMask.NameToLayer(groundLayerName);
        if (groundLayer >= 0) go.layer = groundLayer;

        // Habilitar locomotor apenas haya un suelo válido
        if (locomotor && !locomotor.enabled)
            locomotor.enabled = true;
    }
}
