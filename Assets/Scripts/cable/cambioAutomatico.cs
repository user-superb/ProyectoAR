using UnityEngine;

public class cambioAutomatico : MonoBehaviour
{
    public comportamientoEntradaSalida comportamiento;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {

        string nombrePadre = transform.parent.name;
        int layerIndex = other.gameObject.layer;
        string layerName = LayerMask.LayerToName(layerIndex);
        if (layerName == "output")
        {
            Transform encontrado = transform.parent.Find("a");
            GameObject principal = null;
            if (encontrado != null)
            {
                principal = encontrado.gameObject;
                Debug.Log("Encontré el objeto: " + principal.name);
            }
            else
            {
                Debug.LogWarning("No se encontró el objeto 'a' dentro de " + transform.parent.name);
            }



            Debug.Log("[comportamientoEntradaSalida] se detecto que se conecto una salida, cambiando a entrada");
            if (nombrePadre == "a")
            {
                comportamiento.Entrada();
            }
            else
            {
                comportamiento.Salida();
            }
            

        }
    }
}
