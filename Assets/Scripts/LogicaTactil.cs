using UnityEngine;
using UnityEngine.XR.ARFoundation; 
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class LogicaTactil : MonoBehaviour
{
    public ComportamientoEntradas IN;
    void Update()
    {
   /* #if UNITY_EDITOR
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            ProcesarToque(mousePosition);
        }
    #else*/
       // if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;
            ProcesarToque(touchPosition);
        }
   // #endif
    }

  

    void ProcesarToque(Vector2 posicion)
{
    Ray ray = Camera.main.ScreenPointToRay(posicion);

    // Layer que solo afecta a estos raycast
    int layerMask = LayerMask.GetMask("CapsuleOnly");

    if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
    {
        IN = hit.collider.GetComponent<ComportamientoEntradas>();
        if (IN != null)
        {
            Debug.Log("Objeto tocado: " + hit.collider.gameObject.name);
            IN.onTouch();
        }
        else
        {
            Debug.Log("Objeto tocado no tiene ComportamientoEntradas: " + hit.collider.gameObject.name);
        }
    }
    else
    {
        Debug.Log("Raycast no golpeó nada en la capa CapsuleOnly.");
    }
}



}
