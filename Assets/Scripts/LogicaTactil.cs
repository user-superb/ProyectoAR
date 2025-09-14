using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class LogicaTactil : MonoBehaviour
{
    public ComportamientoEntradas IN;

    void OnEnable()  => EnhancedTouchSupport.Enable();
    void OnDisable() => EnhancedTouchSupport.Disable();

    void Update()
    {
#if UNITY_EDITOR
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            ProcesarToque(mousePosition);
        }
#endif
        if (Touch.activeTouches.Count > 0)
        {
            var t = Touch.activeTouches[0];
            if (t.phase == TouchPhase.Began)
            {
                ProcesarToque(t.screenPosition);
            }
        }
    }

    void ProcesarToque(Vector2 posicion)
    {
        Ray ray = Camera.main.ScreenPointToRay(posicion);
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
