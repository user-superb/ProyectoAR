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
        
        Debug.Log(other.gameObject.layer);
        if (other.gameObject.layer == gameObject.layer)
        {
            Debug.Log("Está en la misma capa");
        }
        else
        {
            Debug.Log("Capa distinta");
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

