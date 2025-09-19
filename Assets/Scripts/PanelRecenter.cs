using UnityEngine;

public class PanelRecenter : MonoBehaviour
{
    [Header("Si lo dejás vacío, usa este GameObject")]
    public Transform panel;          // Raíz del Canvas en World Space
    [Header("Ajustes de ubicación")]
    public float distancia = 1.2f;   // metros frente a la cámara
    public Vector3 offset = Vector3.zero; // ajuste fino (mundo)

    [Header("Orientación")]
    public bool mirarALaCamara = true;
    public bool mantenerDerecho = true; // solo gira en Y (sin inclinarse)

    Camera cam;

    void Awake()
    {
        if (panel == null) panel = transform;
        cam = Camera.main;
    }

    public void ColocarFrenteACamara()
    {
        if (cam == null) cam = Camera.main;
        if (cam == null || panel == null) return;

        // Dirección hacia adelante
        Vector3 fwd = cam.transform.forward;
        if (mantenerDerecho)
            fwd = Vector3.ProjectOnPlane(fwd, Vector3.up).normalized;

        if (fwd.sqrMagnitude < 1e-4f) fwd = cam.transform.forward;

        // Posicionar
        Vector3 pos = cam.transform.position + fwd * distancia;
        panel.position = pos + offset;

        // Rotar para mirar a la cámara
        if (mirarALaCamara)
            panel.rotation = Quaternion.LookRotation(fwd, Vector3.up);
    }


    // Agregá esto dentro de PanelRecenter (debajo del código anterior)
    void OnEnable()
    {
        // Algunas versiones exponen este evento:
        if (OVRManager.display != null)
            OVRManager.display.RecenteredPose += OnRecenter;
    }

    void OnDisable()
    {
        if (OVRManager.display != null)
            OVRManager.display.RecenteredPose -= OnRecenter;
    }

    void OnRecenter()
    {
        ColocarFrenteACamara();
    }
}