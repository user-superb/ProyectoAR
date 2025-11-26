using UnityEngine;

public class InZone : MonoBehaviour
{
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("input");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == gameObject.layer)
        {
            if (other.CompareTag("ZonaOut"))
            {
                Conexion conexion = other.GetComponentInParent<Conexion>();
                if (conexion != null)
                {
                    Debug.Log("Conectado con el padre!");
                    int index = transform.GetSiblingIndex();
                    // conexion.AgregarCable(index, other.gameObject);
                }
                else
                {
                    Debug.Log("No se encontró Conexion en los padres");
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.layer == gameObject.layer)
        //{
        //    if (other.CompareTag("ZonaOut"))
        //    {
        //        Conexion conexion = GetComponentInParent<Conexion>();
        //        if (conexion != null)
        //        {
        //            Debug.Log("Objeto salió de la zona, eliminando cable");
        //            int index = conexion.GetCableIndex(other.gameObject, transform);
        //            if (index >= 0)
        //            {
        //                conexion.EliminarCable(index);
        //            }
        //        }
        //    }
        //}
    }
}
