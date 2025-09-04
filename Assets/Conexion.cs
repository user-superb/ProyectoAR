using System;
using UnityEngine;

public class Conexion : MonoBehaviour
{
    public GameObject pointB;  // Segundo objeto
    public Boolean habilitarLinea = false;
    private Transform transformer;
    private LineRenderer line;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        line = gameObject.AddComponent<LineRenderer>();
        line.positionCount = 4;
        line.enabled = false;
        line.startWidth = 0.05f; // grosor inicial
        line.endWidth = 0.05f;   // grosor final
        line.material = new Material(Shader.Find("Sprites/Default")); // material simple
        line.startColor = Color.red;   // color inicial
        

        transformer = transform.GetChild(1);
        

    }

    // Update is called once per frame
    void Update()
    {

        if (habilitarLinea && (pointB != null))
        {
            if (!line.isVisible)
                line.enabled = true;
            if (pointB != null)
            {

                
                Vector3 a = new Vector3(transformer.position.x,transformer.position.y,transformer.position.z- (float)0.5);

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
    }
    public void recibirB(GameObject b)
    {
        pointB = b;
    }
}
