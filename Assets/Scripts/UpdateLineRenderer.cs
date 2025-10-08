using System;
using UnityEngine;

public class UpdaterLineRenderer : MonoBehaviour
{
    public GameObject pointB;  // Segundo objeto
    public Boolean habilitarLinea = false;

    private Transform transformer;
    private LineRenderer line;
    public bool lineaActiva;
    private Gradient g;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        g = new Gradient();
        lineaActiva = false;
        line = GetComponent<LineRenderer>();
        line.positionCount = 4;
        line.startWidth = (float)0.0025;
        line.endWidth = (float)0.0025;
        #if UNITY_EDITOR
            line.startWidth = 0.1f;
            line.endWidth = 0.1f;
        #endif
        transformer = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (habilitarLinea)
        {
            if (!line.isVisible)
                line.enabled = true;
            if (pointB != null)
            {
                Vector3 a = new Vector3(transformer.position.x, transformer.position.y, transformer.position.z);

                Vector3 b = pointB.transform.position;

                Vector3 inter1 = new Vector3(b.x, a.y, a.z); // mueve en X
                Vector3 inter2 = new Vector3(b.x, b.y, b.z); // baja/sube en Y

                line.SetPosition(0, a);
                line.SetPosition(1, inter1);
                line.SetPosition(2, inter2);
                line.SetPosition(3, b);

            }
        }
        else
        {
            // Peligroso
            if (line.isVisible)
                line.enabled = false;
        }
        if (lineaActiva)
        {            
            g.SetKeys(
               new GradientColorKey[] { new GradientColorKey(Color.green, 0.0f), new GradientColorKey(Color.green, 1.0f) },
               new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) });
            line.colorGradient = g;
        }
        else
        {
            g.SetKeys(
               new GradientColorKey[] { new GradientColorKey(Color.red, 0.0f), new GradientColorKey(Color.red, 1.0f) },
               new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) });
            line.colorGradient = g;
         }
    }

    public bool activo()
    {
        return habilitarLinea;
    }

    public void actualizarPuntoB(GameObject b)
    {
        // Optimizar
        if (b == null)
            habilitarLinea = false;
        else
            habilitarLinea = true;

        pointB = b;
    }
}
