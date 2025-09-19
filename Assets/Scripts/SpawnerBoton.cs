using UnityEngine;

public class SpawnerBoton : MonoBehaviour
{
    // Prefab a instanciar (lo arrastrás desde Project al Inspector)
    public GameObject prefab;

    // Lugar donde aparecerá el prefab
    public Vector3 posicionSpawn = new Vector3(0, 1, 0);
    public Quaternion rotacionSpawn = Quaternion.identity;

    // Método que vamos a enganchar al botón
    public void SpawnPrefab()
    {
        if (prefab != null)
        {
            Instantiate(prefab, posicionSpawn, rotacionSpawn);
        }
        else
        {
            Debug.LogWarning("No hay prefab asignado en SpawnerBoton");
        }
    }
}
