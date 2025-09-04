using UnityEngine;
using System.Collections.Generic;
public class InZone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Zonas");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == gameObject.layer)
        {
            if (other.CompareTag("ZonaOut"))
            {
                GameObject out1 = GameObject.Find("Out1");
                if (out1 != null)
                {
                    // Busca el script en el objeto o en sus padres
                    Conexion conexion = GetComponentInParent<Conexion>();
                    if (conexion != null)
                    {
                        Debug.Log("Conectado con el padre!");
                        conexion.recibirB(out1);
                        conexion.habilitarLinea = true;
                    }
                    else
                    {
                        Debug.Log("No se encontró Conexion en Out1 ni en sus padres");
                    }
                }
                else
                {
                    Debug.Log("No se encontró el objeto Out1");
                }
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}